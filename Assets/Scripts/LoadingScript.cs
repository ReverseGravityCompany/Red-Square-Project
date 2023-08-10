using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    private Image LoadingBar;
    public float speed = 3.5f;

    private void OnEnable()
    {
        LoadingBar = GetComponent<Image>();
        LoadingBar.fillClockwise = true;
        LoadingBar.fillAmount = 0;
    }

    private void Update()
    {
        if (LoadingBar.fillClockwise)
        {
            LoadingBar.fillAmount += speed * Time.deltaTime;
            if(LoadingBar.fillAmount >= 1)
            {
                LoadingBar.fillClockwise = false;
            }
        } 
        else
        {
            LoadingBar.fillAmount -= speed * Time.deltaTime;

            if (LoadingBar.fillAmount <= 0)
            {
                LoadingBar.fillClockwise = true;
            }
        }
        
    }
}
