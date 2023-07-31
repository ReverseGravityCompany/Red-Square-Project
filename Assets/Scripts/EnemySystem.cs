using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] public bool Red, Yellow, Pink, Green, Orange, Purple;
    private GameObject[] allsquare;
    private List<GameObject> RedSquare;
    private List<GameObject> yellowSquare;
    private List<GameObject> pinkSquare;
    private List<GameObject> greenSquare;
    private List<GameObject> OrangeSquare;
    private List<GameObject> purpleSquare;
    private WaitForSeconds Timer;
    public float TimebettweenAttacks;
    public float TimeForFirstTime;
    private bool isFirstTimeAttack = true;
    private Player thePlayer;

    public enum Difficault { Easy, Meduim, Hard }
    public Difficault Difficaulty;


    private EnemySystem()
    {
        RedSquare = new List<GameObject>();
        yellowSquare = new List<GameObject>();
        pinkSquare = new List<GameObject>();
        greenSquare = new List<GameObject>();
        OrangeSquare = new List<GameObject>();
        purpleSquare = new List<GameObject>();
    }


    private void Awake()
    {
        thePlayer = FindObjectOfType<Player>();
        allsquare = new GameObject[GameObject.FindGameObjectsWithTag("square").Length];
        for (int i = 0; i < allsquare.Length; i++)
        {
            allsquare[i] = GameObject.FindGameObjectsWithTag("square")[i].gameObject;
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

    public void LookAtList(GameObject obj, Identity.iden type)
    {
        switch (type)
        {
            case Identity.iden.Red:
                for (int i = 0; i < RedSquare.Count; i++)
                {
                    if (RedSquare[i] == obj)
                    {
                        RedSquare.Remove(obj);
                    }
                }
                break;
            case Identity.iden.Yellow:
                for (int i = 0; i < yellowSquare.Count; i++)
                {
                    if (yellowSquare[i] == obj)
                    {
                        yellowSquare.Remove(obj);
                    }
                }
                break;
            case Identity.iden.Pink:
                for (int i = 0; i < pinkSquare.Count; i++)
                {
                    if (pinkSquare[i] == obj)
                    {
                        pinkSquare.Remove(obj);
                    }
                }
                break;
            case Identity.iden.Green:
                for (int i = 0; i < greenSquare.Count; i++)
                {
                    if (greenSquare[i] == obj)
                    {
                        greenSquare.Remove(obj);
                    }
                }
                break;
            case Identity.iden.Orange:
                for (int i = 0; i < OrangeSquare.Count; i++)
                {
                    if (OrangeSquare[i] == obj)
                    {
                        OrangeSquare.Remove(obj);
                    }
                }
                break;
            case Identity.iden.LastColor:
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
                if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
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
                    if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
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
                        if (RedSquare[randomRed].GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
                        {
                            List<GameObject> TypeOfAttack = RedSquare[randomRed].GetComponent<StateMortal>().MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack) continue;

                                IncreaseMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i].GetComponent<IncreaseMortal>();
                                IncreaseMortal tempRedSquareIncreaseMortal = RedSquare[randomRed].GetComponent<IncreaseMortal>();


                                #region Add Enemy Square To Itself
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Red && Random.Range(0, 100) < 15)
                                {
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;
                                    int AttackDamage = tempRedSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempRedSquareIncreaseMortal.CurrentCount;
                                        tempRedSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;
                                        // Line Effect
                                        RedSquare[randomRed].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, RedSquare[randomRed].transform.position, thePlayer.RedColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
                                }
                                #endregion
                                #region Attack To Others
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() != Identity.iden.Red)
                                {
                                    if (tempRedSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

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
                                        RedSquare[randomRed].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, RedSquare[randomRed].transform.position, thePlayer.RedColor);

                                        if (objBurnValue > 12)
                                            RedSquare[randomRed].GetComponent<StateMortal>().ArmyBurning(TypeOfAttack[i].gameObject, thePlayer.RedColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                            TypeOfAttack[i].GetComponent<Image>().color = thePlayer.RedColor;
                                            TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
                                            TypeOfAttack[i].GetComponent<SquareClass>().ResetAllSkills();
                                            if (thePlayer.MyBlue != null)
                                            {
                                                thePlayer.MyBlueSquareClass.Check_Attack_N();
                                            }
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
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
                if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
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
                    if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
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
                        if (yellowSquare[randomsquare].GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
                        {
                            List<GameObject> TypeOfAttack = yellowSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack) continue;

                                IncreaseMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i].GetComponent<IncreaseMortal>();
                                IncreaseMortal tempSquareIncreaseMortal = yellowSquare[randomsquare].GetComponent<IncreaseMortal>();

                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        yellowSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, yellowSquare[randomsquare].transform.position, thePlayer.YellowColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() != Identity.iden.Yellow)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;
                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        yellowSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, yellowSquare[randomsquare].transform.position, thePlayer.YellowColor);

                                        if (objBurnValue > 12)
                                            yellowSquare[randomsquare].GetComponent<StateMortal>().ArmyBurning(TypeOfAttack[i].gameObject, thePlayer.YellowColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                            Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].GetComponent<Identity>().SetIdentity(Identity.iden.Yellow);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                            TypeOfAttack[i].GetComponent<Image>().color = thePlayer.YellowColor;
                                            TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.YellowColorText;
                                            TypeOfAttack[i].GetComponent<SquareClass>().ResetAllSkills();
                                            if (thePlayer.MyBlue != null)
                                            {
                                                thePlayer.MyBlueSquareClass.Check_Attack_N();
                                            }
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
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
                if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
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
                    if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
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
                        if (pinkSquare[randomsquare].GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
                        {
                            List<GameObject> TypeOfAttack = pinkSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack) continue;

                                IncreaseMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i].GetComponent<IncreaseMortal>();
                                IncreaseMortal tempSquareIncreaseMortal = pinkSquare[randomsquare].GetComponent<IncreaseMortal>();

                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Pink && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;
                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        pinkSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, pinkSquare[randomsquare].transform.position, thePlayer.PinkColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() != Identity.iden.Pink)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;
                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        pinkSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, pinkSquare[randomsquare].transform.position, thePlayer.PinkColor);

                                        if (objBurnValue > 12)
                                            pinkSquare[randomsquare].GetComponent<StateMortal>().ArmyBurning(TypeOfAttack[i].gameObject,
                                            thePlayer.PinkColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempSquareIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                                            tempSquareIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempSquareIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].GetComponent<Identity>().SetIdentity(Identity.iden.Pink);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                            TypeOfAttack[i].GetComponent<Image>().color = thePlayer.PinkColor;
                                            TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.PinkColorText;
                                            TypeOfAttack[i].GetComponent<SquareClass>().ResetAllSkills();
                                            if (thePlayer.MyBlue != null)
                                            {
                                                thePlayer.MyBlueSquareClass.Check_Attack_N();
                                            }
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
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
                if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
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
                    if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
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
                        if (greenSquare[randomsquare].GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
                        {
                            List<GameObject> TypeOfAttack = greenSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack) continue;

                                IncreaseMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i].GetComponent<IncreaseMortal>();
                                IncreaseMortal tempSquareIncreaseMortal = greenSquare[randomsquare].GetComponent<IncreaseMortal>();

                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Green && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        greenSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, greenSquare[randomsquare].transform.position, thePlayer.GreenColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() != Identity.iden.Green)
                                {
                                    if (greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        greenSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, greenSquare[randomsquare].transform.position, thePlayer.GreenColor);

                                        if (objBurnValue > 12)
                                            greenSquare[randomsquare].GetComponent<StateMortal>().ArmyBurning(TypeOfAttack[i].gameObject,
                                            thePlayer.GreenColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);


                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].GetComponent<Identity>().SetIdentity(Identity.iden.Green);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                            TypeOfAttack[i].GetComponent<Image>().color = thePlayer.GreenColor;
                                            TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.GreenColorText;
                                            TypeOfAttack[i].GetComponent<SquareClass>().ResetAllSkills();
                                            if (thePlayer.MyBlue != null)
                                            {
                                                thePlayer.MyBlueSquareClass.Check_Attack_N();
                                            }
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
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
                if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
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
                    if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
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
                        if (OrangeSquare[randomsquare].GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
                        {
                            List<GameObject> TypeOfAttack = OrangeSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone) continue;
                                if (TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack) continue;

                                IncreaseMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i].GetComponent<IncreaseMortal>();
                                IncreaseMortal tempSquareIncreaseMortal = OrangeSquare[randomsquare].GetComponent<IncreaseMortal>();

                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() == Identity.iden.Orange && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        OrangeSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, OrangeSquare[randomsquare].transform.position, thePlayer.OrangeColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() != Identity.iden.Orange)
                                {
                                    if (OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        OrangeSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, OrangeSquare[randomsquare].transform.position, thePlayer.OrangeColor);

                                        if (objBurnValue > 12)
                                            OrangeSquare[randomsquare].GetComponent<StateMortal>().ArmyBurning(TypeOfAttack[i].gameObject,
                                            thePlayer.OrangeColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].GetComponent<Identity>().SetIdentity(Identity.iden.Orange);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                            TypeOfAttack[i].GetComponent<Image>().color = thePlayer.OrangeColor;
                                            TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.OrangeColorText;
                                            TypeOfAttack[i].GetComponent<SquareClass>().ResetAllSkills();
                                            if (thePlayer.MyBlue != null)
                                            {
                                                thePlayer.MyBlueSquareClass.Check_Attack_N();
                                            }
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
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
                if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
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
                    if (allsquare[i].GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
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
                        if (purpleSquare[randomsquare].GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
                        {
                            List<GameObject> TypeOfAttack = purpleSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                            for (int i = 0; i < TypeOfAttack.Count; i++)
                            {
                                if (isAttackDone || TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack) continue;

                                IncreaseMortal tempTypeOfAttackIncreaseMortal = TypeOfAttack[i].GetComponent<IncreaseMortal>();
                                IncreaseMortal tempSquareIncreaseMortal = purpleSquare[randomsquare].GetComponent<IncreaseMortal>();

                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor && Random.Range(0, 100) < 10)
                                {
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

                                    int AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                    int CountOfattacking = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                    if ((AttackDamage += CountOfattacking) < 1000)
                                    {
                                        AttackDamage = tempSquareIncreaseMortal.CurrentCount;
                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount += AttackDamage;

                                        purpleSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, purpleSquare[randomsquare].transform.position, thePlayer.LastColor);

                                        isAttackDone = true;
                                    }
                                    TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
                                }
                                if (TypeOfAttack[i].GetComponent<Identity>().GetIdentity() != Identity.iden.LastColor)
                                {
                                    if (tempSquareIncreaseMortal.CurrentCount > tempTypeOfAttackIncreaseMortal.CurrentCount || Random.Range(0, 100) < 40)
                                    {
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = true;

                                        int AttackDamage = tempSquareIncreaseMortal.CurrentCount;

                                        int maxDamage = AttackDamage;
                                        if (AttackDamage > tempTypeOfAttackIncreaseMortal.CurrentCount)
                                        {
                                            maxDamage = tempTypeOfAttackIncreaseMortal.CurrentCount;
                                        }

                                        int objBurnValue = maxDamage;

                                        tempSquareIncreaseMortal.CurrentCount = 0;
                                        tempTypeOfAttackIncreaseMortal.CurrentCount -= AttackDamage;

                                        purpleSquare[randomsquare].GetComponent<StateMortal>()
                                            .LineConnections(TypeOfAttack[i].gameObject, purpleSquare[randomsquare].transform.position, thePlayer.LastColor);

                                        if (objBurnValue > 12)
                                            purpleSquare[randomsquare].GetComponent<StateMortal>().ArmyBurning(TypeOfAttack[i].gameObject,
                                            thePlayer.LastColor, TypeOfAttack[i].GetComponent<Image>().color, objBurnValue / 4);

                                        isAttackDone = true;
                                        if (tempTypeOfAttackIncreaseMortal.CurrentCount <= 0)
                                        {
                                            TypeOfAttack[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                                            tempTypeOfAttackIncreaseMortal.CurrentCount =
                                                Mathf.Abs(tempTypeOfAttackIncreaseMortal.CurrentCount);
                                            TypeOfAttack[i].GetComponent<Identity>().SetIdentity(Identity.iden.LastColor);
                                            thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                            TypeOfAttack[i].GetComponent<Image>().color = thePlayer.LastColor;
                                            TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.LastColorText;
                                            TypeOfAttack[i].GetComponent<SquareClass>().ResetAllSkills();
                                            if (thePlayer.MyBlue != null)
                                            {
                                                thePlayer.MyBlueSquareClass.Check_Attack_N();
                                            }
                                            isAttackDone = true;
                                        }
                                        TypeOfAttack[i].GetComponent<SquareClass>().isUnderAttack = false;
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
