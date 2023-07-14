using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseMortal : MonoBehaviour
{
    [HideInInspector] public Text ShowMortal;
    public int MaxSpace = 100;
    public int CurrentCount;
    public int AmountIncrease;
    public bool NoneAmount = false;
    [HideInInspector] public bool isHaveAnySpace;

    private void Awake()
    {
        ShowMortal = transform.Find("CountMortal").GetComponent<Text>();
        ShowMortal.text = CurrentCount.ToString();
    }

    private void Start()
    {
        if (NoneAmount && GetComponent<Identity>().GetIdentity() == Identity.iden.None)
        {
            AmountIncrease = 0;
        }
    }

    private void Update() {
        ShowMortal.text = CurrentCount.ToString();
    }

    public void ValidateMortal()
    {
        AmountIncrease = 1;
    }
}
