using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareClass : MonoBehaviour
{
    #region Properties
    [HideInInspector] public bool CanPush = false;
    [HideInInspector] public bool CanAttack = false;
    [HideInInspector] public bool isUnderAttack = false;
    private Player thePlayer;

    [HideInInspector] public bool TurboMortal, CopacityMortal, RandomChangeMortal, AllAttackMortal, X2Mortal, MaxSpaceMortal;
    public Color WhiteLow;
    public Color InvisibaleColor;

    private Skills skills;

    [HideInInspector] public GameObject star1, star2, star3;
    [HideInInspector] public List<GameObject> stateMortal;

    #endregion

    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        stateMortal = gameObject.GetComponent<StateMortal>().MyTypeOfAttack;
        for (int i = 0; i < stateMortal.Count; i++)
        {
            stateMortal[i] = gameObject.GetComponent<StateMortal>().MyTypeOfAttack[i];
        }
        skills = Skills._Instance;

        star1 = transform.Find("Star1").gameObject;
        star2 = transform.Find("Star2").gameObject;
        star3 = transform.Find("Star3").gameObject;

        star1.GetComponent<Image>().color = thePlayer.BlueColorText;
        star2.GetComponent<Image>().color = thePlayer.BlueColorText;
        star3.GetComponent<Image>().color = thePlayer.BlueColorText;

        if (gameObject.GetComponent<IncreaseMortal>().AmountIncrease == 2)
        {
            gameObject.GetComponent<IncreaseMortal>().UpdateDoubleX();
        }
    }


    public void UpdateSkills()
    {
        //Skills
        if (thePlayer.MyBlue != null && gameObject == thePlayer.MyBlue)
        {
            if (!TurboMortal)
            {
                skills.TurboObj.GetComponent<Image>().color = Color.white;
                star1.SetActive(false);
                gameObject.GetComponent<IncreaseMortal>().AmountIncrease = 1;
                skills.TurboObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                star1.SetActive(true);
                skills.TurboObj.gameObject.GetComponent<Image>().color = WhiteLow;
                skills.TurboObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }

            if (!CopacityMortal)
            {
                skills.CopacityObj.GetComponent<Image>().color = Color.white;
                star2.SetActive(false);
                gameObject.GetComponent<IncreaseMortal>().MaxSpace = 100;
                skills.CopacityObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                skills.CopacityObj.GetComponent<Image>().color = WhiteLow;
                star2.SetActive(true);
                skills.CopacityObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!RandomChangeMortal)
            {
                skills.RandomChangeObj.GetComponent<Image>().color = Color.white;
                skills.RandomChangeObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                skills.RandomChangeObj.GetComponent<Image>().color = WhiteLow;
                skills.RandomChangeObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!AllAttackMortal)
            {
                skills.AllAttackObj.GetComponent<Image>().color = Color.white;
                star3.SetActive(false);
                StateMortal sm = GetComponent<StateMortal>();
                sm.MyTypeOfAttack = stateMortal;
                for (int i = 0; i < sm.MyTypeOfAttack.Count; i++)
                {
                    sm.MyTypeOfAttack[i] = stateMortal[i];
                }
                skills.AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

            }
            else
            {
                skills.AllAttackObj.GetComponent<Image>().color = WhiteLow;
                star3.SetActive(true);
                skills.AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!X2Mortal)
            {
                skills.X2Obj.GetComponent<Image>().color = Color.white;
                skills.X2Obj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

            }
            else
            {
                skills.X2Obj.GetComponent<Image>().color = WhiteLow;
                skills.X2Obj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!MaxSpaceMortal)
            {
                skills.MaxSpaceObj.GetComponent<Image>().color = Color.white;
                skills.MaxSpaceObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                skills.MaxSpaceObj.GetComponent<Image>().color = WhiteLow;
                skills.MaxSpaceObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }
        }
    }

    public void ResetEnemyTurbo()
    {
        star1.GetComponent<Image>().color = thePlayer.BlueColorText;
        star1.SetActive(false);
        TurboMortal = false;
    }

    public void Check_Attack_N()
    {
        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < ObjectsMortal.Length; i++)
        {
            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
        }

        for (int i = 0; i < ObjectsMortal.Length; i++)
        {
            if (thePlayer.MyBlue != null && ObjectsMortal[i] == thePlayer.MyBlue)
            {
                ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
                ObjectsMortal[i].GetComponent<StateMortal>().Attack_N(ObjectsMortal, gameObject);
            }
        }
    }

    public void ResetAllSkills()
    {
        TurboMortal = false;
        CopacityMortal = false;
        RandomChangeMortal = false;
        AllAttackMortal = false;
        X2Mortal = false;
        MaxSpaceMortal = false;

        star1.SetActive(false);
        gameObject.GetComponent<IncreaseMortal>().AmountIncrease = 1;

        star2.SetActive(false);
        gameObject.GetComponent<IncreaseMortal>().MaxSpace = 100;


        star3.SetActive(false);
        StateMortal sm = GetComponent<StateMortal>();
        sm.MyTypeOfAttack = stateMortal;
        for (int i = 0; i < sm.MyTypeOfAttack.Count; i++)
        {
            sm.MyTypeOfAttack[i] = stateMortal[i];
        }
    }
}
