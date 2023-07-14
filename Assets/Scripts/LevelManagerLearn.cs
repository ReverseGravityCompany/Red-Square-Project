using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManagerLearn : MonoBehaviour
{
    public bool Learn1 , Learn2 , Learn3 , Learn4;


    public GameObject DESquare;

    public TextMeshProUGUI Task;
    public TextMeshProUGUI Task2;

    public Color ChangeTaskColor;
    public GameObject FadeNextbutton;

    public string nextLearn;

    public Image TurboImage;
    public GameObject TurboOff;
    public GameObject BeSquare;
    public GameObject Star1;

    public GameObject CopacityOff;

    private void Awake() {
        Time.timeScale = 1;
        if(FindObjectOfType<squareSoliderCounte>()){
            FindObjectOfType<squareSoliderCounte>().StartGenerateSolider();
        }
    }

    private void Update() {
        if(Learn1){
            if(DESquare.GetComponent<Identity>().GetIdentity() == Identity.iden.Red){
                Task.color = ChangeTaskColor;
                FadeNextbutton.SetActive(false);
                FadeNextbutton.transform.parent.GetComponent<Button>().interactable = true;
            }
        }
        if(Learn2){
            if(DESquare.GetComponent<Identity>().GetIdentity() == Identity.iden.Red){
                Task.color = ChangeTaskColor;
                FadeNextbutton.SetActive(false);
                FadeNextbutton.transform.parent.GetComponent<Button>().interactable = true;
            }
        }

        if(Learn3){
            if(DESquare.GetComponent<Identity>().GetIdentity() == Identity.iden.Red){
                Task.color = ChangeTaskColor;
                FadeNextbutton.SetActive(false);
                FadeNextbutton.transform.parent.GetComponent<Button>().interactable = true;
            }
        }

        if(Learn4){
            if(DESquare.GetComponent<Identity>().GetIdentity() == Identity.iden.Red){
                Task.color = ChangeTaskColor;
                FadeNextbutton.SetActive(false);
                FadeNextbutton.transform.parent.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Next(){
        SceneManager.LoadScene(nextLearn);
    }

    public void Turbo(){
      TurboOff.SetActive(true);
      Task2.color = ChangeTaskColor;
      Star1.SetActive(true);
      BeSquare.GetComponent<IncreaseMortal>().AmountIncrease = 2;
    }

    public void Copacity(){
      CopacityOff.SetActive(true);
      Task2.color = ChangeTaskColor;
      Star1.SetActive(true);
      BeSquare.GetComponent<IncreaseMortal>().MaxSpace = 200;
    }

}
