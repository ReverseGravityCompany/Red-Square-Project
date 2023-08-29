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
    [SerializeField] public Image SoundSetting, MusicSetting, VibrationSetting;
    [SerializeField] Color SoundSetting_OffColor, SoundSetting_OnColor, MusicSetting_OnColor, Vibration_OnColor;
    [SerializeField] public GameObject ButtonStart, DecreaseLevel, IncreaseLevel, LevelNameSelection;
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

    [Header("Sounds")]
    public AudioSource LevelChangeSound;
    public AudioSource PlaySound;
    public AudioSource PauseSound;
    public AudioSource BackSound;

    [Space(15)]
    public GameObject TouchScreen;
    [HideInInspector] public bool GameStarted = false;

    // Level 4 Helper
    public GameObject Helper_Menu, Hand;

    private Camera camera;

    [HideInInspector] public bool PauseStatus;
    private int frameCount;

    public Sprite EasyImage, NormalImage, HardImage;

    public GameObject MenuPanel;

    public Sprite persian, english;
    public GameObject[] persianNote;
    public GameObject[] englishNote;

    private Animator anim;

    public Coroutine ScoreCorotuine;

    public GameObject BackPanel;
    public GameObject backButton;

    public GameObject SettingsPanel;

    public Image CameraLockImage;
    public Sprite LockCameraSprite, UnlockCameraSprite;

    public GameObject CameraTutPanel;
    #endregion

    #region Functions
    private void Awake()
    {
        GameStarted = false;
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            CurrentLevel = PlayerPrefs.GetInt("MyLevel");

            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }
        else
        {
            CurrentLevel = 1;
            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }
    }

    private void Start()
    {
        camera = Camera.main;
        anim = GetComponent<Animator>();

        if (PlayerPrefs.HasKey("SaveMusic"))
        {
            int res = PlayerPrefs.GetInt("SaveMusic");

            if (res == 0)
            {
                MusicSetting.color = SoundSetting_OffColor;
                PlayerPrefs.SetInt("SaveMusic", 0);
                LevelManager._Instance.Game_AudioMixer.DOSetFloat("MusicVolume", -80, 0).SetEase(Ease.Linear).SetUpdate(true);
            }
            else
            {
                MusicSetting.color = MusicSetting_OnColor;
                PlayerPrefs.SetInt("SaveMusic", 1);
                LevelManager._Instance.Game_AudioMixer.DOSetFloat("MusicVolume", 0, 0).SetEase(Ease.Linear).SetUpdate(true);
            }
        }

        if (PlayerPrefs.HasKey("Vibration"))
        {
            int vib = PlayerPrefs.GetInt("Vibration");

            if (vib == 0)
            {
                VibrationSetting.color = SoundSetting_OffColor;
                LevelManager._Instance.AllowVibration = false;
            }
            else
            {
                VibrationSetting.color = Vibration_OnColor;
                LevelManager._Instance.AllowVibration = true;
            }
        }
        else
        {
            VibrationSetting.color = Vibration_OnColor;
            PlayerPrefs.SetInt("Vibration", 1);
            LevelManager._Instance.AllowVibration = true;
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
    }

    public void ResetSpecialData()
    {
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            CurrentLevel = PlayerPrefs.GetInt("MyLevel");

            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
            LevelNameSelection.GetComponent<Text>().text = CurrentLevel.ToString();
        }
        else
        {
            CurrentLevel = 1;
            LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
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
    }

    public void StartGame()
    {
        LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 1000, 1.65f).SetUpdate(true);
        SoundSetting.gameObject.SetActive(false);
        PlaySound.Play();
        MenuPanel.SetActive(false);
        ButtonStart.SetActive(false);
        backButton.SetActive(true);
        BackPanel.SetActive(false);
        CameraLockImage.gameObject.SetActive(true);
        if (LevelManager._Instance.handOperaion != null)
        {
            LevelManager._Instance.handOperaion.gameObject.SetActive(true);
        }

        Time.timeScale = 1f;
        TouchScreen.SetActive(true);

        if (LevelManager._Instance.CurrentLevel == 1)
        {
            FindAnyObjectByType<Player>().gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.SetActive(true);
        }
        else if (LevelManager._Instance.CurrentLevel == 2)
        {
            FindAnyObjectByType<Player>().gameObject.transform.Find("Mortal(RE)").Find("Hand").gameObject.SetActive(true);
        }
        else if (LevelManager._Instance.CurrentLevel == 3)
        {
            FindAnyObjectByType<Player>().gameObject.transform.Find("Mortal(GE)").Find("Hand").gameObject.SetActive(true);
        }

        if (LevelManager._Instance.DailyRewardCorotiune != null)
            StopCoroutine(LevelManager._Instance.DailyRewardCorotiune);

        float ExtraOrthSize = 0.5f;

        if (LevelManager._Instance.AllMortalObjects.Count < 8)
            ExtraOrthSize = 0;

        if (CameraMovement._Instance.cam.orthographicSize == LevelManager._Instance.mainMenuCameraOrthgraphicSize[CurrentLevel - 1] + ExtraOrthSize)
            CameraMovement._Instance.cam.orthographicSize = LevelManager._Instance.inGameCameraOrthgraphicSize[CurrentLevel - 1] + ExtraOrthSize;

        GameStarted = true;
        enemySystem.GenerateEnemy();

        LevelManager._Instance.StartGenerateSolider();
        ScoreCorotuine = StartCoroutine(LevelManager._Instance.MultiSliderScore());

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

    public void MusicChecking()
    {
        LevelChangeSound.Play();
        if (PlayerPrefs.HasKey("SaveMusic"))
        {
            int res = PlayerPrefs.GetInt("SaveMusic");

            if (res == 0)
            {
                MusicSetting.color = MusicSetting_OnColor;
                PlayerPrefs.SetInt("SaveMusic", 1);
                LevelManager._Instance.Game_AudioMixer.DOSetFloat("MusicVolume", 0, 0).SetEase(Ease.Linear).SetUpdate(true);
            }
            else
            {
                MusicSetting.color = SoundSetting_OffColor;
                PlayerPrefs.SetInt("SaveMusic", 0);
                LevelManager._Instance.Game_AudioMixer.DOSetFloat("MusicVolume", -80f, 0).SetEase(Ease.Linear).SetUpdate(true);
            }
        }
        else
        {
            MusicSetting.color = SoundSetting_OffColor;
            PlayerPrefs.SetInt("SaveMusic", 0);
            LevelManager._Instance.Game_AudioMixer.DOSetFloat("MusicVolume", -80f, 0).SetEase(Ease.Linear).SetUpdate(true);
        }
    }

    public void SetCameraLockState()
    {
        if (!PlayerPrefs.HasKey("SetCameraLock_Tut"))
        {
            CameraTutPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 300, 0).SetEase(Ease.Linear).SetUpdate(true);
            PlayerPrefs.SetInt("SetCameraLock_Tut", 1);
        }

        if (CameraMovement._Instance.LockCamera == false)
        {
            CameraLockImage.sprite = LockCameraSprite;
            CameraMovement._Instance.LockCamera = true;
            CameraMovement._Instance.PressCount = 0;
        }
        else
        {
            CameraLockImage.sprite = UnlockCameraSprite;
            CameraMovement._Instance.LockCamera = false;
            CameraMovement._Instance.PressCount = 0;
        }
    }

    public void ShowCameraTut()
    {
        CameraTutPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 300, 0).SetEase(Ease.Linear).SetUpdate(true);
    }

    public void DisableCameraTutPanel()
    {
        Time.timeScale = 1;
        LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 1000, 1.65f).SetUpdate(true);
        CameraTutPanel.gameObject.SetActive(false);
    }

    public void CheckCameraState()
    {
        if (CameraMovement._Instance.LockCamera == false)
        {
            CameraLockImage.sprite = UnlockCameraSprite;
        }
        else
        {
            CameraLockImage.sprite = LockCameraSprite;
        }
    }

    public void VibrationChecking()
    {
        int vib = PlayerPrefs.GetInt("Vibration");

        if (vib == 0)
        {
            VibrationSetting.color = Vibration_OnColor;
            LevelManager._Instance.AllowVibration = true;
            PlayerPrefs.SetInt("Vibration", 1);
        }
        else
        {
            VibrationSetting.color = SoundSetting_OffColor;
            LevelManager._Instance.AllowVibration = false;
            PlayerPrefs.SetInt("Vibration", 0);
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
        BackSound.Play();
        BackPanel.SetActive(false);
        backButton.SetActive(false);
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);
        LevelManager._Instance.ResetGameData();
    }

    public void PauseBut()
    {
        BackSound.Play();
        if (PauseStatus)
        {
            LevelManager._Instance.Game_AudioMixer.SetFloat("Lowpass_Music", 1000);
            Time.timeScale = 1f;
            PauseStatus = false;
            BackPanel.SetActive(false);
        }
        else
        {
            LevelManager._Instance.Game_AudioMixer.SetFloat("Lowpass_Music", 300);
            Time.timeScale = 0f;
            PauseStatus = true;
            BackPanel.SetActive(true);
        }
    }

    public void OpenSettingPanel()
    {
        BackSound.Play();
        SettingsPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        SettingsPanel.SetActive(false);
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
    }

    public void GuideLangauage(Image img)
    {
        if (img.sprite == persian)
        {
            img.sprite = english;
            foreach (var obj in persianNote)
            {
                obj.SetActive(false);
            }
            foreach (var obj in englishNote)
            {
                obj.SetActive(true);
            }
        }
        else if (img.sprite == english)
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
