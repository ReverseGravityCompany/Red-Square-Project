using UnityEngine;
using UnityEngine.UI;
public class Skills : MonoBehaviour
{
    #region Properties
    public Image TurboObj, CopacityObj, RandomChangeObj, AllAttackObj, X2Obj, MaxSpaceObj;
    [HideInInspector] public StateMortal SelectedMortal;

    public AudioSource Correct_Sound;
    public AudioSource Error_Sound;

    private TextHelperNote Note;
    private LevelManager theLevelmanager;

    public static Skills _Instance { get;private set; }

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
    }

    #endregion

    private void Start()
    {
        theLevelmanager = LevelManager._Instance;
        Note = FindObjectOfType<TextHelperNote>();
    }

    #region Abilities Functions
    public void Turbo()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(SelectedMortal != null && SelectedMortal.TurboMortal == false)
        {
            if(PlayerPrefs.GetInt("MyCoin") >= 100)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 100;
                theLevelmanager.UpdateCoin();
                PlayerPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                SelectedMortal.TurboMortal = true;
                TurboObj.color = SelectedMortal.WhiteLow;
                SelectedMortal.star1.gameObject.SetActive(true);
                SelectedMortal.AmountIncrease = 2;
                SelectedMortal.UpdateSkills();
                Note.GreenMark();
                TurboObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }
            else
            {
                Error_Sound.Play();
                Note.RedMark();
            }
        }
        else if (SelectedMortal.TurboMortal)
        {
            Note.RedMark();
        }
        

    }
    public void Copacity()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (SelectedMortal != null && SelectedMortal.CopacityMortal == false)
        {
            if (PlayerPrefs.GetInt("MyCoin") >= 400)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 400;
                theLevelmanager.UpdateCoin();
                PlayerPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                SelectedMortal.CopacityMortal = true;
                CopacityObj.color = SelectedMortal.WhiteLow;
                SelectedMortal.star2.gameObject.SetActive(true);
                SelectedMortal.MaxSpace = 200;
                SelectedMortal.UpdateSkills();
                Note.GreenMark();
                CopacityObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                Error_Sound.Play();
                Note.RedMark();
            }
        }
        else if (SelectedMortal.CopacityMortal)
        {
            Note.RedMark();
        }

    }
    public void RandomChange()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (SelectedMortal != null && SelectedMortal.RandomChangeMortal == false)
        {
            if (PlayerPrefs.GetInt("MyCoin") >= 30)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 30;
                theLevelmanager.UpdateCoin();
                PlayerPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
             //   randomChange = true;
                SelectedMortal.RandomChangeMortal = true;
                RandomChangeObj.color = SelectedMortal.WhiteLow;

                SelectedMortal.CurrentCount += Random.value < 0.5f ? -10 : 10;
                if (SelectedMortal.CurrentCount <= 0)
                {
                    SelectedMortal.CurrentCount = 0;
                }
                SelectedMortal.UpdateSkills();
                Note.GreenMark();
                RandomChangeObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                Error_Sound.Play();
                Note.RedMark();
            }
        }
        else if (SelectedMortal.RandomChangeMortal)
        {
            Note.RedMark();
        }
    }
    public void AllAttack()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (SelectedMortal != null && SelectedMortal.AllAttackMortal == false)
        {
            if (PlayerPrefs.GetInt("MyCoin") >= 200)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 200;
                theLevelmanager.UpdateCoin();
                PlayerPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                SelectedMortal.AllAttackMortal = true;
                AllAttackObj.color = SelectedMortal.WhiteLow;
                SelectedMortal.star3.gameObject.SetActive(true);
                // ADD ALL MORTAL TO SELECTED
                SelectedMortal.MyTypeOfAttack.Clear();
                SelectedMortal.MaxTypeOfAttack(SelectedMortal.gameObject);
                //
                SelectedMortal.ResetInitializeAttack(SelectedMortal);
                SelectedMortal.UpdateSkills();
                Note.GreenMark(); 
                AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                Error_Sound.Play();
                Note.RedMark();
            }
        }
        else if (SelectedMortal.AllAttackMortal)
        {
            Note.RedMark();
        }

    }
    public void x2()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (SelectedMortal != null && SelectedMortal.X2Mortal == false)
        {
            if (PlayerPrefs.GetInt("MyCoin") >= 1000)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 1000;
                theLevelmanager.UpdateCoin();
                PlayerPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                SelectedMortal.X2Mortal = true;
                X2Obj.color = SelectedMortal.WhiteLow;
                SelectedMortal.CurrentCount *= 2;
                SelectedMortal.UpdateSkills();
                Note.GreenMark();
                X2Obj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                Error_Sound.Play();
                Note.RedMark();
            }
        }
        else if (SelectedMortal.X2Mortal)
        {
            Note.RedMark();
        }
    }
    public void Maxspace()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (SelectedMortal != null && SelectedMortal.MaxSpaceMortal == false)
        {
            if (PlayerPrefs.GetInt("MyCoin") >= 2500)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 2500;
                theLevelmanager.UpdateCoin();
                PlayerPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                SelectedMortal.MaxSpaceMortal = true;
                MaxSpaceObj.color = SelectedMortal.WhiteLow;
                if (SelectedMortal.CurrentCount <= SelectedMortal.MaxSpace)
                    SelectedMortal.CurrentCount = SelectedMortal.MaxSpace;

                SelectedMortal.UpdateSkills();
                Note.GreenMark();
                MaxSpaceObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }
            else
            {
                Error_Sound.Play();
                Note.RedMark();
            }
        }
        else if (SelectedMortal.MaxSpaceMortal)
        {
            Note.RedMark();
        }
    }

    #endregion
}
