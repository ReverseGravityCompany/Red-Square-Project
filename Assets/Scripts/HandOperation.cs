using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandOperation : MonoBehaviour
{
    private RectTransform rect;
    public Vector2 ToPos;
    private Tween tween;

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        tween = rect.DOAnchorPos(ToPos, 1f).SetDelay(0.3f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetAutoKill(true);
    }

    private void OnDisable()
    {
        tween.Kill();
    }
}
