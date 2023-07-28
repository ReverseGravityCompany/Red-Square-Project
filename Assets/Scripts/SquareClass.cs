using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareClass : MonoBehaviour
{
    #region Properties
    [HideInInspector] public bool CanPush = false;
    [HideInInspector] public bool CanAttack = false;
    private Player thePlayer;

    [HideInInspector] public bool StopCouting;
    [HideInInspector] public bool StopCounting2;
    [HideInInspector] public bool StopCounting3;

    [HideInInspector] public bool TurboMortal, CopacityMortal, RandomChangeMortal, AllAttackMortal, X2Mortal, MaxSpaceMortal;
    public Color WhiteLow;
    public Color InvisibaleColor;

    private Skills skills;

    [HideInInspector] public GameObject star1, star2, star3;
    [HideInInspector] public Color MyCountNumberColor;
    [HideInInspector] public List<GameObject> stateMortal;

    public bool Learn;

    #endregion

    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        stateMortal = gameObject.GetComponent<StateMortal>().MyTypeOfAttack;
        for (int i = 0; i < stateMortal.Count; i++)
        {
            stateMortal[i] = gameObject.GetComponent<StateMortal>().MyTypeOfAttack[i];
        }

        star1 = transform.Find("Star1").gameObject;
        star2 = transform.Find("Star2").gameObject;
        star3 = transform.Find("Star3").gameObject;

        skills = Skills._Instance;
    }


    private void Update()
    {
        if (!Learn)
        {
            if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
            {
                MyCountNumberColor = thePlayer.RedColorText;
            }
            else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
            {
                MyCountNumberColor = thePlayer.BlueColorText;
            }
            else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
            {
                MyCountNumberColor = thePlayer.YellowColorText;
            }
            else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
            {
                MyCountNumberColor = thePlayer.PinkColorText;
            }
            else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
            {
                MyCountNumberColor = thePlayer.GreenColorText;
            }
            else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
            {
                MyCountNumberColor = thePlayer.OrangeColorText;
            }
            else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
            {
                MyCountNumberColor = thePlayer.LastColorText;
            }


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
                    star1.GetComponent<Image>().color = MyCountNumberColor;
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
                    star2.GetComponent<Image>().color = MyCountNumberColor;
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
                    star3.GetComponent<Image>().color = MyCountNumberColor;
                    skills.AllAttackObj.GetComponent<Image>().color = WhiteLow;
                    star3.SetActive(true);
                    skills.AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

                }

                if (!X2Mortal)
                {
                    skills.X2Obj.GetComponent<Image>().color = Color.white;
                    StopCounting2 = false;
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
    }


    public void CheckCanWhoAttack()
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
                ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, gameObject);
            }
        }
    }


    public void CheckAllSkills()
    {
        if (!TurboMortal)
        {
            star1.SetActive(false);
            gameObject.GetComponent<IncreaseMortal>().AmountIncrease = 1;
        }

        if (!CopacityMortal)
        {
            star2.SetActive(false);
            gameObject.GetComponent<IncreaseMortal>().MaxSpace = 100;
        }

        if (!RandomChangeMortal)
        {

        }
        if (!AllAttackMortal)
        {
            star3.SetActive(false);
            StateMortal sm = GetComponent<StateMortal>();
            sm.MyTypeOfAttack = stateMortal;
            for (int i = 0; i < sm.MyTypeOfAttack.Count; i++)
            {
                sm.MyTypeOfAttack[i] = stateMortal[i];
            }

        }

        if (!X2Mortal)
        {
            StopCounting2 = false;
        }

        if (!MaxSpaceMortal)
        {
            StopCounting3 = false;
        }
    }
}
