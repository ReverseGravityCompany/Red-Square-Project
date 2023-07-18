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
    [SerializeField] Image SoundSetting;
    [SerializeField] Color SoundSetting_OffColor, SoundSetting_OnColor;
    [SerializeField] GameObject ButtonStart, DecreaseLevel, IncreaseLevel, LevelNameSelection;
    [SerializeField] GameObject Pause, Home, RestartLevel;
    [SerializeField] Sprite PauseOff, PauseOn;

    [Space(20)]
    [SerializeField] GameObject MarkDifficaulty;
    [SerializeField] GameObject CoverMenu;
    [SerializeField] GameObject NoteHelperPanel;
    [SerializeField] Button NoteHelperButton;
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
    public Color NightColor, DayColor;
    public Sprite DaySprite, NightSprite;
    public Image daynightImage;
    private Camera camera;

    private bool PauseStatus;
    private int frameCount;


    #endregion

    #region Functions
    private void Awake()
    {
        
        if (CurrentLevel > 100)
        {
            CurrentLevel = 100;
        }
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
        LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 1000, 0.5f).SetUpdate(true);
        enemySystem = FindObjectOfType<EnemySystem>();
        camera = Camera.main;
        if (Helper_Menu != null && Hand != null)
        {
            Helper_Menu.SetActive(false);
            Hand.SetActive(false);
        }
        if (PlayerPrefs.HasKey("FastRun"))
        {
            if (PlayerPrefs.GetInt("FastRun") == 1)
            {
                StartGame();
                PlayerPrefs.SetInt("FastRun", 0);
            }
        }
        if (!PlayerPrefs.HasKey("DayAndNight"))
        {
            daynightImage.sprite = NightSprite;
            PlayerPrefs.SetInt("DayAndNight", 0);
            camera.backgroundColor = NightColor;

            LevelShowInLevel.color = DayColor;
            Pause.gameObject.GetComponent<Image>().color = DayColor;
            Home.gameObject.GetComponent<Image>().color = DayColor;
            RestartLevel.gameObject.GetComponent<Image>().color = DayColor;
        }
        else
        {
            if (PlayerPrefs.GetInt("DayAndNight") == 1) // 1 Day, 0 Night
            {
                daynightImage.sprite = DaySprite;
                camera.backgroundColor = DayColor;

                LevelShowInLevel.color = NightColor;
                Pause.gameObject.GetComponent<Image>().color = NightColor;
                Home.gameObject.GetComponent<Image>().color = NightColor;
                RestartLevel.gameObject.GetComponent<Image>().color = NightColor;
            }
            else if (PlayerPrefs.GetInt("DayAndNight") == 0) // 1 Day, 0 Night
            {
                daynightImage.sprite = NightSprite;
                camera.backgroundColor = NightColor;

                LevelShowInLevel.color = DayColor;
                Pause.gameObject.GetComponent<Image>().color = DayColor;
                Home.gameObject.GetComponent<Image>().color = DayColor;
                RestartLevel.gameObject.GetComponent<Image>().color = DayColor;
            }
        }


        if (SceneManager.GetActiveScene().buildIndex == 4 && !PlayerPrefs.HasKey("NoteHelperLevel4"))
        {
            PlayerPrefs.SetInt("NoteHelperLevel4", 0);
            Helper_Menu.SetActive(true);
            Hand.SetActive(true);
        }
    }

    private void Update()
    {
        // Use These Code Every 10 Frames
        //frameCount++;
        //if (frameCount % 10 == 0)
        //{
        //    if (CurrentLevel != SceneManager.GetActiveScene().buildIndex)
        //    {
        //        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        //        LevelMenu_Text.text = CurrentLevel.ToString();
        //    }
        //    frameCount = 0;
        //}
    }

    public void StartGame()
    {
        if (CurrentLevel >= 100)
        {
            CurrentLevel = 100;
        }


        if (CurrentLevel <= 100)
        {
            LevelManager._Instance.Game_AudioMixer.DOSetFloat("Lowpass_Music", 5000, 1.65f).SetUpdate(true);
            PlaySound.Play();
            SoundSetting.gameObject.SetActive(false);
            ButtonStart.SetActive(false);
            LevelMenu_Text.gameObject.SetActive(false);
            DecreaseLevel.gameObject.SetActive(false);
            IncreaseLevel.SetActive(false);
            Pause.SetActive(true);
            LevelNameSelection.SetActive(false);
            Time.timeScale = 1f;
            TouchScreen.SetActive(true);
            LevelShowInLevel.gameObject.SetActive(true);
            MarkDifficaulty.SetActive(false);
            RewardButton.SetActive(false);
            daynightImage.gameObject.SetActive(false);
            GameStarted = true;
            enemySystem.GenerateEnemy();
            GameObject.FindObjectOfType<SquareSoliderCount>().StartGenerateSolider();
            CoverMenu.SetActive(false);
            NoteHelperButton.gameObject.SetActive(false);
            NoteHelperPanel.gameObject.SetActive(false);
        }
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
            if (CurrentLevel >= 100)
            {
                CurrentLevel = 100;
                LevelMenu_Text.text = "Level " + CurrentLevel.ToString();
                return;
            }
            CurrentLevel += 1;
            int CheckLevel = CurrentLevel - 1;
            if (CurrentLevel <= PlayerPrefs.GetInt("UnlockLevel"))
            {
                PlayerPrefs.SetInt("MyLevel", CurrentLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void Menu()
    {
        PauseSound.Play();
        //if (Random.Range(0, 100) < 40)
        //{
        //    FindObjectOfType<InitialazeAdsMonitize>().ShowAd();
        //}
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseBut()
    {
        PauseSound.Play();
        if (PauseStatus)
        {
            Time.timeScale = 1f;
            PauseStatus = false;
            Pause.GetComponent<Image>().sprite = PauseOff;
            Home.SetActive(false);
            RestartLevel.SetActive(false);
            SoundSetting.gameObject.SetActive(false);
            daynightImage.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            PauseStatus = true;
            Pause.GetComponent<Image>().sprite = PauseOn;
            Home.SetActive(true);
            RestartLevel.SetActive(true);
            SoundSetting.gameObject.SetActive(true);
            daynightImage.gameObject.SetActive(true);
        }
    }

    public void ResetartLevel()
    {
        PauseSound.Play();
        PlayerPrefs.SetInt("FastRun", 1);
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NoteHelperButtonListener()
    {
        LevelChangeSound.Play();
        if (SceneManager.GetActiveScene().buildIndex == 4)
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

    public void DayAndNight()
    {
        LevelChangeSound.Play();
        if (PlayerPrefs.GetInt("DayAndNight") == 1) // 1 Day, 0 Night
        {
            daynightImage.sprite = NightSprite;
            PlayerPrefs.SetInt("DayAndNight", 0);
            camera.backgroundColor = NightColor;

            LevelShowInLevel.color = DayColor;
            Pause.gameObject.GetComponent<Image>().color = DayColor;
            Home.gameObject.GetComponent<Image>().color = DayColor;
            RestartLevel.gameObject.GetComponent<Image>().color = DayColor;
        }
        else if (PlayerPrefs.GetInt("DayAndNight") == 0) // 1 Day, 0 Night
        {
            daynightImage.sprite = DaySprite;
            PlayerPrefs.SetInt("DayAndNight", 1);
            camera.backgroundColor = DayColor;

            LevelShowInLevel.color = NightColor;
            Pause.gameObject.GetComponent<Image>().color = NightColor;
            Home.gameObject.GetComponent<Image>().color = NightColor;
            RestartLevel.gameObject.GetComponent<Image>().color = NightColor;
        }
    }

    #region Animation Events
    public void RunAddCoinAnimationEvent()
    {
        LevelManager._Instance.AddCoin();
    }
    #endregion

    #endregion
}
