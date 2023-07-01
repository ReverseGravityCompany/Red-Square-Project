using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
public class Skills : MonoBehaviour
{
    public bool turbo, copacity, randomChange, allAttack , X2 , MaxSpace;
    public GameObject TurboObj, CopacityObj, RandomChangeObj, AllAttackObj, X2Obj, MaxSpaceObj;
    public SquareClass ChoiceMortal;
    private LevelManager theLevelmanager;
    public Color InvisibaleColor;
    public bool DontWork;

    public AudioSource SoundSkillWork;
    public AudioSource SoundSkillDidntWork;

    public TextHelperNote Note;

    private void Start()
    {
        theLevelmanager = FindObjectOfType<LevelManager>();
        Note = FindObjectOfType<TextHelperNote>();
    }

    public void Turbo()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        if(ChoiceMortal != null && ChoiceMortal.TurboMortal == false)
        {
            if(ObscuredPrefs.GetInt("MyCoin") >= 100)
            {
                SoundSkillWork.Play();
                theLevelmanager.CurrentCoin -= 100;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                turbo = true;
                ChoiceMortal.TurboMortal = true;
                TurboObj.GetComponent<Image>().color = ChoiceMortal.WhiteLow;
                ChoiceMortal.star1.GetComponent<Image>().color = ChoiceMortal.MyCountNumberColor;
                ChoiceMortal.star1.SetActive(true);
                ChoiceMortal.GetComponent<IncreaseMortal>().AmountIncrease = 2;
                Note.GreenMark();
                TurboObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }
            else
            {
                SoundSkillDidntWork.Play();
                Note.RedMark();
            }
        }
        else if (ChoiceMortal.TurboMortal)
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
        if (ChoiceMortal != null && ChoiceMortal.CopacityMortal == false)
        {
            if (ObscuredPrefs.GetInt("MyCoin") >= 400)
            {
                SoundSkillWork.Play();
                theLevelmanager.CurrentCoin -= 400;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                copacity = true;
                ChoiceMortal.CopacityMortal = true;
                CopacityObj.GetComponent<Image>().color = ChoiceMortal.WhiteLow;
                ChoiceMortal.star2.GetComponent<Image>().color = ChoiceMortal.MyCountNumberColor;
                ChoiceMortal.star2.SetActive(true);
                ChoiceMortal.GetComponent<IncreaseMortal>().MaxSpace = 200;
                Note.GreenMark();
                CopacityObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                SoundSkillDidntWork.Play();
                Note.RedMark();
            }
        }
        else if (ChoiceMortal.CopacityMortal)
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
        if (ChoiceMortal != null && ChoiceMortal.RandomChangeMortal == false)
        {
            if (ObscuredPrefs.GetInt("MyCoin") >= 30)
            {
                SoundSkillWork.Play();
                theLevelmanager.CurrentCoin -= 30;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                randomChange = true;
                ChoiceMortal.RandomChangeMortal = true;
                RandomChangeObj.GetComponent<Image>().color = ChoiceMortal.WhiteLow;

                ChoiceMortal.GetComponent<IncreaseMortal>().CurrentCount += Random.value < 0.5f ? -10 : 10;
                if (ChoiceMortal.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                {
                    ChoiceMortal.GetComponent<IncreaseMortal>().CurrentCount = 0;
                }
                Note.GreenMark();
                RandomChangeObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                SoundSkillDidntWork.Play();
                Note.RedMark();
            }
        }
        else if (ChoiceMortal.RandomChangeMortal)
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
        if (ChoiceMortal != null && ChoiceMortal.AllAttackMortal == false && !DontWork)
        {
            if (ObscuredPrefs.GetInt("MyCoin") >= 200)
            {
                SoundSkillWork.Play();
                theLevelmanager.CurrentCoin -= 200;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                allAttack = true;
                ChoiceMortal.AllAttackMortal = true;
                ChoiceMortal.star3.GetComponent<Image>().color = ChoiceMortal.MyCountNumberColor;
                AllAttackObj.GetComponent<Image>().color = ChoiceMortal.WhiteLow;
                ChoiceMortal.star3.SetActive(true);
                ChoiceMortal.GetComponent<StateMortal>().MyTypeOfAttack.Clear();
                for (int i = 0; i < GameObject.FindObjectsOfType<SquareClass>().Length; i++)
                {
                    ChoiceMortal.GetComponent<StateMortal>().MyTypeOfAttack.Add(GameObject.FindObjectsOfType<SquareClass>()[i].gameObject);
                }
                ChoiceMortal.CheckCanWhoAttack();
                Note.GreenMark();
                AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                SoundSkillDidntWork.Play();
                Note.RedMark();
            }
        }
        else if (ChoiceMortal.AllAttackMortal)
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
        if (ChoiceMortal != null && ChoiceMortal.X2Mortal == false)
        {
            if (ObscuredPrefs.GetInt("MyCoin") >= 1000)
            {
                SoundSkillWork.Play();
                theLevelmanager.CurrentCoin -= 1000;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                X2 = true;
                ChoiceMortal.X2Mortal = true;
                X2Obj.GetComponent<Image>().color = ChoiceMortal.WhiteLow;
                ChoiceMortal.GetComponent<IncreaseMortal>().CurrentCount *= 2;
                Note.GreenMark();
                X2Obj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }
            else
            {
                SoundSkillDidntWork.Play();
                Note.RedMark();
            }
        }
        else if (ChoiceMortal.X2Mortal)
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
        if (ChoiceMortal != null && ChoiceMortal.MaxSpaceMortal == false)
        {
            if (ObscuredPrefs.GetInt("MyCoin") >= 2500)
            {
                SoundSkillWork.Play();
                theLevelmanager.CurrentCoin -= 2500;
                ObscuredPrefs.SetInt("MyCoin", theLevelmanager.CurrentCoin);
                MaxSpace = true;
                ChoiceMortal.MaxSpaceMortal = true;
                MaxSpaceObj.GetComponent<Image>().color = ChoiceMortal.WhiteLow;
                if (ChoiceMortal.GetComponent<IncreaseMortal>().CurrentCount <= ChoiceMortal.GetComponent<IncreaseMortal>().MaxSpace)
                    ChoiceMortal.GetComponent<IncreaseMortal>().CurrentCount = ChoiceMortal.GetComponent<IncreaseMortal>().MaxSpace;

                Note.GreenMark();
                MaxSpaceObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }
            else
            {
                SoundSkillDidntWork.Play();
                Note.RedMark();
            }
        }
        else if (ChoiceMortal.MaxSpaceMortal)
        {
            Note.RedMark();
        }
    }

}
