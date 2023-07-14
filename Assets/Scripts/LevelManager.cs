using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using CodeStage.AntiCheat.ObscuredTypes;

public class LevelManager : MonoBehaviour
{
    #region Properties
    public GameObject Win, Lose;
    [HideInInspector] public ObscuredInt CurrentCoin;
    private ObscuredInt MaxCoin, MinCoin;
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

    private bool CoinRecive;
    public float CoinPosX = -180f;
    private int WinStatus;
    private int LoseStatus;
    Identity[] AllMortalObjects;
    private bool WinGame, LoseGame;

    private bool IsCoinTaken = false;

    public static LevelManager _Instance { get; private set; }

    #endregion

    #region Functions

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }


        MaxCoin = 999999;
        MinCoin = 500;
        Unlocknextlevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            PlayerPrefs.SetInt("LearnEnd", 1);
        }
    }

    private void Start()
    {
        if (ObscuredPrefs.HasKey("MyCoin"))
        {
            CurrentCoin = ObscuredPrefs.GetInt("MyCoin");
            ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
            CoinText.text = CurrentCoin.ToString();
        }
        else
        {
            ObscuredPrefs.SetInt("MyCoin", MinCoin);
            CurrentCoin = MinCoin;
            CoinText.text = CurrentCoin.ToString();
        }



        AllMortalObjects = new Identity[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<Identity>()[i];
        }
    }

    private void Update()
    {
        CoinText.text = CurrentCoin.ToString();
        if (ObscuredPrefs.GetInt("MyCoin") >= MaxCoin)
        {
            CurrentCoin = MaxCoin;
            ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
            CoinText.text = CurrentCoin.ToString();
        }

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


        // CHECK WHAT ARE CURRENT STATE (WIN OR LOSE)
        if (LoseStatus < AllMortalObjects.Length && WinStatus < AllMortalObjects.Length)
        {
            foreach (Identity obj in AllMortalObjects)
            {
                if (obj.GetIdentity() != Identity.iden.Red)
                {
                    LoseStatus++;
                    if (LoseStatus >= AllMortalObjects.Length)
                        return;
                }
                else
                {
                    LoseStatus = 0;
                }

                if (obj.GetIdentity() == Identity.iden.Red)
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
        if (LoseStatus >= AllMortalObjects.Length)
        {
            LoseGame = true;
        }
        if (LoseGame)
        {
            Time.timeScale = 0f;
            Lose.SetActive(true);
            SkillsState = false;
            for (int i = 0; i < SkillsObject.Length; i++)
            {
                SkillsObject[i].gameObject.SetActive(false);
            }
            CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(CoinPosX, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
        }


        // IF WE WIN
        if (WinStatus >= AllMortalObjects.Length)
        {
            WinGame = true;
            SkillsState = false;
            for (int i = 0; i < SkillsObject.Length; i++)
            {
                SkillsObject[i].gameObject.SetActive(false);
            }
        }
        if (WinGame)
        {
            if (PlayerPrefs.HasKey("UnlockLevel"))
            {
                if (PlayerPrefs.GetInt("UnlockLevel") == SceneManager.GetActiveScene().buildIndex)
                {
                    anim.SetTrigger("WinCoin");
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
            CheckValueOfLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (check >= CheckValueOfLevel)
            {
                CheckValueOfLevel = check;
            }
            if (CheckValueOfLevel >= 100)
            {
                CheckValueOfLevel = 100;
            }
            PlayerPrefs.SetInt("ValueOfLevels", CheckValueOfLevel);
            PlayerPrefs.SetInt("MyLevel", Unlocknextlevel);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("ValueOfLevels"));

            Time.timeScale = 0f;
            if (SceneManager.GetActiveScene().buildIndex != 100)
            {
                Win.SetActive(true);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 100 && !CoinRecive)
            {
                //CoinWinning = MaxCoin;
                CurrentCoin += CoinWinning;
                ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
                //StartCoroutine(CloseLast());
                CoinRecive = true;
                return;
            }

            if (!CoinRecive)
            {
                int b = CoinWinning < 100 ? CoinWinning - Random.Range(-3, 5) : CoinWinning < 300 ?
       CoinWinning - Random.Range(-30, 30) : CoinWinning < 1000 ? CoinWinning - Random.Range(-50, 50) : CoinWinning > 1000 ? CoinWinning - Random.Range(-100, 100) : CoinWinning;
                CoinWinning = b;
                WinSound.Play();
                if (IsCoinTaken)
                {
                    CurrentCoin += CoinWinning;
                    ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
                }
                CoinRecive = true;
            }
            CoinWiningText.text = CoinWinning.ToString();
            CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(CoinPosX, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    public void AddCoin()
    {
        MoneySound.Play();
    }

    public void ResetartLevel()
    {
        DG.Tweening.DOTween.KillAll();
        PlayerPrefs.SetInt("FastRun", 1);
        PlayerPrefs.SetInt("Restarted", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        DG.Tweening.DOTween.KillAll();
        if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RewardGift(int RewardCount)
    {
        CurrentCoin = ObscuredPrefs.GetInt("MyCoin");
        CurrentCoin += RewardCount;
        Debug.Log(CurrentCoin);
        ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
        CoinText.text = CurrentCoin.ToString();
    }

    private void OnDestroy()
    {
        if (_Instance != null)
        {
            _Instance = null;
        }
    }

    #endregion
}
