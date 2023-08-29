using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.DOScale(0.4f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetAutoKill(true);
        rect.DOAnchorPosY(200, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetAutoKill(true);
    }
}
