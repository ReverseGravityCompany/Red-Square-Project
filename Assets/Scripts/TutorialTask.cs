using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialTask : MonoBehaviour
{
    public GameObject NextTutorial_Button;
    public TextMeshProUGUI Task;
    public Color SuccessfulLearning;

    StateMortal[] AllMortalObjects;


    public void LessonSuccesful()
    {
        NextTutorial_Button.GetComponent<Button>().interactable = true;
        //Task.color = SuccessfulLearning;
    }

    public void NextTutorial()
    {
        DG.Tweening.DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Turbo(Button btn)
    {
        AllMortalObjects = new StateMortal[FindObjectsOfType<StateMortal>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<StateMortal>()[i];
            if(AllMortalObjects[i].GetIdentity() == StateMortal.iden.Blue)
            {
                AllMortalObjects[i].GetComponent<StateMortal>().AmountIncrease = 2;
                AllMortalObjects[i].GetComponent<StateMortal>().TurboMortal = true;
            }
        }
        btn.interactable = false;
    }

    public void Copacity(Button btn)
    {
        AllMortalObjects = new StateMortal[FindObjectsOfType<StateMortal>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<StateMortal>()[i];
            if (AllMortalObjects[i].GetIdentity() == StateMortal.iden.Blue)
            {
                AllMortalObjects[i].GetComponent<StateMortal>().MaxSpace = 200;
                AllMortalObjects[i].GetComponent<StateMortal>().CopacityMortal = true;
            }
        }
        btn.interactable = false;
    }
}
