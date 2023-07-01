using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using CodeStage.AntiCheat.ObscuredTypes;

public class LevelManager : MonoBehaviour
{
    public GameObject Win, Lose , Last , again , WinLast;
    private AllMortal allMortal;
    public ObscuredInt CurrentCoin;
    private ObscuredInt MaxCoin, MinCoin;
    public Text CoinText;

    int Unlocknextlevel;
    int CheckValueOfLevel;
    public bool SkillsOn;
    public GameObject[] SkillsObject;
    //[SerializeField] Animator animTimer;
    //[SerializeField] float TimeForward;

    public int CoinWinning;
    public Text CoinWiningShow;
    public Animator anim;
    private bool runOneTime;

    public AudioSource NextMatchSound;
    public AudioSource WinSound;
    public AudioSource MoneySound;
    private bool OnRun;
    public float Offset;
    public int CheckWin;
    public int CheckLose;
    GameObject[] AllMortalObjects;
    public bool WinGame, LoseGame;
    [HideInInspector]public bool CatchCoin;
    private Skills skills;

    private bool CoinIsGone = false;
    
   
    private void Awake()
    {
        MaxCoin = 999999;
        MinCoin = 500;
        Unlocknextlevel = SceneManager.GetActiveScene().buildIndex + 1;
        allMortal = FindObjectOfType<AllMortal>();
        if(SceneManager.GetActiveScene().name == "Level 1"){
            PlayerPrefs.SetInt("LearnEnd",1);
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
            ObscuredPrefs.SetInt("MyCoin",MinCoin);
            CurrentCoin = MinCoin;
            CoinText.text = CurrentCoin.ToString();
        }



        AllMortalObjects = new GameObject[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<Identity>()[i].gameObject;
        }

        Offset = -180f;

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

        if (!SkillsOn)
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



        if (CheckLose < AllMortalObjects.Length && CheckWin < AllMortalObjects.Length)
        {
            foreach (GameObject go in AllMortalObjects)
            {
                if (go.GetComponent<Identity>().GetIden() != Identity.iden.Red)
                {
                    CheckLose++;
                    if (CheckLose >= AllMortalObjects.Length)
                        return;
                }
                else
                {
                    CheckLose = 0;
                }

                if (go.GetComponent<Identity>().GetIden() == Identity.iden.Red)
                {
                    CheckWin++;
                    if (CheckWin >= AllMortalObjects.Length)
                        return;
                }
                else if (go.GetComponent<Identity>().GetIden() == Identity.iden.None)
                {
                    CheckWin++;
                    if (CheckWin >= AllMortalObjects.Length)
                        return;
                }
                else
                {
                    CheckWin = 0;
                }
            }
        }






        if (CheckLose >= AllMortalObjects.Length)
        {
            LoseGame = true;
        }

        if (LoseGame)
        {
            Time.timeScale = 0f;
            Lose.SetActive(true);
            SkillsOn = false;
            for (int i = 0; i < SkillsObject.Length; i++)
            {
                SkillsObject[i].gameObject.SetActive(false);
            }
            CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(Offset, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
        }



        if (CheckWin >= AllMortalObjects.Length)
        {
            WinGame = true;
            SkillsOn = false;
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
                    CoinIsGone = true;
                }
            }
            else{
                anim.SetTrigger("WinCoin");
                    CoinIsGone = true;
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
            if(CheckValueOfLevel >= 100)
            {
                CheckValueOfLevel = 100;
            }
            PlayerPrefs.SetInt("ValueOfLevels", CheckValueOfLevel);
            PlayerPrefs.SetInt("MyLevel", Unlocknextlevel);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("ValueOfLevels"));

            Time.timeScale = 0f;
            if(SceneManager.GetActiveScene().buildIndex != 100)
            {
                Win.SetActive(true);
            }
            else if(SceneManager.GetActiveScene().buildIndex == 100 && !OnRun)
            {
               CoinWinning = MaxCoin;
               CurrentCoin += CoinWinning;
               ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
               StartCoroutine(CloseLast());
               OnRun = true;
               return;
            }
            
            if (!OnRun)
            {
                 int b = CoinWinning < 100 ? CoinWinning - Random.Range(-3,5) : CoinWinning < 300 ? 
        CoinWinning - Random.Range(-30,30) : CoinWinning < 1000 ? CoinWinning - Random.Range(-50,50) : CoinWinning > 1000 ? CoinWinning - Random.Range(-100,100) : CoinWinning;
        CoinWinning = b;
        WinSound.Play();
        if(CoinIsGone){
        CurrentCoin += CoinWinning;
        ObscuredPrefs.SetInt("MyCoin", CurrentCoin);
        }
                OnRun = true;
            }
            CoinWiningShow.text = CoinWinning.ToString();
            CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(Offset, CoinText.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
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
        PlayerPrefs.SetInt("Restarted",1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        DG.Tweening.DOTween.KillAll();
        if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
        {
           // PlayerPrefs.SetInt("FastRun", 1);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    private IEnumerator CloseLast()
    {
        WinLast.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        Last.gameObject.SetActive(true);
        WinLast.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
