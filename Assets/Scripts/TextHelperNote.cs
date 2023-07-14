using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHelperNote : MonoBehaviour
{
    #region Properties
    public Image StatesMask;
    public Sprite SpRed, SpGreen;
    public Color Red, Green;

    #endregion

    #region Functions
    public void GreenMark()
    {
        StatesMask.sprite = SpGreen;
        StatesMask.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(NoteEnd());
    }


    public void RedMark()
    {
        StatesMask.sprite = SpRed;
        StatesMask.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(NoteEnd());
    }

    private IEnumerator NoteEnd()
    {
        yield return new WaitForSeconds(2f);
        StatesMask.gameObject.SetActive(false);
    }
    #endregion
}