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

    private void LateUpdate()
    {
        ShowMortal.text = CurrentCount.ToString();
    }

    public void ValidateMortal()
    {
        AmountIncrease = 1;
    }

    public void UpdateDoubleX()
    {
        SquareClass squareClass = GetComponent<SquareClass>();
        Player player = FindObjectOfType<Player>();
        squareClass.star1.SetActive(true);
        Identity.iden EnemyColor = gameObject.GetComponent<Identity>().GetIdentity();
        switch (EnemyColor)
        {
            case Identity.iden.Red:
                squareClass.star1.GetComponent<Image>().color = player.RedColorText;
                break;
            case Identity.iden.Yellow:
                squareClass.star1.GetComponent<Image>().color = player.YellowColorText;
                break;
            case Identity.iden.Pink:
                squareClass.star1.GetComponent<Image>().color = player.PinkColorText;
                break;
            case Identity.iden.Green:
                squareClass.star1.GetComponent<Image>().color = player.GreenColorText;
                break;
            case Identity.iden.Orange:
                squareClass.star1.GetComponent<Image>().color = player.OrangeColorText;
                break;
            case Identity.iden.LastColor:
                squareClass.star1.GetComponent<Image>().color = player.LastColorText;
                break;
        }
        squareClass.TurboMortal = true;
    }
}
