using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;

[ExecuteInEditMode]
public class MenuSetting : MonoBehaviour
{
    [SerializeField] Image sound;
    [SerializeField] Color OffColor, OnColor;
    [SerializeField] GameObject ButStart, DereaseLevel, IncreaseLevel, ShowLevelGame;
    [SerializeField] GameObject Pause, Home, RestartLevel;
    [SerializeField] Sprite Pauseoff, PauseOn;
    [SerializeField] string urlYoutube;
    private GameObject MarkDif;
    public GameObject LanPlayer;
    public string SettingServerLevelName;
    private GameObject Tire;
    private GameObject NoteHelperPanel;
    private Button NoteHelperButton;
    public GameObject GiftObj;
    private GameObject YoutubeObj;
    private EnemySystem enemysystem;

    public int CurrentLevel;
    [SerializeField] Text LevelShow;

    public AudioSource LevelChangeSound, PlaySound, PauseSound;
    public bool Started = false;
    private bool PauseCheck;

    public GameObject TouchScreen;

    public Text LevelShowInLevel;
    public bool GameStarted = false;

    public GameObject ShowHelper, Hand;

    private LevelManager theLevelmanager;

    public Color NightColor, DayColor;
    public Sprite DaySprite, NightSprite;
    public Image daynightImage;
    private Camera camera;

    private void Awake()
    {
        if (CurrentLevel > 100)
        {
            CurrentLevel = 100;
        }
        StartCoroutine("ShowTextMultiplayer");
        LanPlayer.transform.GetChild(0).gameObject.SetActive(false);
        Tire = transform.GetChild(0).gameObject;
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            CurrentLevel = PlayerPrefs.GetInt("MyLevel");
            LevelShow.text = CurrentLevel.ToString();
        }
        else
        {
            CurrentLevel = 1;
            LevelShow.text = CurrentLevel.ToString();
        }

        if (PlayerPrefs.HasKey("SaveSound"))
        {
            if (PlayerPrefs.GetInt("SaveSound") == 0)
            {
                sound.color = OffColor;
                AudioListener.pause = true;
                AudioListener.volume = 0;
            }
            else if (PlayerPrefs.GetInt("SaveSound") == 1)
            {
                sound.color = OnColor;
                AudioListener.pause = false;
                AudioListener.volume = 1;
            }
        }
        theLevelmanager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        enemysystem = FindObjectOfType<EnemySystem>();
        NoteHelperPanel = gameObject.transform.Find("NoteHelperPanel").gameObject;
        NoteHelperButton = gameObject.transform.Find("NoteHelperButton").GetComponent<Button>();
        NoteHelperButton.onClick.AddListener(NoteHelperButtonListener);
        NoteHelperPanel.GetComponent<Button>().onClick.AddListener(NoteHelperPanelListener);
        YoutubeObj = gameObject.transform.Find("Youtube").gameObject;
        camera = Camera.main;
        if(ShowHelper != null && Hand != null)
        {
            ShowHelper.SetActive(false);
            Hand.SetActive(false);
        }
        urlYoutube = "https://www.youtube.com/channel/UCgXs2PTiL19Rv1qOn1SI7XQ";
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
            ShowHelper.SetActive(true);
            Hand.SetActive(true);
        }
        if (CurrentLevel != SceneManager.GetActiveScene().buildIndex)
        {
            CurrentLevel = SceneManager.GetActiveScene().buildIndex;
            LevelShow.text = CurrentLevel.ToString();
        }
    }

    private IEnumerator ShowTextMultiplayer()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (Random.Range(0, 100) > 80)
        {
            LanPlayer.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(15f);
            LanPlayer.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(Random.Range(10, 20));
        }
        yield return StartCoroutine(ShowTextMultiplayer());
    }

    public void MultiplayerButton()
    {
        PlaySound.Play();
        SceneManager.LoadScene(SettingServerLevelName);
    }

    private void Update()
    {
        if (CurrentLevel != SceneManager.GetActiveScene().buildIndex)
        {
            CurrentLevel = SceneManager.GetActiveScene().buildIndex;
            LevelShow.text = CurrentLevel.ToString();
        }
    }

    public void StartGame()
    {
        if (CurrentLevel >= 100)
        {
            CurrentLevel = 100;
        }
        if (CurrentLevel <= 100)
        {
            MarkDif = gameObject.transform.Find("MarkDiffculty").gameObject;
            PlaySound.Play();
            Started = true;
            LanPlayer.SetActive(false);
            YoutubeObj.SetActive(false);
            sound.gameObject.SetActive(false);
            ButStart.SetActive(false);
            LevelShow.gameObject.SetActive(false);
            DereaseLevel.gameObject.SetActive(false);
            IncreaseLevel.SetActive(false);
            Pause.SetActive(true);
            ShowLevelGame.SetActive(false);
            Time.timeScale = 1f;
            TouchScreen.SetActive(true);
            LevelShowInLevel.gameObject.SetActive(true);
            MarkDif.SetActive(false);
            GiftObj.SetActive(false);
            daynightImage.gameObject.SetActive(false);
            GameStarted = true;
            enemysystem.GenerateEnemy();
            GameObject.FindObjectOfType<squareSoliderCounte>().StartGenerateSolider();
            Tire.SetActive(false);
            if (NoteHelperButton != null && NoteHelperPanel != null)
            {
                NoteHelperButton.gameObject.SetActive(false);
                NoteHelperPanel.gameObject.SetActive(false);
            }
            else
            {
                NoteHelperPanel = gameObject.transform.Find("NoteHelperPanel").gameObject;
                NoteHelperButton = gameObject.transform.Find("NoteHelperButton").GetComponent<Button>();
                NoteHelperButton.gameObject.SetActive(false);
                NoteHelperPanel.gameObject.SetActive(false);
            }


            if (CurrentLevel != SceneManager.GetActiveScene().buildIndex)
            {
                CurrentLevel = SceneManager.GetActiveScene().buildIndex;
                LevelShow.text = CurrentLevel.ToString();
            }
        }
    }

    public void SoundChecking()
    {
        LevelChangeSound.Play();
        if (AudioListener.pause == true && AudioListener.volume == 0)
        {
            sound.color = OnColor;
            AudioListener.pause = false;
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("SaveSound", 1);
        }
        else if (AudioListener.pause != true && AudioListener.volume == 1)
        {
            sound.color = OffColor;
            AudioListener.pause = true;
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("SaveSound", 0);
        }
    }

    public void YoutubeJ()
    {
        YoutubemJoin(urlYoutube);
    }
    void YoutubemJoin(string url)
    {
        Application.OpenURL(url);
    }

    public void NextLevelBtn()
    {
        LevelChangeSound.Play();
        if (PlayerPrefs.HasKey("MyLevel"))
        {
            if (CurrentLevel >= 100)
            {
                CurrentLevel = 100;
                LevelShow.text = CurrentLevel.ToString();
                return;
            }
            CurrentLevel += 1;
            int CheckLevel = CurrentLevel - 1;
            if (CurrentLevel <= PlayerPrefs.GetInt("UnlockLevel"))
            {
                PlayerPrefs.SetInt("MyLevel", CurrentLevel);
                LevelShow.text = CurrentLevel.ToString();
                SceneManager.LoadScene(CurrentLevel);
            }
            else
            {
                CurrentLevel = CheckLevel;
                LevelShow.text = CurrentLevel.ToString();
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
                LevelShow.text = CurrentLevel.ToString();
                return;
            }
            CurrentLevel -= 1;
            PlayerPrefs.SetInt("MyLevel", CurrentLevel);
            LevelShow.text = CurrentLevel.ToString();
            SceneManager.LoadScene(CurrentLevel);
        }

    }

    public void ResetartLevel()
    {
        PauseSound.Play();
        PlayerPrefs.SetInt("FastRun", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        PauseSound.Play();
        if (Random.Range(0, 100) < 40)
        {
            FindObjectOfType<InitialazeAdsMonitize>().ShowAd();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseBut()
    {
        PauseSound.Play();
        if (PauseCheck)
        {
            Time.timeScale = 1f;
            PauseCheck = false;
            Pause.GetComponent<Image>().sprite = Pauseoff;
            Home.SetActive(false);
            RestartLevel.SetActive(false);
            sound.gameObject.SetActive(false);
            daynightImage.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            PauseCheck = true;
            Pause.GetComponent<Image>().sprite = PauseOn;
            Home.SetActive(true);
            RestartLevel.SetActive(true);
            sound.gameObject.SetActive(true);
            daynightImage.gameObject.SetActive(true);
        }
    }

    public void RunAddCoin()
    {
        GameObject.FindObjectOfType<LevelManager>().AddCoin();
    }

    public void NoteHelperButtonListener()
    {
        LevelChangeSound.Play();
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            ShowHelper.SetActive(false);
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

}
