using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using TMPro;
//using Cafebazaar;
using System.Linq;

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

    //[ColorUsage(true, true)]
    //public Color OutlineColor;

    //public Vector3 OutlineSize;

    private DateTime currentTime;
    private DateTime lastTimeClicked;

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

    private WaitForSeconds scoreDelay;

    public Image WhiteScore, BlueScore, RedScore, YellowScore, PinkScore, GreenScore, OrangeScore, PurpleScore, OutlineScore;
    private EnemySystem enemySystem;

    private Dictionary<string, float> ColorsPercentage;
    int total;


    public Color LineEffectCMColor, LineEffectMissColor;

    private LineRenderer lr;

    public GameObject OutOutlinePrefab;
    public Sprite Status_Frame_Light;

    public RectTransform WinningCoin;

    public GameObject ControlArea;

    public HandOperation handOperaion;

    public bool AllowVibration;

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

            Instantiate(HandCraftedLevels[level - 1].gameObject, LevelsParent.transform, false);
            CurrentLevel = level;
        }
        else if (!PlayerPrefs.HasKey("MyLevel") && PlayerPrefs.GetInt("LearnProcess") >= 2)
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

            //AllMortalObjects[i].Out.SetActive(false);
            // AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().color = OutlineColor;
            //AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().sortingOrder = -50;
           // AllMortalObjects[i].Fx.GetComponent<Image>().color = new Color32(0, 0, 0, 200);
            //AllMortalObjects[i].Out.transform.localScale = OutlineSize;
        }

        thePlayer = FindObjectOfType<Player>();

        Game_AudioMixer.DOSetFloat("Lowpass_Music", 300, 0).SetEase(Ease.Linear).SetUpdate(true);

        float ExtraOrthSize = 0.5f;

        if (AllMortalObjects.Count < 8)
            ExtraOrthSize = 0;

        if (!LearningLevels)
            CameraMovement._Instance.cam.orthographicSize = mainMenuCameraOrthgraphicSize[CurrentLevel - 1] + ExtraOrthSize;
        else
        {
            CameraMovement._Instance.cam.orthographicSize = 4;
        }

        DailyRewardCorotiune = StartCoroutine(DailyRewardStatus());
        //MiniGame.Initialize();
        enemySystem = FindObjectOfType<EnemySystem>();
        scoreDelay = new WaitForSeconds(0.25f);
        ColorsPercentage = new Dictionary<string, float>();
        lr = GameObject.FindWithTag("LineRenderer").GetComponent<LineRenderer>();

        handOperaion = FindObjectOfType<HandOperation>();
        if (handOperaion != null)
        {
            handOperaion.gameObject.SetActive(false);
        }
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
                    Game_AudioMixer.DOSetFloat("Lowpass_Music", 300, 1.8f).SetEase(Ease.Linear).SetUpdate(true);
                    Time.timeScale = 0.5f;
                    StartCoroutine(OpenPanel(Lose,false));
                    SkillsState = false;
                    UpdateSkills();
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
                            //anim.SetBool("Coin", true);
                            int b = CurrentLevel - 1;
                            if (b <= 0) b = 0;
                            CoinWinning = GoldForEachLevels[b];
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
                            //anim.SetBool("Coin", true);
                        }
                    }
                    else
                    {
                        int b = CurrentLevel - 1;
                        if (b <= 0) b = 0;
                        CoinWinning = GoldForEachLevels[b];
                        //anim.SetBool("Coin", true);
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

                    Game_AudioMixer.DOSetFloat("Lowpass_Music", 300, 1.55f).SetEase(Ease.Linear).SetUpdate(true);

                    Time.timeScale = 0.5f;
                    
                    StartCoroutine(OpenPanel(Win,true));

                    

                    CoinWiningText.text = "+" + CoinWinning.ToString();
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

        //StartCoroutine(
        //              MiniGame.SendScore(
        //              score: CurrentLevel * 10,
        //              onSuccess: OnSuccess,
        //              onFail: OnFail
        //              )
        //              );
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
        //MiniGame.Initialize();
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

    private IEnumerator OpenPanel(GameObject obj, bool gameResult)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        if (gameResult)
        {
            WinSound.Play();
            CurrentCoin += CoinWinning;
            UpdateCoin();
        }
        else
        {
            LoseSound.Play();
        }
        obj.SetActive(true);
        obj.transform.GetChild(1).DOScale(1, 0.25f).SetEase(Ease.Linear);
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
                WinningCoin.gameObject.SetActive(true);
                CurrentCoin += 2000;
                MoneySound.Play();
                WinningCoin.DOScale(1.2f, 1).SetEase(Ease.Linear).SetUpdate(true).SetDelay(0.75f);
                WinningCoin.DOAnchorPosY(700f, 1).SetEase(Ease.Flash).SetUpdate(true).SetDelay(1f).OnComplete(() =>
                {
                    WinningCoin.gameObject.SetActive(false);
                    WinningCoin.DOAnchorPosY(-202, 0f).SetEase(Ease.Linear).SetUpdate(true);
                    WinningCoin.DOScale(1, 0).SetEase(Ease.Linear).SetUpdate(true);
                });
                PlayerPrefs.SetInt("MyCoin", CurrentCoin);
                UpdateCoin();
            }
        }
        else
        {
            PlayerPrefs.SetString("Last Time Clicked", DateTime.UtcNow.ToString());
            CurrentCoin = PlayerPrefs.GetInt("MyCoin");
            CurrentCoin += 2000;
            MoneySound.Play();
            WinningCoin.gameObject.SetActive(true);
            WinningCoin.DOScale(1.2f, 1).SetEase(Ease.Linear).SetUpdate(true).SetDelay(0.75f);
            WinningCoin.DOAnchorPosY(700f, 1).SetEase(Ease.Flash).SetUpdate(true).SetDelay(1f).OnComplete(() =>
            {
                WinningCoin.gameObject.SetActive(false);
                WinningCoin.DOAnchorPosY(-202f, 0).SetEase(Ease.Linear).SetUpdate(true);
                WinningCoin.DOScale(1, 0).SetEase(Ease.Linear).SetUpdate(true);
            });
            PlayerPrefs.SetInt("MyCoin", CurrentCoin);
            UpdateCoin();
        }
    }

    public void UpdateCoin()
    {
        CoinText.DOText(CurrentCoin.ToString(), 1f, true,ScrambleMode.Numerals).SetUpdate(true);
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

    public void UpdateColors()
    {
        for(int i = 0; i < AllMortalObjects.Count; i++)
        {
            AllMortalObjects[i].CheckColorOrder();
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

    public IEnumerator MultiSliderScore()
    {
        float blueCount;
        float whiteCount;
        float redCount;
        float yellowCount;
        float pinkCount;
        float greenCount;
        float orangeCount;
        float purpleCount;

        total = AllMortalObjects.Count;

        float totalPercentageFilled = 0;
        int layerIndex = 0;
        float Temp = 0;
        string key = null;


        BlueScore.gameObject.SetActive(true);
        OutlineScore.gameObject.SetActive(true);

        for (int i = 0; i < AllMortalObjects.Count; i++)
        {
            if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.None)
            {
                WhiteScore.gameObject.SetActive(true);
                break;
            }
        }

        if (enemySystem.Red)
        {
            RedScore.gameObject.SetActive(true);
        }
        if (enemySystem.Yellow)
        {
            YellowScore.gameObject.SetActive(true);
        }
        if (enemySystem.Pink)
        {
            PinkScore.gameObject.SetActive(true);
        }
        if (enemySystem.Green)
        {
            GreenScore.gameObject.SetActive(true);
        }
        if (enemySystem.Orange)
        {
            OrangeScore.gameObject.SetActive(true);
        }
        if (enemySystem.Purple)
        {
            PurpleScore.gameObject.SetActive(true);
        }

        while (true)
        {
            yield return scoreDelay;

            #region What We Have Here?

            ColorsPercentage.Clear();
            blueCount = 0;
            whiteCount = 0;
            redCount = 0;
            yellowCount = 0;
            pinkCount = 0;
            greenCount = 0;
            orangeCount = 0;
            purpleCount = 0;

            totalPercentageFilled = 0;
            for (int i = 0; i < AllMortalObjects.Count; i++)
            {
                if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Blue)
                {
                    blueCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.None)
                {
                    whiteCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Red)
                {
                    redCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Yellow)
                {
                    yellowCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Pink)
                {
                    pinkCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Green)
                {
                    greenCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Orange)
                {
                    orangeCount++;
                }
                else if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.LastColor)
                {
                    purpleCount++;
                }
            }

            //print("=========");
            //print("Blue : " + blueCount);
            //print("White : " + whiteCount);
            //print("Yellow : " + yellowCount);
            //print("Red : " + redCount);
            //print("=========");

            if (blueCount == 0)
            {
                BlueScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Blue", blueCount / total * 100); // 0% ~ 100%
            }
            if (whiteCount == 0)
            {
                WhiteScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("White", whiteCount / total * 100);
            }
            if (redCount == 0)
            {
                RedScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Red", redCount / total * 100);
            }
            if (yellowCount == 0)
            {
                YellowScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Yellow", yellowCount / total * 100);
            }
            if (pinkCount == 0)
            {
                PinkScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Pink", pinkCount / total * 100);
            }
            if (greenCount == 0)
            {
                GreenScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Green", greenCount / total * 100);
            }
            if (orangeCount == 0)
            {
                OrangeScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Orange", orangeCount / total * 100);
            }
            if (purpleCount == 0)
            {
                PurpleScore.gameObject.SetActive(false);
            }
            else
            {
                ColorsPercentage.Add("Purple", purpleCount / total * 100);
            }
            #endregion

            layerIndex = 0;

            while (ColorsPercentage.Count > 0)
            {
                Temp = 0;
                key = null;

                Temp = Mathf.Max(ColorsPercentage.Values.ToArray());

                foreach (var item in ColorsPercentage)
                {
                    if (item.Value == Temp)
                    {
                        key = item.Key;
                    }
                }

                totalPercentageFilled += Temp / 100;

                if (totalPercentageFilled >= 1)
                {
                    totalPercentageFilled = 1;
                }

                layerIndex++;


                //print("--------------------");
                //print("Key: " + key);
                //print("temp: " + Temp);
                //print("TotalPercentage: " + totalPercentageFilled);
                //print("ColorsPercentage Count: " + ColorsPercentage.Count);
                //print("Total : " + total);


                switch (key)
                {
                    case "Blue":
                        BlueScore.fillAmount = totalPercentageFilled;
                        BlueScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (BlueScore.GetComponent<Canvas>().sortingOrder <= 0)
                        {
                            BlueScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "White":
                        WhiteScore.fillAmount = totalPercentageFilled;
                        WhiteScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (WhiteScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            WhiteScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "Red":
                        RedScore.fillAmount = totalPercentageFilled;
                        RedScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (RedScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            RedScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "Yellow":
                        YellowScore.fillAmount = totalPercentageFilled;
                        YellowScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (YellowScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            YellowScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "Pink":
                        PinkScore.fillAmount = totalPercentageFilled;
                        PinkScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (PinkScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            PinkScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "Green":
                        GreenScore.fillAmount = totalPercentageFilled;
                        GreenScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (GreenScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            GreenScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "Orange":
                        OrangeScore.fillAmount = totalPercentageFilled;
                        OrangeScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (OrangeScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            OrangeScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                    case "Purple":
                        PurpleScore.fillAmount = totalPercentageFilled;
                        PurpleScore.GetComponent<Canvas>().sortingOrder = (int)Temp - layerIndex;
                        if (PurpleScore.GetComponent<Canvas>().sortingOrder < 0)
                        {
                            PurpleScore.GetComponent<Canvas>().sortingOrder = 0;
                        }
                        ColorsPercentage.Remove(key);
                        break;
                }
            }
        }
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
        lr.positionCount = 0;
        BurnArmy.Clear();


        WhiteScore.gameObject.SetActive(false);
        BlueScore.gameObject.SetActive(false);
        RedScore.gameObject.SetActive(false);
        YellowScore.gameObject.SetActive(false);
        OutlineScore.gameObject.SetActive(false);


        #endregion

        #region Reset UI
        UI_Canvas.CoverMenu.SetActive(true);
        UI_Canvas.SoundSetting.gameObject.SetActive(true);
        UI_Canvas.MenuPanel.SetActive(true);
        UI_Canvas.ButtonStart.SetActive(true);
        UI_Canvas.TouchScreen.SetActive(false);
        UI_Canvas.backButton.SetActive(false);
        UI_Canvas.BackPanel.SetActive(false);
        UI_Canvas.CameraLockImage.gameObject.SetActive(false);
        UI_Canvas.PauseStatus = false;

        UI_Canvas.ScoreCorotuine = null;
        #endregion

        System.GC.Collect();
        Resources.UnloadUnusedAssets();

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
        else if(!PlayerPrefs.HasKey("MyLevel") && PlayerPrefs.GetInt("LearnProcess") >= 2)
        {
            PlayerPrefs.SetInt("MyLevel", level);

            CurrentLevel = level;
        }

        Unlocknextlevel = PlayerPrefs.GetInt("MyLevel") + 1;

        int StateMortalLength = FindObjectsOfType<StateMortal>().Length;
        for (int i = 0; i < StateMortalLength; i++)
        {
            AllMortalObjects.Add(FindObjectsOfType<StateMortal>()[i]);

           // Image outline = Instantiate(OutOutlinePrefab, AllMortalObjects[i].transform).GetComponent<Image>();
           // outline.pixelsPerUnitMultiplier = AllMortalObjects[i].GetComponent<Image>().pixelsPerUnitMultiplier;

            //AllMortalObjects[i].Out.SetActive(false);
            // AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().color = OutlineColor;
            // AllMortalObjects[i].Out.GetComponent<SpriteRenderer>().sortingOrder = -50;
           // AllMortalObjects[i].Fx.GetComponent<Image>().color = new Color32(0, 0, 0, 200);
            //AllMortalObjects[i].Out.transform.localScale = OutlineSize;
        }

        thePlayer = FindObjectOfType<Player>();   

        CameraMovement._Instance.cam.transform.position = new Vector3(0, 0, -10);

        if (!LearningLevels)
            CameraMovement._Instance.cam.orthographicSize = mainMenuCameraOrthgraphicSize[CurrentLevel - 1];

        Game_AudioMixer.DOSetFloat("Lowpass_Music", 300, 0).SetEase(Ease.Linear).SetUpdate(true);

        DailyRewardCorotiune = StartCoroutine(DailyRewardStatus());
        enemySystem = FindObjectOfType<EnemySystem>();
        UI_Canvas.ResetSpecialData();

        int currentColor = PlayerPrefs.GetInt("Color_Pick");
        ColorPicker._Instance.SetColor(currentColor);

        handOperaion = FindObjectOfType<HandOperation>();
        if (handOperaion != null)
        {
            handOperaion.gameObject.SetActive(false);
        }
    }

    #endregion
}
