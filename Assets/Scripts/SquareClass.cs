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

    public GameObject Turbo, Copacity, RandomChange, AllAttack, X2, MaxSpace;

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
                    Turbo.GetComponent<Image>().color = Color.white;
                    star1.SetActive(false);
                    gameObject.GetComponent<IncreaseMortal>().AmountIncrease = 1;
                    Turbo.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
                }
                else
                {
                    star1.GetComponent<Image>().color = MyCountNumberColor;
                    star1.SetActive(true);
                    Turbo.gameObject.GetComponent<Image>().color = WhiteLow;
                    Turbo.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

                }

                if (!CopacityMortal)
                {
                    Copacity.GetComponent<Image>().color = Color.white;
                    star2.SetActive(false);
                    gameObject.GetComponent<IncreaseMortal>().MaxSpace = 100;
                    Copacity.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
                }
                else
                {
                    Copacity.GetComponent<Image>().color = WhiteLow;
                    star2.GetComponent<Image>().color = MyCountNumberColor;
                    star2.SetActive(true);
                    Copacity.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

                }

                if (!RandomChangeMortal)
                {
                    RandomChange.GetComponent<Image>().color = Color.white;
                    RandomChange.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
                }
                else
                {
                    RandomChange.GetComponent<Image>().color = WhiteLow;
                    RandomChange.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

                }




                if (!AllAttackMortal)
                {
                    AllAttack.GetComponent<Image>().color = Color.white;
                    star3.SetActive(false);
                    StateMortal sm = GetComponent<StateMortal>();
                    sm.MyTypeOfAttack = stateMortal;
                    for (int i = 0; i < sm.MyTypeOfAttack.Count; i++)
                    {
                        sm.MyTypeOfAttack[i] = stateMortal[i];
                    }
                    AllAttack.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

                }
                else
                {
                    star3.GetComponent<Image>().color = MyCountNumberColor;
                    AllAttack.GetComponent<Image>().color = WhiteLow;
                    star3.SetActive(true);
                    AllAttack.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

                }

                if (!X2Mortal)
                {
                    X2.GetComponent<Image>().color = Color.white;
                    StopCounting2 = false;
                    X2.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

                }
                else
                {
                    X2.GetComponent<Image>().color = WhiteLow;
                    X2.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

                }

                if (!MaxSpaceMortal)
                {
                    MaxSpace.GetComponent<Image>().color = Color.white;
                    MaxSpace.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
                }
                else
                {
                    MaxSpace.GetComponent<Image>().color = WhiteLow;
                    MaxSpace.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
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
