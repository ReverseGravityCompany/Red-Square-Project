using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Audio;

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

    public int CoinWinning;
    public Text CoinWiningText;
    public Animator anim;

    public AudioSource NextMatchSound;
    public AudioSource WinSound;
    public AudioSource MoneySound;
    public AudioSource LoseSound;
    public AudioSource SelectMortalSound, AttackNoneColorSound, AttackRedColorSound, AttackYellowColorSound, AttackSound;
    public AudioMixer Game_AudioMixer;


    private bool CoinRecive;
    public float CoinPosX = -180f;
    private int WinStatus;
    private int LoseStatus;
    Identity[] AllMortalObjects;
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
    public GameObject UI_Canvas;
    private TutorialTask tutorialTask;

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

                GameObject.FindObjectOfType<SquareSoliderCount>().StartGenerateSolider();
                tutorialTask = FindObjectOfType<TutorialTask>();
            }
            else
            {
                Instantiate(TutorialsLevels[0].gameObject, LevelsParent.transform, false);
                Instantiate(TutorialsCanvas[0].gameObject);

                // StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Learn\": " + 1 + "}"));


                GameObject.FindObjectOfType<SquareSoliderCount>().StartGenerateSolider();
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
            PlayerPrefs.SetInt("MyCoin", CurrentCoin);
            CoinText.text = CurrentCoin.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("MyCoin", MinCoin);
            CurrentCoin = MinCoin;
            CoinText.text = CurrentCoin.ToString();
        }


        AllMortalObjects = new Identity[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<Identity>()[i];
        }

       // MiniGame.Initialize();
    }

    private void Update()
    {
        if (!LearningLevels)
        {
            // CHECK WHAT ARE CURRENT STATE (WIN OR LOSE)
            if (LoseStatus < AllMortalObjects.Length && WinStatus < AllMortalObjects.Length)
            {
                foreach (Identity obj in AllMortalObjects)
                {
                    if (obj.GetIdentity() != Identity.iden.Blue)
                    {
                        LoseStatus++;
                        if (LoseStatus >= AllMortalObjects.Length)
                            return;
                    }
                    else
                    {
                        LoseStatus = 0;
                    }

                    if (obj.GetIdentity() == Identity.iden.Blue)
                    {
                        WinStatus++;
                        if (WinStatus >= AllMortalObjects.Length)
                            return;
                    }
                    else if (obj.GetIdentity() == Identity.iden.None)
                    {
                        WinStatus++;
                        if (WinStatus >= AllMortalObjects.Length)
                            return;
                    }
                    else
                    {
                        WinStatus = 0;
                    }
                }
            }

            // IF WE LOSE
            if (!LoseGame && LoseStatus >= AllMortalObjects.Length)
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
            if (WinStatus >= AllMortalObjects.Length && !WinningRewardTaken)
            {
                WinGame = true;
                SkillsState = false;
                FindObjectOfType<MenuSetting>().GameStarted = false;
                UpdateSkills();
            }
            if (WinGame && !WinningRewardTaken)
            {
                //StartCoroutine(
                //  MiniGame.SendScore(
                //  score: 4 + CurrentLevel,
                //  onSuccess: OnSuccess,
                //  onFail: OnFail
                //  )
                //  );

                if (PlayerPrefs.HasKey("UnlockLevel"))
                {
                    if (PlayerPrefs.GetInt("UnlockLevel") == CurrentLevel)
                    {
                        anim.SetTrigger("WinCoin");
                        CoinWinning = GoldForEachLevels[CurrentLevel - 1];
                        IsCoinTaken = true;
                    }
                    else
                    {
                        anim.SetTrigger("WinCoin");
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
                        IsCoinTaken = true;
                    }
                }
                else
                {
                    anim.SetTrigger("WinCoin");
                    IsCoinTaken = true;
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

                if (!CoinRecive)
                {
                    int b = CoinWinning < 100 ? CoinWinning - Random.Range(-12, 12) : CoinWinning < 300 ?
           CoinWinning - Random.Range(-40, 30) : CoinWinning < 1000 ? CoinWinning - Random.Range(-80, 50) : CoinWinning > 1000 ? CoinWinning - Random.Range(-3000, 100) : CoinWinning;
                    CoinWinning = b;

                    if (IsCoinTaken)
                    {
                        CurrentCoin += CoinWinning;
                        UpdateCoin();
                        PlayerPrefs.SetInt("MyCoin", CurrentCoin);
                    }
                    CoinRecive = true;
                }
                CoinWiningText.text = CoinWinning.ToString();
                CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(CoinPosX, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
                WinningRewardTaken = true;
            }
        }
        else
        {
            if (LoseStatus < AllMortalObjects.Length && WinStatus < AllMortalObjects.Length)
            {
                foreach (Identity obj in AllMortalObjects)
                {
                    if (obj.GetIdentity() != Identity.iden.Blue)
                    {
                        LoseStatus++;
                        if (LoseStatus >= AllMortalObjects.Length)
                            return;
                    }
                    else
                    {
                        LoseStatus = 0;
                    }

                    if (obj.GetIdentity() == Identity.iden.Blue)
                    {
                        WinStatus++;
                        if (WinStatus >= AllMortalObjects.Length)
                            return;
                    }
                    else if (obj.GetIdentity() == Identity.iden.None)
                    {
                        WinStatus++;
                        if (WinStatus >= AllMortalObjects.Length)
                            return;
                    }
                    else
                    {
                        WinStatus = 0;
                    }
                }
            }

            if (WinStatus >= AllMortalObjects.Length && !WinningRewardTaken)
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
                  //      StartCoroutine(
                  //MiniGame.SendScore(
                  //score: LearnProcess - 1,
                  //onSuccess: OnSuccess,
                  //onFail: OnFail
                  //)
                  //);
                        //StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Level\": " + 1 + "}"));
                    }
                    else
                    {
                        // StartCoroutine(postRequest(BazzarAPIURL, "{\r\n    \"game_slug\": \"SharifGame-Block-Brawl-Fast-Reaction\",\r\n    \"uid\": \"ec96a9add993bcb8422e85dc5c2b57581a60c329\",\r\n    \"Learn\": " + LearnProcess + "}"));
                   //     StartCoroutine(
                   //MiniGame.SendScore(
                   //score: LearnProcess - 1,
                   //onSuccess: OnSuccess,
                   //onFail: OnFail
                   //)
                   //);
                    }
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

    private void OnSuccess()
    {
        Debug.Log("Request succeeded.");
    }

    private void OnFail()
    {
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
    //    uwr.timeout = 3;
    //    yield return uwr.SendWebRequest();



    //    if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.DataProcessingError
    //        || uwr.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.Log("Error While Sending: " + uwr.error);
    //    }
    //    else if (uwr.result == UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log("Received: " + uwr.result);
    //        Debug.Log("Received: " + uwr.downloadHandler.text);
    //    }

    //    uwr.Dispose();
    //}

    public void AddCoin()
    {
        MoneySound.Play();
    }

    public void ResetartLevel()
    {
        DG.Tweening.DOTween.KillAll();

        PlayerPrefs.SetInt("FastRun", 1);
        PlayerPrefs.SetInt("MyLevel", CurrentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        DG.Tweening.DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        CurrentCoin = PlayerPrefs.GetInt("MyCoin");
        CurrentCoin += 2000;
        Debug.Log(CurrentCoin);
        PlayerPrefs.SetInt("MyCoin", CurrentCoin);
        UpdateCoin();
    }

    public void UpdateCoin()
    {
        CoinText.text = CurrentCoin.ToString();
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

    #endregion
}
