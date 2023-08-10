using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;

using Cafebazaar;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour
{
    #region Properties
    public GameObject Win, Lose;
    [HideInInspector] public int CurrentCoin;
    private int MaxCoin, MinCoin;
    public Text CoinText;

    int Unlocknextlevel;
    int CheckValueOfLevel;
    [HideInInspector] public bool SkillsState;
    public GameObject[] SkillsObject;

    private int CoinWinning;
    public Text CoinWiningText;
    public Animator anim;

    public AudioSource NextMatchSound;
    public AudioSource WinSound;
    public AudioSource MoneySound;
    public AudioSource LoseSound;
    public AudioSource SelectMortalSound, AttackNoneColorSound, AttackRedColorSound, AttackYellowColorSound, AttackSound;
    public AudioMixer Game_AudioMixer;


    public float CoinPosX = -180f;
    private int WinStatus;
    private int LoseStatus;
    public List<StateMortal> AllMortalObjects;
    public bool WinGame, LoseGame;

    private bool WinningRewardTaken;

    private bool IsCoinTaken = false;

    public static LevelManager _Instance { get; private set; }


    public GameObject[] HandCraftedLevels;
    public GameObject LevelsParent;
    [HideInInspector] public int CurrentLevel;

    [HideInInspector] public bool isGeneratingLevel;
    public int[] GoldForEachLevels;

    private string BazzarAPIURL = "https://minigames-api.cafebazaar.org/score/";

    [Header("Learning Properties")]
    public GameObject[] TutorialsLevels;
    public GameObject[] TutorialsCanvas;
    public bool LearningLevels;
    public MenuSetting UI_Canvas;
    private TutorialTask tutorialTask;

    public Color OutlineColor;

    public Vector3 OutlineSize;

    private bool CanReciveGift;
    private DateTime currentTime;
    private DateTime lastTimeClicked;

    public GameObject RewardExImage;
    public Image DailyRewardImage;
    public TextMeshProUGUI DailyRewardText;
    public Coroutine DailyRewardCorotiune;

    private WaitForSeconds timer;

    private Player thePlayer;

    public ParticleSystem BurnArmy;

    public float[] mainMenuCameraOrthgraphicSize;
    public float[] inGameCameraOrthgraphicSize;

    public Button winButton;
    public GameObject ErrorScoreSendingPanel;

    public GameObject Loading_dataToServer;
    public GameObject Info_dataToServer;
    #endregion

    #region Functions

    private void Awake()
    {
        Time.timeScale = 0;
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }

        LearningLevels = false;


        if (!PlayerPrefs.HasKey("MyLevel"))
        {
            LearningLevels = true;
            UI_Canvas.gameObject.SetActive(false);
            if (PlayerPrefs.HasKey("LearnProcess"))
            {
                int LearnProcess = PlayerPrefs.GetInt("LearnProcess");
                Instantiate(TutorialsLevels[LearnProcess - 1].gameObject, LevelsParent.transform, false);
                Instantiate(TutorialsCanvas[LearnProcess - 1].gameObject);

                StartGenerateSolider();
                tutorialTask = FindObjectOfType<TutorialTask>();
            }
            else
            {
                Instantiate(TutorialsLevels[0].gameObject, LevelsParent.transform, false);
                Instantiate(TutorialsCanvas[0].gameObject);

                // StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Learn\": " + 1 + "}"));


                StartGenerateSolider();
                tutorialTask = FindObjectOfType<TutorialTask>();
                PlayerPrefs.SetInt("LearnProcess", 1);
            }
            Time.timeScale = 1;
            return;
        }


        MaxCoin = 999999;
        MinCoin = 500;

        int level = 1;


        if (PlayerPrefs.HasKey("MyLevel"))
        {
            level = PlayerPrefs.GetInt("MyLevel");

            GameObject obj = Instantiate(HandCraftedLevels[level - 1].gameObject, LevelsParent.transform, false);
            CurrentLevel = level;
        }
        else
        {
            PlayerPrefs.SetInt("MyLevel", level);

            CurrentLevel = level;
        }

        Unlocknextlevel = PlayerPrefs.GetInt("MyLevel") + 1;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MyCoin"))
        {
            CurrentCoin = PlayerPrefs.GetInt("MyCoin");
            CoinText.text = CurrentCoin.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("MyCoin", MinCoin);
            CurrentCoin = MinCoin;
            CoinText.text = CurrentCoin.ToString();
        }


        int StateMortalLength = FindObjectsOfType<StateMortal>().Length;
        for (int i = 0; i < StateMortalLength; i++)
        {
            AllMortalObjects.Add(FindObjectsOfType<StateMortal>()[i]);
            AllMortalObjects[i].Out.SetActive(false);
            AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().color = OutlineColor;
            AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().sortingOrder = -50;
            AllMortalObjects[i].Out.transform.localScale = OutlineSize;
        }

        thePlayer = FindObjectOfType<Player>();

        if (!LearningLevels)
            CameraMovement._Instance.cam.orthographicSize = mainMenuCameraOrthgraphicSize[CurrentLevel - 1];

        DailyRewardCorotiune = StartCoroutine(DailyRewardStatus());
        MiniGame.Initialize();
    }

    private void Update()
    {
        if (!LearningLevels)
        {
            if (UI_Canvas.GameStarted)
            {
                // CHECK WHAT ARE CURRENT STATE (WIN OR LOSE)
                if (LoseStatus < AllMortalObjects.Count && WinStatus < AllMortalObjects.Count)
                {
                    foreach (StateMortal obj in AllMortalObjects)
                    {
                        if (obj.GetIdentity() != StateMortal.iden.Blue)
                        {
                            LoseStatus++;
                            if (LoseStatus >= AllMortalObjects.Count)
                                return;
                        }
                        else
                        {
                            LoseStatus = 0;
                        }

                        if (obj.GetIdentity() == StateMortal.iden.Blue)
                        {
                            WinStatus++;
                            if (WinStatus >= AllMortalObjects.Count)
                                return;
                        }
                        else if (obj.GetIdentity() == StateMortal.iden.None)
                        {
                            WinStatus++;
                            if (WinStatus >= AllMortalObjects.Count)
                                return;
                        }
                        else
                        {
                            WinStatus = 0;
                        }
                    }
                }

                // IF WE LOSE
                if (!LoseGame && LoseStatus >= AllMortalObjects.Count)
                {
                    LoseGame = true;
                }
                if (LoseGame && !Lose.activeSelf)
                {
                    LoseSound.Play();
                    Game_AudioMixer.DOSetFloat("Lowpass_Music", 650, 1.8f).SetEase(Ease.Linear).SetUpdate(true);
                    Time.timeScale = 0.5f;
                    Lose.SetActive(true);
                    SkillsState = false;
                    UpdateSkills();
                    CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(CoinPosX, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
                }

                // IF WE WIN
                if (WinStatus >= AllMortalObjects.Count && !WinningRewardTaken)
                {
                    WinGame = true;
                    winButton.interactable = false;
                    SkillsState = false;
                    UI_Canvas.GameStarted = false;
                    UpdateSkills();
                }
                if (WinGame && !WinningRewardTaken)
                {
                    SendScoreDataToServer();

                   // StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Level\": " + CurrentLevel + "}"));
                    if (PlayerPrefs.HasKey("UnlockLevel"))
                    {
                        if (PlayerPrefs.GetInt("UnlockLevel") == CurrentLevel)
                        {
                            anim.SetBool("Coin", true);
                            CoinWinning = GoldForEachLevels[CurrentLevel - 1];
                        }
                        else
                        {
                            CoinWinning = 0;
                            EnemySystem.Difficault dif = FindObjectOfType<EnemySystem>().Difficaulty;
                            switch (dif)
                            {
                                case EnemySystem.Difficault.Easy:
                                    CoinWinning = 50;
                                    break;
                                case EnemySystem.Difficault.Meduim:
                                    CoinWinning = 100;
                                    break;
                                case EnemySystem.Difficault.Hard:
                                    CoinWinning = 200;
                                    break;
                            }
                            anim.SetBool("Coin", true);
                        }
                    }
                    else
                    {
                        anim.SetBool("Coin", true);
                    }
                    if (PlayerPrefs.HasKey("ValueOfLevels"))
                    {
                        CheckValueOfLevel = PlayerPrefs.GetInt("ValueOfLevels");
                    }
                    int check = CheckValueOfLevel;
                    CheckValueOfLevel = 1 + CurrentLevel;
                    if (check >= CheckValueOfLevel)
                    {
                        CheckValueOfLevel = check;
                    }
                    if (CheckValueOfLevel >= 200)
                    {
                        CheckValueOfLevel = 200;
                        Unlocknextlevel = CheckValueOfLevel;
                    }
                    PlayerPrefs.SetInt("ValueOfLevels", CheckValueOfLevel);
                    PlayerPrefs.SetInt("MyLevel", Unlocknextlevel);
                    PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("ValueOfLevels"));

                    Game_AudioMixer.DOSetFloat("Lowpass_Music", 650, 1.55f).SetEase(Ease.Linear).SetUpdate(true);

                    Time.timeScale = 0.5f;
                    Win.SetActive(true);
                    WinSound.Play();

                    int b = CoinWinning < 100 ? CoinWinning + Random.Range(0, 5) : CoinWinning < 300 ?
           CoinWinning + Random.Range(-10, 20) : CoinWinning < 1000 ? CoinWinning + Random.Range(-50, 50) : CoinWinning > 1000 ? CoinWinning + Random.Range(-100, 100) : CoinWinning;
                    
                    if(b < 0)
                    {
                        b = Math.Abs(b);
                    }

                    if(b == 0)
                    {
                        b = 50;
                    }

                    CoinWinning = b;

                    CurrentCoin += CoinWinning;
                    UpdateCoin();

                    CoinWiningText.text = CoinWinning.ToString();
                    CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(CoinPosX, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
                    WinningRewardTaken = true;
                }
            }
        }
        else
        {
            if (LoseStatus < AllMortalObjects.Count && WinStatus < AllMortalObjects.Count)
            {
                foreach (StateMortal obj in AllMortalObjects)
                {
                    if (obj.GetIdentity() != StateMortal.iden.Blue)
                    {
                        LoseStatus++;
                        if (LoseStatus >= AllMortalObjects.Count)
                            return;
                    }
                    else
                    {
                        LoseStatus = 0;
                    }

                    if (obj.GetIdentity() == StateMortal.iden.Blue)
                    {
                        WinStatus++;
                        if (WinStatus >= AllMortalObjects.Count)
                            return;
                    }
                    else if (obj.GetIdentity() == StateMortal.iden.None)
                    {
                        WinStatus++;
                        if (WinStatus >= AllMortalObjects.Count)
                            return;
                    }
                    else
                    {
                        WinStatus = 0;
                    }
                }
            }

            if (WinStatus >= AllMortalObjects.Count && !WinningRewardTaken)
            {
                WinGame = true;
            }
            if (WinGame && !WinningRewardTaken)
            {
                WinningRewardTaken = true;

                if (PlayerPrefs.HasKey("LearnProcess"))
                {
                    int LearnProcess = PlayerPrefs.GetInt("LearnProcess");
                    LearnProcess += 1;
                    PlayerPrefs.SetInt("LearnProcess", LearnProcess);



                    if (LearnProcess == TutorialsLevels.Length + 1)
                    {
                        PlayerPrefs.SetInt("MyLevel", 1);
                        //StartCoroutine(
                        //MiniGame.SendScore(
                        //score: LearnProcess - 1,
                        //onSuccess: OnSuccess,
                        //onFail: OnFail
                        //)
                        //);
                        //StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Level\": " + 1 + "}"));
                    }
                   // else
                   // {
                   //     // StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Learn\": " + LearnProcess + "}"));
                   //     StartCoroutine(
                   //MiniGame.SendScore(
                   //score: LearnProcess - 1,
                   //onSuccess: OnSuccess,
                   //onFail: OnFail
                   //)
                   //);
                   // }
                }
                else
                {
                    PlayerPrefs.SetInt("LearnProcess", 2);
                }

                Time.timeScale = 0.5f;
                tutorialTask.LessonSuccesful();
                WinSound.Play();
            }
        }
    }

    public void SendScoreDataToServer()
    {
        ErrorScoreSendingPanel.SetActive(false);
        Loading_dataToServer.SetActive(true);
        Info_dataToServer.SetActive(true);

        StartCoroutine(
                      MiniGame.SendScore(
                      score: CurrentLevel * 10,
                      onSuccess: OnSuccess,
                      onFail: OnFail
                      )
                      );
    }

    private void OnSuccess()
    {
        ErrorScoreSendingPanel.SetActive(false);
        Loading_dataToServer.SetActive(false);
        Info_dataToServer.SetActive(false);
        winButton.interactable = true;
        Debug.Log("Request succeeded.");
    }

    private void OnFail()
    {
        MiniGame.Initialize();
        Loading_dataToServer.SetActive(false);
        Info_dataToServer.SetActive(false);
        ErrorScoreSendingPanel.SetActive(true);
        winButton.interactable = false;
        Debug.Log("Request failed.");
    }

    //private IEnumerator postRequest(string url, string json)
    //{
    //    var uwr = new UnityWebRequest(url, "POST");
    //    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    //    uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
    //    uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

    //    uwr.SetRequestHeader("Content-Type", "application/json");

    //    //Send the request then wait here until it returns
    //    uwr.timeout = 10;
    //    yield return uwr.SendWebRequest();



    //    if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.DataProcessingError
    //        || uwr.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.Log("Error While Sending: " + uwr.error);
    //        OnFail();
    //    }
    //    else if (uwr.result == UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log("Received: " + uwr.result);
    //        Debug.Log("Received: " + uwr.downloadHandler.text);
    //        OnSuccess();
    //    }

    //    uwr.Dispose();
    //}

    public void StartGenerateSolider()
    {
        timer = new WaitForSeconds(1);
        StartCoroutine(GeneratingSolider());
    }

    private IEnumerator GeneratingSolider()
    {
        while (true)
        {
            yield return timer;
            for (int i = 0; i < AllMortalObjects.Count; i++)
            {
                if (!AllMortalObjects[i].isHaveAnySpace)
                {
                    if (AllMortalObjects[i].CurrentCount >= AllMortalObjects[i].MaxSpace)
                    {
                        AllMortalObjects[i].isHaveAnySpace = true;
                        continue;
                    }

                    AllMortalObjects[i].CurrentCount += AllMortalObjects[i].AmountIncrease;

                }
                else
                {
                    if (AllMortalObjects[i].CurrentCount < AllMortalObjects[i].MaxSpace)
                    {
                        AllMortalObjects[i].isHaveAnySpace = false;

                        AllMortalObjects[i].CurrentCount += AllMortalObjects[i].AmountIncrease;
                    }
                }
            }
        }
    }

    public void AddCoin()
    {
        MoneySound.Play();
    }

    public void ResetartLevel()
    {
        DG.Tweening.DOTween.KillAll();

        //PlayerPrefs.SetInt("FastRun", 1);
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);

        ResetGameData();
    }

    public void NextLevel()
    {
        DG.Tweening.DOTween.KillAll();
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        ResetGameData();
    }

    public void RewardGift(int RewardCount)
    {
        CurrentCoin = PlayerPrefs.GetInt("MyCoin");
        CurrentCoin += RewardCount;
        Debug.Log(CurrentCoin);
        PlayerPrefs.SetInt("MyCoin", CurrentCoin);
        UpdateCoin();
    }

    private void OnDestroy()
    {
        if (_Instance != null)
        {
            _Instance = null;
        }
    }

    public void GiftDemo()
    {
        if (PlayerPrefs.HasKey("Last Time Clicked"))
        {
            lastTimeClicked = DateTime.Parse(PlayerPrefs.GetString("Last Time Clicked"));

            currentTime = DateTime.UtcNow;

            TimeSpan difference = currentTime.Subtract(lastTimeClicked);

            if (difference.TotalSeconds >= 300)
            {
                PlayerPrefs.SetString("Last Time Clicked", DateTime.UtcNow.ToString());
                CurrentCoin = PlayerPrefs.GetInt("MyCoin");
                CurrentCoin += 2000;
                RewardExImage.gameObject.SetActive(true);
                MoneySound.Play();
                PlayerPrefs.SetInt("MyCoin", CurrentCoin);
                UpdateCoin();
            }
        }
        else
        {
            PlayerPrefs.SetString("Last Time Clicked", DateTime.UtcNow.ToString());
            CurrentCoin = PlayerPrefs.GetInt("MyCoin");
            CurrentCoin += 2000;
            Debug.Log(CurrentCoin);
            RewardExImage.gameObject.SetActive(true);
            MoneySound.Play();
            PlayerPrefs.SetInt("MyCoin", CurrentCoin);
            UpdateCoin();
        }
    }

    public void UpdateCoin()
    {
        CoinText.text = CurrentCoin.ToString();
        PlayerPrefs.SetInt("MyCoin", CurrentCoin);
        if (PlayerPrefs.GetInt("MyCoin") >= MaxCoin)
        {
            CurrentCoin = MaxCoin;
            PlayerPrefs.SetInt("MyCoin", CurrentCoin);
            CoinText.text = CurrentCoin.ToString();
        }
    }

    public void UpdateSkills()
    {
        if (!SkillsState)
        {
            for (int i = 0; i < SkillsObject.Length; i++)
            {
                SkillsObject[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < SkillsObject.Length; i++)
            {
                SkillsObject[i].SetActive(true);
            }
        }
    }

    public void InitializeAttack(StateMortal MyBlue)
    {
        for (int i = 0; i < AllMortalObjects.Count; i++)
        {
            if (AllMortalObjects[i] == MyBlue)
            {
                AllMortalObjects[i].ShowTypeOfAttack();
                AllMortalObjects[i].Attack_N(AllMortalObjects, MyBlue);
            }
        }
    }

    public void DeserilaizeAttack()
    {
        for (int i = 0; i < AllMortalObjects.Count; i++)
        {
            AllMortalObjects[i].HideTypeOfAttack();
        }
    }

    private IEnumerator DailyRewardStatus()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            if (PlayerPrefs.HasKey("Last Time Clicked"))
            {
                lastTimeClicked = DateTime.Parse(PlayerPrefs.GetString("Last Time Clicked"));

                currentTime = DateTime.UtcNow;

                TimeSpan difference = currentTime.Subtract(lastTimeClicked);

                float currentProgress = (float)difference.TotalSeconds;

                if (currentProgress >= 300)
                {
                    currentProgress = 300;

                    RewardExImage.gameObject.SetActive(false);
                    DailyRewardText.text = "";
                    DailyRewardImage.fillAmount = 0;
                    continue;
                }

                float TimeSpend = scaleValue(0, 300, 0, 1, currentProgress);

                float revFill = 1 - TimeSpend;

                DailyRewardImage.fillAmount = revFill;

                float RevProgress = 300 - currentProgress;

                var ts = TimeSpan.FromSeconds(RevProgress);

                DailyRewardText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            }
            else
            {
                RewardExImage.gameObject.SetActive(false);
                DailyRewardImage.fillAmount = 0;
                DailyRewardText.text = "";
            }
        }
    }

    public float scaleValue(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public void ResetGameData()
    {
        #region Reset Managers
        Time.timeScale = 0;
        UI_Canvas.GameStarted = false;
        StopAllCoroutines();
        LearningLevels = false;
        DestroyImmediate(thePlayer.gameObject);
        WinGame = false;
        LoseGame = false;
        WinStatus = 0;
        LoseStatus = 0;
        SkillsState = false;
        UpdateSkills();
        UpdateCoin();
        AllMortalObjects.Clear();
        Lose.SetActive(false);
        Win.SetActive(false);
        IsCoinTaken = false;
        WinningRewardTaken = false;
        DG.Tweening.DOTween.KillAll();
        BurnArmy.Clear();
        CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
        #endregion

        #region Reset UI
        UI_Canvas.CoverMenu.SetActive(true);
        UI_Canvas.SoundSetting.gameObject.SetActive(true);
        UI_Canvas.ColorPicker_UI.gameObject.SetActive(true);
        UI_Canvas.MenuPanel.SetActive(true);
        UI_Canvas.ButtonStart.SetActive(true);
        UI_Canvas.Pause.SetActive(false);
        UI_Canvas.RestartLevel.SetActive(false);
        UI_Canvas.Home.SetActive(false);
        UI_Canvas.Pause.GetComponent<Image>().sprite = UI_Canvas.PauseOff;
        UI_Canvas.TouchScreen.SetActive(false);
        UI_Canvas.PauseStatus = false;
        UI_Canvas.LevelShowInLevel.gameObject.SetActive(false);
        #endregion

        System.GC.Collect();
        Resources.UnloadUnusedAssets();

        foreach (var item in SkillsObject)
        {
            item.transform.GetChild(0).GetComponent<MaskAnimate>().animate();
        }

        if (!PlayerPrefs.HasKey("MyLevel"))
        {
            LearningLevels = true;
            UI_Canvas.gameObject.SetActive(false);
            if (PlayerPrefs.HasKey("LearnProcess"))
            {
                int LearnProcess = PlayerPrefs.GetInt("LearnProcess");
                Instantiate(TutorialsLevels[LearnProcess - 1].gameObject, LevelsParent.transform, false);
                Instantiate(TutorialsCanvas[LearnProcess - 1].gameObject);

                StartGenerateSolider();
                tutorialTask = FindObjectOfType<TutorialTask>();
            }
            else
            {
                Instantiate(TutorialsLevels[0].gameObject, LevelsParent.transform, false);
                Instantiate(TutorialsCanvas[0].gameObject);

                // StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Learn\": " + 1 + "}"));


                StartGenerateSolider();
                tutorialTask = FindObjectOfType<TutorialTask>();
                PlayerPrefs.SetInt("LearnProcess", 1);
            }
            Time.timeScale = 1;
            return;
        }

        int level = 1;

        if (PlayerPrefs.HasKey("MyLevel"))
        {
            level = PlayerPrefs.GetInt("MyLevel");

            GameObject obj = Instantiate(HandCraftedLevels[level - 1].gameObject, LevelsParent.transform, false);
            CurrentLevel = level;
        }
        else
        {
            PlayerPrefs.SetInt("MyLevel", level);

            CurrentLevel = level;
        }

        Unlocknextlevel = PlayerPrefs.GetInt("MyLevel") + 1;

        int StateMortalLength = FindObjectsOfType<StateMortal>().Length;
        for (int i = 0; i < StateMortalLength; i++)
        {
            AllMortalObjects.Add(FindObjectsOfType<StateMortal>()[i]);
            AllMortalObjects[i].Out.SetActive(false);
            AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().color = OutlineColor;
            AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().sortingOrder = -50;
            AllMortalObjects[i].Out.transform.localScale = OutlineSize;
        }

        thePlayer = FindObjectOfType<Player>();

        CameraMovement._Instance.cam.transform.position = new Vector3(0, 0, -10);

        if (!LearningLevels)
            CameraMovement._Instance.cam.orthographicSize = mainMenuCameraOrthgraphicSize[CurrentLevel - 1];

        DailyRewardCorotiune = StartCoroutine(DailyRewardStatus());

        UI_Canvas.ResetSpecialData();
    }

    #endregion
}
