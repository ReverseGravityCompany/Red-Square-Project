using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider slider;
    public float speed;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void LateUpdate() {
        slider.value += speed * Time.deltaTime;
        if(slider.value == slider.maxValue){
            NextScene();
        }
    }

    private void NextScene()
    {
        if(PlayerPrefs.HasKey("LearnEnd")){
        int Level;

        if (PlayerPrefs.HasKey("MyLevel"))
        {
            Level = PlayerPrefs.GetInt("MyLevel");
        }
        else
            Level = 1;

        SceneManager.LoadScene(Level);
        }
        else{
          SceneManager.LoadScene("Learn1");
        }

    }
}
