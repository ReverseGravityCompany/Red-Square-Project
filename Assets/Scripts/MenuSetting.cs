using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DG.Tweening;

public class MenuSetting : MonoBehaviour
{
    #region Properties
    [SerializeField] public Image SoundSetting;
    [SerializeField] Color SoundSetting_OffColor, SoundSetting_OnColor;
    [SerializeField] public GameObject ButtonStart, DecreaseLevel, IncreaseLevel, LevelNameSelection;
    [SerializeField] public GameObject Pause, Home, RestartLevel;
    [SerializeField] public Sprite PauseOff, PauseOn;

    [Space(20)]
    [SerializeField] public Image MarkDifficaulty;
    [SerializeField] public GameObject CoverMenu;
    [SerializeField] public GameObject NoteHelperPanel;
    [SerializeField] public Button NoteHelperButton;
    public GameObject RewardButton;
    private EnemySystem enemySystem;

    private int CurrentLevel;
    [SerializeField] Text LevelMenu_Text;
    [SerializeField] Text LevelGame_Text;

    [Header("Sounds")]
    public AudioSource LevelChangeSound;
    public AudioSource PlaySound;
    public AudioSource PauseSound;

    [Space(15)]
    public GameObject TouchScreen;
    public Text LevelShowInLevel;
    [HideInInspector] public bool GameStarted = false;

    // Level 4 Helper
    public GameObject Helper_Menu, Hand;

    // Light/Dark Mode
    public Image BG_UI;
    public Image ColorPicker_UI;
    public Color BlackColor;
    public Sprite[] BG;
    public Sprite[] ColorPicker;
    private Camera camera;

    [HideInInspector] public bool PauseStatus;
    private int frameCount;

    public Sprite EasyImage, NormalImage, HardImage;

    public GameObject MenuPanel;

    public Sprite persian, english;
    public GameObject[] persianNote;
    public GameObject[] englishNote;

    private Animator anim;
    #endregion

    #region Functions
    private void Awake()
    {
        GameStarted = false;
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            CurrentLevel = PlayerPrefs.GetInt("MyLevel");

            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelGame_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }
        else
        {
            CurrentLevel = 1;
            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelGame_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }

        if (PlayerPrefs.HasKey("SaveSound"))
        {
            if (PlayerPrefs.GetInt("SaveSound") == 0)
            {
                SoundSetting.color = SoundSetting_OffColor;
                AudioListener.pause = true;
                AudioListener.volume = 0;
            }
            else if (PlayerPrefs.GetInt("SaveSound") == 1)
            {
                SoundSetting.color = SoundSetting_OnColor;
                AudioListener.pause = false;
                AudioListener.volume = 1;
            }
        }
    }

    private void Start()
    {
        camera = Camera.main;
        anim = GetComponent<Animator>();

        if (PlayerPrefs.HasKey("BG"))
        {
            int currentBG = PlayerPrefs.GetInt("BG");

            if (currentBG != 1)
            {
                BG_UI.sprite = BG[currentBG - 2];
                BG_UI.gameObject.SetActive(true);
                ColorPicker_UI.sprite = ColorPicker[currentBG - 1];
            }
            else
            {
                camera.backgroundColor = BlackColor;
                BG_UI.gameObject.SetActive(false);
                ColorPicker_UI.sprite = ColorPicker[0];
            }
        }
        else
        {
            PlayerPrefs.SetInt("BG", 1);

            camera.backgroundColor = BlackColor;
            BG_UI.gameObject.SetActive(false);
            ColorPicker_UI.sprite = ColorPicker[0];
        }

        if (!PlayerPrefs.HasKey("NotesRemainder") && CurrentLevel == 4)
        {
            PlayerPrefs.SetInt("NotesRemainder", 1);
            Hand.SetActive(true);
            Helper_Menu.SetActive(true);
        }

        LevelManager._Instance.Game_AudioMixer.SetFloat("Lowpass_Music", 1000);
        enemySystem = FindObjectOfType<EnemySystem>();


        if (enemySystem.Difficaulty == EnemySystem.Difficault.Easy)
        {
            MarkDifficaulty.sprite = EasyImage;
        }
        else if (enemySystem.Difficaulty == EnemySystem.Difficault.Meduim)
        {
            MarkDifficaulty.sprite = NormalImage;
        }
        else if (enemySystem.Difficaulty == EnemySystem.Difficault.Hard)
        {
            MarkDifficaulty.sprite = HardImage;
        }


        //if (PlayerPrefs.HasKey("FastRun"))
        //{
        //    if (PlayerPrefs.GetInt("FastRun") == 1)
        //    {
        //        StartGame();
        //        PlayerPrefs.SetInt("FastRun", 0);
        //    }
        //}
    }

    public void ResetSpecialData()
    {
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            CurrentLevel = PlayerPrefs.GetInt("MyLevel");

            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelGame_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }
        else
        {
            CurrentLevel = 1;
            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelGame_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }

        if (!PlayerPrefs.HasKey("NotesRemainder") && CurrentLevel == 4)
        {
            PlayerPrefs.SetInt("NotesRemainder", 1);
            Hand.SetActive(true);
            Helper_Menu.SetActive(true);
        }

        LevelManager._Instance.Game_AudioMixer.SetFloat("Lowpass_Music", 1000);

        enemySystem = FindObjectOfType<EnemySystem>();

        if (enemySystem.Difficaulty == EnemySystem.Difficault.Easy)
        {
            MarkDifficaulty.sprite = EasyImage;
        }
        else if (enemySystem.Difficaulty == EnemySystem.Difficault.Meduim)
        {
            MarkDifficaulty.sprite = NormalImage;
        }
        else if (enemySystem.Difficaulty == EnemySystem.Difficault.Hard)
        {
            MarkDifficaulty.sprite = HardImage;
        }


        //if (PlayerPrefs.HasKey("FastRun"))
        //{
        //    if (PlayerPrefs.GetInt("FastRun") == 1)
        //    {
        //        StartGame();
        //        PlayerPrefs.SetInt("FastRun", 0);
        //    }
        //}

    }

    public void StartGame()
    {
        LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 5000, 1.65f).SetUpdate(true);
        SoundSetting.gameObject.SetActive(false);
        ColorPicker_UI.gameObject.SetActive(false);
        PlaySound.Play();
        MenuPanel.SetActive(false);
        ButtonStart.SetActive(false);
        Pause.SetActive(true);
        Time.timeScale = 1f;
        TouchScreen.SetActive(true);
        LevelShowInLevel.gameObject.SetActive(true);

        if (LevelManager._Instance.DailyRewardCorotiune != null)
            StopCoroutine(LevelManager._Instance.DailyRewardCorotiune);

        if (CameraMovement._Instance.cam.orthographicSize == LevelManager._Instance.mainMenuCameraOrthgraphicSize[CurrentLevel - 1])
            CameraMovement._Instance.cam.orthographicSize = LevelManager._Instance.inGameCameraOrthgraphicSize[CurrentLevel - 1];

        GameStarted = true;
        enemySystem.GenerateEnemy();

        LevelManager._Instance.StartGenerateSolider();

        CoverMenu.SetActive(false);
    }

    public void SoundChecking()
    {
        LevelChangeSound.Play();
        if (AudioListener.pause == true && AudioListener.volume == 0)
        {
            SoundSetting.color = SoundSetting_OnColor;
            AudioListener.pause = false;
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("SaveSound", 1);
        }
        else if (AudioListener.pause != true && AudioListener.volume == 1)
        {
            SoundSetting.color = SoundSetting_OffColor;
            AudioListener.pause = true;
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("SaveSound", 0);
        }
    }

    public void NextLevelBtn()
    {
        LevelChangeSound.Play();
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            if (CurrentLevel >= 200)
            {
                CurrentLevel = 200;
                LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
                return;
            }
            CurrentLevel += 1;
            int CheckLevel = CurrentLevel - 1;
            if (CurrentLevel <= PlayerPrefs.GetInt("UnlockLevel"))
            {
                PlayerPrefs.SetInt("MyLevel", CurrentLevel);
                LevelManager._Instance.ResetGameData();
            }
            else
            {
                CurrentLevel = CheckLevel;
            }
        }
    }

    public void PreviousLevel()
    {
        LevelChangeSound.Play();
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            if (CurrentLevel == 1)
            {
                CurrentLevel = 1;
                LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
                return;
            }
            CurrentLevel -= 1;
            PlayerPrefs.SetInt("MyLevel", CurrentLevel);
            LevelManager._Instance.ResetGameData();
        }

    }

    public void Menu()
    {
        PauseSound.Play();
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);
        LevelManager._Instance.ResetGameData();
    }

    public void PauseBut()
    {
        PauseSound.Play();
        if (PauseStatus)
        {

            LevelManager._Instance.Game_AudioMixer.SetFloat("Lowpass_Music", 1000);
            Time.timeScale = 1f;
            PauseStatus = false;
            Pause.GetComponent<Image>().sprite = PauseOff;
            Home.SetActive(false);
            RestartLevel.SetActive(false);
            SoundSetting.gameObject.SetActive(false);
            ColorPicker_UI.gameObject.SetActive(false);
        }
        else
        {
            LevelManager._Instance.Game_AudioMixer.SetFloat("Lowpass_Music", 650);
            Time.timeScale = 0f;
            PauseStatus = true;
            Pause.GetComponent<Image>().sprite = PauseOn;
            Home.SetActive(true);
            RestartLevel.SetActive(true);
            SoundSetting.gameObject.SetActive(true);
            ColorPicker_UI.gameObject.SetActive(true);
        }
    }

    public void ResetartLevel()
    {
        PauseSound.Play();

        //PlayerPrefs.SetInt("FastRun", 1);
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);

        LevelManager._Instance.ResetGameData();
    }

    public void NoteHelperButtonListener()
    {
        LevelChangeSound.Play();
        if (CurrentLevel == 4)
        {
            Helper_Menu.SetActive(false);
            Hand.SetActive(false);
        }
        NoteHelperPanel.SetActive(true);
    }

    public void NoteHelperPanelListener()
    {
        NoteHelperPanel.SetActive(false);
    }

    public void BackgroundPicker()
    {
        LevelChangeSound.Play();
        if (PlayerPrefs.HasKey("BG"))
        {
            int currentBG = PlayerPrefs.GetInt("BG");

            currentBG++;

            if (currentBG > 5)
            {
                BG_UI.gameObject.SetActive(false);
                ColorPicker_UI.sprite = ColorPicker[0];
                PlayerPrefs.SetInt("BG", 1);
                return;
            }

            BG_UI.gameObject.SetActive(true);
            BG_UI.sprite = BG[currentBG - 2];
            ColorPicker_UI.sprite = ColorPicker[currentBG - 1];
            PlayerPrefs.SetInt("BG", currentBG);
        }
    }

    public void GuideLangauage(Image img)
    {
        if(img.sprite == persian)
        {
            img.sprite = english;
            foreach(var obj in persianNote)
            {
                obj.SetActive(false);
            }
            foreach(var obj in englishNote)
            {
                obj.SetActive(true);
            }
        }
        else if(img.sprite == english)
        {
            img.sprite = persian;
            foreach (var obj in englishNote)
            {
                obj.SetActive(false);
            }
            foreach (var obj in persianNote)
            {
                obj.SetActive(true);
            }
        }
    }

    #region Animation Events
    public void RunAddCoinAnimationEvent()
    {
        anim.SetBool("Coin", false);
        LevelManager._Instance.AddCoin();
    }
    #endregion

    #endregion
}
