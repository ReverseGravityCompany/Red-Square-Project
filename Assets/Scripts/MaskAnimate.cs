using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MaskAnimate : MonoBehaviour
{
    public RectTransform mask;
    public Vector3 truePlace, Offset;
    public float Speed;
    public float MinDelay, MaxDelay;
    public Color invisible;

    void Start()
    {
        mask.DOAnchorPos(truePlace, 0);
        animate();
    }

    public void animate()
    {
        mask.DOAnchorPos(Offset, Speed).SetDelay(Random.Range(MinDelay, MaxDelay)).OnComplete(() =>
        {
            mask.DOAnchorPos(truePlace, 0);
            animate();
        });
    }

    public void disableMask()
    {
        gameObject.GetComponent<Image>().color = invisible;
    }

    public void EnableMask()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }
}
