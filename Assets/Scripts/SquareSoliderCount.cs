using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSoliderCount : MonoBehaviour
{
    private WaitForSeconds timer;
    [SerializeField] private IncreaseMortal[] sqaurs;

    private void Start()
    {
        sqaurs = new IncreaseMortal[GameObject.FindGameObjectsWithTag("square").Length];
        for (int i = 0; i < sqaurs.Length; i++)
        {
            sqaurs[i] = GameObject.FindGameObjectsWithTag("square")[i].GetComponent<IncreaseMortal>();
        }
    }

    public void StartGenerateSolider()
    {
        timer = new WaitForSeconds(1);
        StartCoroutine(GeneratingSolider());
    }

    private void Update()
    {
        for (int i = 0; i < sqaurs.Length; i++)
        {
            sqaurs[i].ShowMortal.text = sqaurs[i].CurrentCount.ToString();
            if (sqaurs[i].CurrentCount >= 1000)
            {
                sqaurs[i].CurrentCount = 1000;
            }
            if (sqaurs[i].CurrentCount >= sqaurs[i].MaxSpace)
            {
                sqaurs[i].isHaveAnySpace = true;
            }
            else
            {
                sqaurs[i].isHaveAnySpace = false;
            }
        }
    }

    private IEnumerator GeneratingSolider()
    {
        while (true)
        {
            yield return timer;
            for (int i = 0; i < sqaurs.Length; i++)
            {
                if (!sqaurs[i].isHaveAnySpace)
                {
                    sqaurs[i].CurrentCount += sqaurs[i].AmountIncrease;
                    sqaurs[i].ShowMortal.text = sqaurs[i].CurrentCount.ToString();
                }
            }
        }
    }
}
