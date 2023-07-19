using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider slider;
    public float speed;
    public string HandCraftedLevelName;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void LateUpdate()
    {
        slider.value += speed * Time.deltaTime;
        if (slider.value == slider.maxValue)
        {
            NextScene();
        }
    }

    private void NextScene()
    {
        if (PlayerPrefs.HasKey("LearnEnd"))
        {
            int Level;

            if (PlayerPrefs.HasKey("MyLevel"))
            {
                Level = PlayerPrefs.GetInt("MyLevel");
                if (Level <= 100)
                {
                    SceneManager.LoadScene(HandCraftedLevelName);
                }
            }
            else
                SceneManager.LoadScene(HandCraftedLevelName);
        }
        else
        {
            //SceneManager.LoadScene("Learn1");
            SceneManager.LoadScene(HandCraftedLevelName);
        }
    }
}
