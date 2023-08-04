using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public float speed;
    public string HandCraftedLevelName;
    public Image cirsqu;

    public Color PrimiaryColor;
    public Color SecondaryColor;

    private void Start()
    {
        Time.timeScale = 1f;

        StartCoroutine(NextScene());
    }

    private void Update()
    {
        var t  = (Mathf.Sin(Time.time * speed) + 1) / 2.0;
        cirsqu.color = Color.Lerp(PrimiaryColor, SecondaryColor, (float)t);

        cirsqu.pixelsPerUnitMultiplier = Mathf.Clamp((float) t,0.01f,1);
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSecondsRealtime(3.35f);
        SceneManager.LoadScene(HandCraftedLevelName);
    }
}
