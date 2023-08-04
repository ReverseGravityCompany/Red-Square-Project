using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] public bool Red, Yellow, Pink, Green, Orange, Purple;
    private StateMortal[] allsquare;
    private List<StateMortal> RedSquare;
    private List<StateMortal> yellowSquare;
    private List<StateMortal> pinkSquare;
    private List<StateMortal> greenSquare;
    private List<StateMortal> OrangeSquare;
    private List<StateMortal> purpleSquare;
    private WaitForSeconds Timer;
    public float TimebettweenAttacks;
    public float TimeForFirstTime;
    private bool isFirstTimeAttack = true;
    private Player thePlayer;

    public enum Difficault { Easy, Meduim, Hard }
    public Difficault Difficaulty;


    private EnemySystem()
    {
        RedSquare = new List<StateMortal>();
        yellowSquare = new List<StateMortal>();
        pinkSquare = new List<StateMortal>();
        greenSquare = new List<StateMortal>();
        OrangeSquare = new List<StateMortal>();
        purpleSquare = new List<StateMortal>();
    }


    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        allsquare = new StateMortal[FindObjectsOfType<StateMortal>().Length];
        for (int i = 0; i < allsquare.Length; i++)
        {
            allsquare[i] = FindObjectsOfType<StateMortal>()[i];
        }

        Timer = new WaitForSeconds(TimebettweenAttacks + TimeForFirstTime);
    }

    public void GenerateEnemy()
    {
        if (Red)
            StartCoroutine(AttackRed());
        if (Yellow)
            StartCoroutine(AttackYellow());
        if (Pink)
            StartCoroutine(AttackPink());
        if (Green)
            StartCoroutine(AttackGreen());
        if (Orange)
            StartCoroutine(AttackOrange());
        if (Purple)
            StartCoroutine(AttackPurple());
    }

    public void LookAtList(StateMortal obj, StateMortal.iden type)
    {
        switch (type)
        {
            case StateMortal.iden.Red:
                for (int i = 0; i < RedSquare.Count; i++)
                {
                    if (RedSquare[i] == obj)
                    {
                        RedSquare.Remove(obj);
                    }
                }
                break;
            case StateMortal.iden.Yellow:
                for (int i = 0; i < yellowSquare.Count; i++)
                {
                    if (yellowSquare[i] == obj)
                    {
                        yellowSquare.Remove(obj);
                    }
                }
                break;
            case StateMortal.iden.Pink:
                for (int i = 0; i < pinkSquare.Count; i++)
                {
                    if (pinkSquare[i] == obj)
                    {
                        pinkSquare.Remove(obj);
                    }
                }
                break;
            case StateMortal.iden.Green:
                for (int i = 0; i < greenSquare.Count; i++)
                {
                    if (greenSquare[i] == obj)
                    {
                        greenSquare.Remove(obj);
                    }
                }
                break;
            case StateMortal.iden.Orange:
                for (int i = 0; i < OrangeSquare.Count; i++)
                {
                    if (OrangeSquare[i] == obj)
                    {
                        OrangeSquare.Remove(obj);
                    }
                }
                break;
            case StateMortal.iden.LastColor:
                for (int i = 0; i < purpleSquare.Count; i++)
                {
                    if (purpleSquare[i] == obj)
                    {
                        purpleSquare.Remove(obj);
                    }
                }
                break;
        }

    }

    private IEnumerator AttackRed()
    {
        while (true)
        {
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            RedSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
            {
                if (allsquare[i].GetIdentity() == StateMortal.iden.Red)
                {
                    RedSquare.Add(allsquare[i]);
                }
            }
            if (RedSquare.Count == 0)
            {
                break;
            }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                isFirstTimeAttack = false;
                RedSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetIdentity() == StateMortal.iden.Red)
                    {
                        RedSquare.Add(allsquare[i]);
                    }
                }
                if (RedSquare.Count == 0)
                {
                    break;
                }
                int randomRed = Random.Range(0, RedSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try
                    {
                        if (RedSquare[randomRed].GetIdentity() == StateMortal.iden.Red)
                        {
                            List<StateMortal> TypeOfAttack = RedSquare[randomRed].MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].isUnderAttack) continue;

                                StateMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i];
                                StateMortal tempRedSquareIncreaseMortal = RedSquare[randomRed];


                                #region Add Enemy Square To Itself
                                if (TypeOfAttack[i].GetIdentity() == StateMortal.iden.Red && Random.Range(0, 100) < 15)
                                {
                                    TypeOfAttack[i].isUnderAttack = true;
                                    int AttackDamage = tempRedSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempRedSquareIncreaseMortal.CurrentCount;
                                        tempRedSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;
                                        // Line Effect
                                        RedSquare[randomRed]
                                            .LineConnections(TypeOfAttack[i].gameObject, RedSquare[randomRed].transform.position, thePlayer.RedColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].isUnderAttack = false;
                                }
                                #endregion
                                #region Attack To Others
                                if (TypeOfAttack[i].GetIdentity() != StateMortal.iden.Red)
                                {
                                    if (tempRedSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].isUnderAttack = true; 
                                        int AttackDamage = tempRedSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempRedSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;
                                        // Line Effect
                                        RedSquare[randomRed]
                                            .LineConnections(TypeOfAttack[i].gameObject, RedSquare[randomRed].transform.position, thePlayer.RedColor);

                                        RedSquare[randomRed].ArmyBurning(TypeOfAttack[i].gameObject, thePlayer.RedColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].SetIdentity(StateMortal.iden.Red);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i].gameObject);
                                            TypeOfAttack[i].SetMortalColors(thePlayer.RedColor, thePlayer.RedColorText);
                                            TypeOfAttack[i].ResetAllSkills();
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].isUnderAttack = false;
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                    isAttackDone = true;

                if (Difficaulty == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 90)
                    {
                        isAttackDone = false;
                    }
                }
                else if (Difficaulty == Difficault.Hard)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
            }
        }
        yield return null;
    }

    private IEnumerator AttackYellow()
    {
        while (true)
        {
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            yellowSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
            {
                if (allsquare[i].GetIdentity() == StateMortal.iden.Yellow)
                {
                    yellowSquare.Add(allsquare[i]);
                }
            }
            if (yellowSquare.Count == 0)
            {
                break;
            }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                isFirstTimeAttack = false;
                yellowSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetIdentity() == StateMortal.iden.Yellow)
                    {
                        yellowSquare.Add(allsquare[i]);
                    }
                }
                if (yellowSquare.Count == 0)
                {
                    break;
                }
                int randomsquare = Random.Range(0, yellowSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try
                    {
                        if (yellowSquare[randomsquare].GetIdentity() == StateMortal.iden.Yellow)
                        {
                            List<StateMortal> TypeOfAttack = yellowSquare[randomsquare].MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].isUnderAttack) continue;

                                StateMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i];
                                StateMortal tempSquareIncreaseMortal = yellowSquare[randomsquare];

                                if (TypeOfAttack[i].GetIdentity() == StateMortal.iden.Yellow && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        yellowSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, yellowSquare[randomsquare].transform.position, thePlayer.YellowColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetIdentity() != StateMortal.iden.Yellow)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].isUnderAttack = true;
                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        yellowSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, yellowSquare[randomsquare].transform.position, thePlayer.YellowColor);

                                        yellowSquare[randomsquare].ArmyBurning(TypeOfAttack[i].gameObject, thePlayer.YellowColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                            Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].SetIdentity(StateMortal.iden.Yellow);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i].gameObject);
                                            TypeOfAttack[i].SetMortalColors(thePlayer.YellowColor, thePlayer.YellowColorText);
                                            TypeOfAttack[i].ResetAllSkills();
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].isUnderAttack = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                    isAttackDone = true;

                if (Difficaulty == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 70)
                    {
                        isAttackDone = false;
                    }
                }
                else if (Difficaulty == Difficault.Hard)
                {
                    if (Random.Range(0, 100) > 60)
                    {
                        isAttackDone = false;
                    }
                }
            }


        }
        yield return null;
    }

    private IEnumerator AttackPink()
    {
        while (true)
        {
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            pinkSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
            {
                if (allsquare[i].GetIdentity() == StateMortal.iden.Pink)
                {
                    pinkSquare.Add(allsquare[i]);
                }
            }
            if (pinkSquare.Count == 0)
            {
                break;
            }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                isFirstTimeAttack = false;
                pinkSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetIdentity() == StateMortal.iden.Pink)
                    {
                        pinkSquare.Add(allsquare[i]);
                    }
                }
                if (pinkSquare.Count == 0)
                {
                    break;
                }
                int randomsquare = Random.Range(0, pinkSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try
                    {
                        if (pinkSquare[randomsquare].GetIdentity() == StateMortal.iden.Pink)
                        {
                            List<StateMortal> TypeOfAttack = pinkSquare[randomsquare].MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].isUnderAttack) continue;

                                StateMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i];
                                StateMortal tempSquareIncreaseMortal = pinkSquare[randomsquare];

                                if (TypeOfAttack[i].GetIdentity() == StateMortal.iden.Pink && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        pinkSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, pinkSquare[randomsquare].transform.position, thePlayer.PinkColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetIdentity() != StateMortal.iden.Pink)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].isUnderAttack = true;
                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        pinkSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, pinkSquare[randomsquare].transform.position, thePlayer.PinkColor);


                                        pinkSquare[randomsquare].ArmyBurning(TypeOfAttack[i].gameObject,
                                          thePlayer.PinkColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].SetIdentity(StateMortal.iden.Pink);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i].gameObject);
                                            TypeOfAttack[i].SetMortalColors(thePlayer.PinkColor, thePlayer.PinkColorText);
                                            TypeOfAttack[i].ResetAllSkills();
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].isUnderAttack = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                    isAttackDone = true;

                if (Difficaulty == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (Difficaulty == Difficault.Hard)
                {
                    if (Random.Range(0, 100) > 70)
                    {
                        isAttackDone = false;
                    }
                }
            }


        }
        yield return null;

    }

    private IEnumerator AttackGreen()
    {
        while (true)
        {
            isFirstTimeAttack = false;
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            greenSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
            {
                if (allsquare[i].GetIdentity() == StateMortal.iden.Green)
                {
                    greenSquare.Add(allsquare[i]);
                }
            }
            if (greenSquare.Count == 0)
            {
                break;
            }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                greenSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetIdentity() == StateMortal.iden.Green)
                    {
                        greenSquare.Add(allsquare[i]);
                    }
                }
                if (greenSquare.Count == 0)
                {
                    break;
                }
                int randomsquare = Random.Range(0, greenSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try
                    {
                        if (greenSquare[randomsquare].GetIdentity() == StateMortal.iden.Green)
                        {
                            List<StateMortal> TypeOfAttack = greenSquare[randomsquare].MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].isUnderAttack) continue;

                                StateMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i];
                                StateMortal tempSquareIncreaseMortal = greenSquare[randomsquare];

                                if (TypeOfAttack[i].GetIdentity() == StateMortal.iden.Green && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        greenSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, greenSquare[randomsquare].transform.position, thePlayer.GreenColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetIdentity() != StateMortal.iden.Green)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].isUnderAttack = true;
                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        greenSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, greenSquare[randomsquare].transform.position, thePlayer.GreenColor);

                                        greenSquare[randomsquare].ArmyBurning(TypeOfAttack[i].gameObject,
                                        thePlayer.GreenColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);


                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].SetIdentity(StateMortal.iden.Green);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i].gameObject);
                                            TypeOfAttack[i].SetMortalColors(thePlayer.GreenColor, thePlayer.GreenColorText);
                                            TypeOfAttack[i].ResetAllSkills();
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].isUnderAttack = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                    isAttackDone = true;


                if (Difficaulty == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (Difficaulty == Difficault.Hard)
                {
                    if (Random.Range(0, 100) > 50)
                    {
                        isAttackDone = false;
                    }
                }
            }


        }
        yield return null;
    }

    private IEnumerator AttackOrange()
    {
        while (true)
        {
            isFirstTimeAttack = false;
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            OrangeSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
            {
                if (allsquare[i].GetIdentity() == StateMortal.iden.Orange)
                {
                    OrangeSquare.Add(allsquare[i]);
                }
            }
            if (OrangeSquare.Count == 0)
            {
                break;
            }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                OrangeSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetIdentity() == StateMortal.iden.Orange)
                    {
                        OrangeSquare.Add(allsquare[i]);
                    }
                }
                if (OrangeSquare.Count == 0)
                {
                    break;
                }
                int randomsquare = Random.Range(0, OrangeSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try
                    {
                        if (OrangeSquare[randomsquare].GetIdentity() == StateMortal.iden.Orange)
                        {
                            List<StateMortal> TypeOfAttack = OrangeSquare[randomsquare].MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].isUnderAttack) continue;

                                StateMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i];
                                StateMortal tempSquareIncreaseMortal = OrangeSquare[randomsquare];

                                if (TypeOfAttack[i].GetIdentity() == StateMortal.iden.Orange && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        OrangeSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, OrangeSquare[randomsquare].transform.position, thePlayer.OrangeColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetIdentity() != StateMortal.iden.Orange)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].isUnderAttack = true;
                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        OrangeSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, OrangeSquare[randomsquare].transform.position, thePlayer.OrangeColor);

                                        OrangeSquare[randomsquare].ArmyBurning(TypeOfAttack[i].gameObject,
                                        thePlayer.OrangeColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].SetIdentity(StateMortal.iden.Orange);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i].gameObject);
                                            TypeOfAttack[i].SetMortalColors(thePlayer.OrangeColor, thePlayer.OrangeColorText);
                                            TypeOfAttack[i].ResetAllSkills();
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].isUnderAttack = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                    isAttackDone = true;

                if (Difficaulty == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (Difficaulty == Difficault.Hard)
                {
                    if (Random.Range(0, 100) > 50)
                    {
                        isAttackDone = false;
                    }
                }
            }
        }
        yield return null;
    }

    private IEnumerator AttackPurple()
    {
        while (true)

        {
            isFirstTimeAttack = false;
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            purpleSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
            {
                if (allsquare[i].GetIdentity() == StateMortal.iden.LastColor)
                {
                    purpleSquare.Add(allsquare[i]);
                }
            }
            if (purpleSquare.Count == 0)
            {
                break;
            }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                purpleSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetIdentity() == StateMortal.iden.LastColor)
                    {
                        purpleSquare.Add(allsquare[i]);
                    }
                }
                if (purpleSquare.Count == 0)
                {
                    break;
                }
                int randomsquare = Random.Range(0, purpleSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try
                    {
                        if (purpleSquare[randomsquare].GetIdentity() == StateMortal.iden.LastColor)
                        {
                            List<StateMortal> TypeOfAttack = purpleSquare[randomsquare].MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].isUnderAttack) continue;

                                StateMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i];
                                StateMortal tempSquareIncreaseMortal = purpleSquare[randomsquare];

                                if (TypeOfAttack[i].GetIdentity() == StateMortal.iden.LastColor && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        purpleSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, purpleSquare[randomsquare].transform.position, thePlayer.LastColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetIdentity() != StateMortal.iden.LastColor)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].isUnderAttack = true;

                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        purpleSquare[randomsquare]
                                            .LineConnections(TypeOfAttack[i].gameObject, purpleSquare[randomsquare].transform.position, thePlayer.LastColor);


                                        purpleSquare[randomsquare].ArmyBurning(TypeOfAttack[i].gameObject,
                                        thePlayer.LastColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].SetIdentity(StateMortal.iden.LastColor);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i].gameObject);
                                            TypeOfAttack[i].SetMortalColors(thePlayer.LastColor, thePlayer.LastColorText);
                                            TypeOfAttack[i].ResetAllSkills();
                                            isAttackDone = true;
                                        }
                                        isAttackDone = true;
                                        TypeOfAttack[i].isUnderAttack = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                    }
                }
                else
                    isAttackDone = true;

                if (Difficaulty == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (Difficaulty == Difficault.Hard)
                {
                    if (Random.Range(0, 100) > 50)
                    {
                        isAttackDone = false;
                    }
                }
            }
        }
        yield return null;
    }
}
