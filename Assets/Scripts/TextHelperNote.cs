using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHelperNote : MonoBehaviour
{
    public Text NoteHelper;
    public Image TajobMask;
    public Sprite SpRed, SpYellow, SpGreen;
    public Color Red, Yellow, Green;


    public void GreenMark()
    {
        TajobMask.sprite = SpGreen;
        //NoteHelper.color = Green;
        //NoteHelper.text = $"Activited";
        TajobMask.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(NoteEnd());
    }


    public void RedMark()
    {
        TajobMask.sprite = SpRed;
        //NoteHelper.color = Red;
        //NoteHelper.text = $"Skill is Activited";
        TajobMask.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(NoteEnd());
    }



    private IEnumerator NoteEnd()
    {
        yield return new WaitForSeconds(2f);
        TajobMask.gameObject.SetActive(false);
    }



}
//NoteHelper.text = $"<color=cyan> Note :</color><color=white> {TextHelper[rand]}</color>";