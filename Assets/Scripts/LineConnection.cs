using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnection : MonoBehaviour
{
    public float DisableTimer = 0.2f;

    private void OnEnable()
    {
        StartCoroutine(GameObject_Disable());
    }

    private IEnumerator GameObject_Disable()
    {
        yield return new WaitForSecondsRealtime(DisableTimer);
        gameObject.SetActive(false);
    }
}
