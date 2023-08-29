using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationShop : MonoBehaviour
{
    private RectTransform rect;
    private Coroutine corut;
    void OnEnable()
    {
        rect = GetComponent<RectTransform>();

        corut = StartCoroutine(animate());
    }

    private void OnDisable()
    {
        StopCoroutine(corut);
    }
    private IEnumerator animate()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2, 6));

            rect.DOShakeScale(Random.Range(0.3f,0.8f), Random.Range(0.2f,0.6f), Random.Range(1,3), 90, true).SetUpdate(true).SetAutoKill(true);
        }
    }
}
