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

    Identity[] AllMortalObjects;


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
        AllMortalObjects = new Identity[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<Identity>()[i];
            if(AllMortalObjects[i].GetIdentity() == Identity.iden.Blue)
            {
                AllMortalObjects[i].GetComponent<IncreaseMortal>().AmountIncrease = 2;
                AllMortalObjects[i].GetComponent<SquareClass>().TurboMortal = true;
            }
        }
        btn.interactable = false;
    }

    public void Copacity(Button btn)
    {
        AllMortalObjects = new Identity[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<Identity>()[i];
            if (AllMortalObjects[i].GetIdentity() == Identity.iden.Blue)
            {
                AllMortalObjects[i].GetComponent<IncreaseMortal>().MaxSpace = 200;
                AllMortalObjects[i].GetComponent<SquareClass>().CopacityMortal = true;
            }
        }
        btn.interactable = false;
    }
}
