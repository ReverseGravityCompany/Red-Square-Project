using System.Collections;
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
        if (gameObject.GetComponent<Image>().color == thePlayer.BlueColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Blue);
        }
        else if(gameObject.GetComponent<Image>().color == thePlayer.NoneColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.None);
        }
        else if (gameObject.GetComponent<Image>().color == thePlayer.RedColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Red);
        }
        else if (gameObject.GetComponent<Image>().color == thePlayer.YellowColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Yellow);
        }
        else if (gameObject.GetComponent<Image>().color == thePlayer.PinkColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Pink);
        }
        else if (gameObject.GetComponent<Image>().color == thePlayer.GreenColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Green);
        }
        else if (gameObject.GetComponent<Image>().color == thePlayer.OrangeColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.Orange);
        }
        else if (gameObject.GetComponent<Image>().color == thePlayer.LastColor)
        {
            gameObject.GetComponent<StateMortal>().SetIdentity(StateMortal.iden.LastColor);
        }





        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.BlueColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.BlueColorText;
        //}
        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.RedColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.RedColorText;
        //}
        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.YellowColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.YellowColorText;
        //}
        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.PinkColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.PinkColorText;
        //}
        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.GreenColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.GreenColorText;
        //}
        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.OrangeColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.OrangeColorText;
        //}
        //else if (gameObject.GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
        //{
        //    gameObject.GetComponent<Image>().color = thePlayer.LastColor;
        //    gameObject.transform.Find("CountMortal").gameObject.GetComponent<Text>().color = thePlayer.LastColorText;
        //}
    }
#endif
}
