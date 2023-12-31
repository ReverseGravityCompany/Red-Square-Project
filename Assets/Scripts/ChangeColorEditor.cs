﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ChangeColorEditor : MonoBehaviour
{
        public Player thePlayer;

    //    private void Awake()
    //    {
    //        gameObject.GetComponent<ChangeColorEditor>().enabled = false;
    //    }

#if UNITY_EDITOR
    private void Update()
    {
        if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Blue)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Blue);
            gameObject.GetComponent<Image>().color = thePlayer.BlueColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.BlueColorText;
        }
        else if(gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.None)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.None);
            gameObject.GetComponent<Image>().color = thePlayer.NoneColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.NoneColorText;
        }
        else if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Red)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Red);
            gameObject.GetComponent<Image>().color = thePlayer.RedColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.RedColorText;
        }
        else if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Yellow)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Yellow);
            gameObject.GetComponent<Image>().color = thePlayer.YellowColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.YellowColorText;
        }
        else if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Pink)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Pink);
            gameObject.GetComponent<Image>().color = thePlayer.PinkColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.PinkColorText;
        }
        else if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Green)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Green);
            gameObject.GetComponent<Image>().color = thePlayer.GreenColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.GreenColorText;
        }
        else if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Orange)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Orange);
            gameObject.GetComponent<Image>().color = thePlayer.OrangeColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.OrangeColorText;
        }
        else if (gameObject.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.LastColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.LastColor);
            gameObject.GetComponent<Image>().color = thePlayer.LastColor;
            gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.LastColorText;
        }
    }
#endif
}
