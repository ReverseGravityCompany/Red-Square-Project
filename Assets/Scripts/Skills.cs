using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
public class Skills : MonoBehaviour
{
    #region Properties
    [HideInInspector] public bool turbo, copacity, randomChange, allAttack , X2 , MaxSpace;
    public GameObject TurboObj, CopacityObj, RandomChangeObj, AllAttackObj, X2Obj, MaxSpaceObj;
    [HideInInspector] public SquareClass SelectedMortal;

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
        theLevelmanager = FindObjectOfType<LevelManager>();
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
            if(ObscuredPrefs.GetInt("MyCoin") >= 100)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 100;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                turbo = true;
                SelectedMortal.TurboMortal = true;
                TurboObj.GetComponent<Image>().color = SelectedMortal.WhiteLow;
                SelectedMortal.star1.GetComponent<Image>().color = SelectedMortal.MyCountNumberColor;
                SelectedMortal.star1.SetActive(true);
                SelectedMortal.GetComponent<IncreaseMortal>().AmountIncrease = 2;
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
            if (ObscuredPrefs.GetInt("MyCoin") >= 400)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 400;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                copacity = true;
                SelectedMortal.CopacityMortal = true;
                CopacityObj.GetComponent<Image>().color = SelectedMortal.WhiteLow;
                SelectedMortal.star2.GetComponent<Image>().color = SelectedMortal.MyCountNumberColor;
                SelectedMortal.star2.SetActive(true);
                SelectedMortal.GetComponent<IncreaseMortal>().MaxSpace = 200;
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
            if (ObscuredPrefs.GetInt("MyCoin") >= 30)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 30;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                randomChange = true;
                SelectedMortal.RandomChangeMortal = true;
                RandomChangeObj.GetComponent<Image>().color = SelectedMortal.WhiteLow;

                SelectedMortal.GetComponent<IncreaseMortal>().CurrentCount += Random.value < 0.5f ? -10 : 10;
                if (SelectedMortal.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                {
                    SelectedMortal.GetComponent<IncreaseMortal>().CurrentCount = 0;
                }
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
            if (ObscuredPrefs.GetInt("MyCoin") >= 200)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 200;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                allAttack = true;
                SelectedMortal.AllAttackMortal = true;
                SelectedMortal.star3.GetComponent<Image>().color = SelectedMortal.MyCountNumberColor;
                AllAttackObj.GetComponent<Image>().color = SelectedMortal.WhiteLow;
                SelectedMortal.star3.SetActive(true);
                SelectedMortal.GetComponent<StateMortal>().MyTypeOfAttack.Clear();
                for (int i = 0; i < GameObject.FindObjectsOfType<SquareClass>().Length; i++)
                {
                    SelectedMortal.GetComponent<StateMortal>().MyTypeOfAttack.Add(GameObject.FindObjectsOfType<SquareClass>()[i].gameObject);
                }
                SelectedMortal.CheckCanWhoAttack();
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
            if (ObscuredPrefs.GetInt("MyCoin") >= 1000)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 1000;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                X2 = true;
                SelectedMortal.X2Mortal = true;
                X2Obj.GetComponent<Image>().color = SelectedMortal.WhiteLow;
                SelectedMortal.GetComponent<IncreaseMortal>().CurrentCount *= 2;
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
            if (ObscuredPrefs.GetInt("MyCoin") >= 2500)
            {
                Correct_Sound.Play();
                theLevelmanager.CurrentCoin -= 2500;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                MaxSpace = true;
                SelectedMortal.MaxSpaceMortal = true;
                MaxSpaceObj.GetComponent<Image>().color = SelectedMortal.WhiteLow;
                if (SelectedMortal.GetComponent<IncreaseMortal>().CurrentCount <= SelectedMortal.GetComponent<IncreaseMortal>().MaxSpace)
                    SelectedMortal.GetComponent<IncreaseMortal>().CurrentCount = SelectedMortal.GetComponent<IncreaseMortal>().MaxSpace;

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
