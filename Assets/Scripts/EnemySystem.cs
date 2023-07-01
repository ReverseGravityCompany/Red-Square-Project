using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private bool Blue, Yellow, Pink, Green, Orange, Purple;
    private GameObject[] allsquare;
    private List<GameObject> blueSquare;
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

    public enum Difficault { Easy , Meduim , Hard}
    public  Difficault dif;


    private EnemySystem()
    {
        blueSquare = new List<GameObject>();
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
            //if(Blue && allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Blue)
            //{
            //    blueSquare.Add(allsquare[i]);
            //}
            //if (Yellow && allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
            //{
            //    yellowSquare.Add(allsquare[i]);
            //}
            //if (Pink && allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Pink)
            //{
            //    pinkSquare.Add(allsquare[i]);
            //}
            //if (Green && allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Green)
            //{
            //    greenSquare.Add(allsquare[i]);
            //}
            //if (Orange && allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Orange)
            //{
            //    OrangeSquare.Add(allsquare[i]);
            //}
            //if (Purple && allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
            //{
            //    purpleSquare.Add(allsquare[i]);
            //}
        }

        Timer = new WaitForSeconds(TimebettweenAttacks + TimeForFirstTime);
    }

    public void GenerateEnemy()
    {
        if (Blue)
            StartCoroutine(AttackBlue());
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

    public void LookAtList(GameObject obj , Identity.iden type){
        switch(type){
        case Identity.iden.Blue:
        for(int i = 0; i < blueSquare.Count;i++){
            if(blueSquare[i] == obj){
               blueSquare.Remove(obj);
            }
        }
        break;
        case Identity.iden.Yellow:
        for(int i = 0; i < yellowSquare.Count;i++){
            if(yellowSquare[i] == obj){
               yellowSquare.Remove(obj);
            }
        }
        break;
        case Identity.iden.Pink:
        for(int i = 0; i < pinkSquare.Count;i++){
            if(pinkSquare[i] == obj){
               pinkSquare.Remove(obj);
            }
        }
        break;
        case Identity.iden.Green:
        for(int i = 0; i < greenSquare.Count;i++){
            if(greenSquare[i] == obj){
               greenSquare.Remove(obj);
            }
        }
        break;
        case Identity.iden.Orange:
        for(int i = 0; i < OrangeSquare.Count;i++){
            if(OrangeSquare[i] == obj){
               OrangeSquare.Remove(obj);
            }
        }
        break;
        case Identity.iden.LastColor:
        for(int i = 0; i < purpleSquare.Count;i++){
            if(purpleSquare[i] == obj){
               purpleSquare.Remove(obj);
            }
        }
        break;
        }

    }
    private IEnumerator AttackBlue()
    {
        while (true)
        {
            if (!isFirstTimeAttack)
                Timer = new WaitForSeconds(TimebettweenAttacks);
            yield return Timer;
            blueSquare.Clear();
            for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Blue)
                    {
                        blueSquare.Add(allsquare[i]);
                    }
                }
                if (blueSquare.Count == 0)
                {
                    break;
                }
            // Start Choose & Attack
            bool isAttackDone = false;
            while (!isAttackDone)
            {
                isFirstTimeAttack = false;
                blueSquare.Clear();
                for (int i = 0; i < allsquare.Length; i++)
                {
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Blue)
                    {
                        blueSquare.Add(allsquare[i]);
                    }
                }
                if (blueSquare.Count == 0)
                {
                    break;
                }
                int randomBlue = Random.Range(0, blueSquare.Count);
                if (Random.Range(0, 100) < 70)
                {
                    try{
                    if (blueSquare[randomBlue].GetComponent<Identity>().GetIden() == Identity.iden.Blue)
                    {
                        List<GameObject> TypeOfAttack = blueSquare[randomBlue].GetComponent<StateMortal>().MyTypeOfAttack;
                        for (int i = 0; i < TypeOfAttack.Count; i++)
                        {
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() == Identity.iden.Blue)
                            {
                                int AttackDamage = blueSquare[randomBlue].GetComponent<IncreaseMortal>().CurrentCount;
                                int CountOfattacking = TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount;
                                if (Random.Range(0, 100) < 10 && (AttackDamage += CountOfattacking) < 1000)
                                {
                                    AttackDamage = blueSquare[randomBlue].GetComponent<IncreaseMortal>().CurrentCount;
                                    blueSquare[randomBlue].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
                                    isAttackDone = true;
                                }
                            }
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() != Identity.iden.Blue)
                            {
                                if (blueSquare[randomBlue].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                {
                                    int AttackDamage = blueSquare[randomBlue].GetComponent<IncreaseMortal>().CurrentCount;
                                    blueSquare[randomBlue].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
                                    isAttackDone = true;
                                    if (TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount <= 0)
                                    {
                                        TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount =
                                            Mathf.Abs(TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount);
                                        TypeOfAttack[i].GetComponent<Identity>().SetIden(Identity.iden.Blue);
                                        thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                        TypeOfAttack[i].GetComponent<Image>().color = thePlayer.BlueColor;
                                        TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.BlueColorText;
                                        TypeOfAttack[i].GetComponent<SquareClass>().TurboMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CopacityMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().RandomChangeMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().AllAttackMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().X2Mortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().MaxSpaceMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CheckAllSkills();
                                        if (thePlayer.MyRed != null)
                                        {
                                            thePlayer.MyRed.GetComponent<SquareClass>().CheckCanWhoAttack();
                                        }
                                        isAttackDone = true;
                                    }
                                }
                            }
                        }
                    }
                    }catch(System.ArgumentOutOfRangeException){

                    }
                }
                if(dif == Difficault.Meduim)
                {
                    if(Random.Range(0,100) > 90)
                    {
                        isAttackDone = false;
                    }
                }
                else if(dif == Difficault.Hard)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
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
                    try{
                    if (yellowSquare[randomsquare].GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
                    {
                        List<GameObject> TypeOfAttack = yellowSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                        for (int i = 0; i < TypeOfAttack.Count; i++)
                        {
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
                            {
                                    int AttackDamage = yellowSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    int CountOfattacking = TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount;
                                if (Random.Range(0, 100) < 10 && (AttackDamage += CountOfattacking) < 1000)
                                {
                                    AttackDamage = yellowSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    yellowSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
                                    isAttackDone = true;
                                }
                            }
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() != Identity.iden.Yellow)
                            {
                                if (yellowSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                {
                                    int AttackDamage = yellowSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    yellowSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
                                    isAttackDone = true;
                                    if (TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount <= 0)
                                    {
                                        TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount =
                                            Mathf.Abs(TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount);
                                        TypeOfAttack[i].GetComponent<Identity>().SetIden(Identity.iden.Yellow);
                                        thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                        TypeOfAttack[i].GetComponent<Image>().color = thePlayer.YellowColor;
                                        TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.YellowColorText;
                                        TypeOfAttack[i].GetComponent<SquareClass>().TurboMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CopacityMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().RandomChangeMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().AllAttackMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().X2Mortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().MaxSpaceMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CheckAllSkills();
                                        if (thePlayer.MyRed != null)
                                        {
                                            thePlayer.MyRed.GetComponent<SquareClass>().CheckCanWhoAttack();
                                        }
                                        isAttackDone = true;
                                    }
                                }
                            }
                        }
                    }
                    }catch(System.ArgumentOutOfRangeException){

                    }
                }
                if (dif == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 70)
                    {
                        isAttackDone = false;
                    }
                }
                else if (dif == Difficault.Hard)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Pink)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Pink)
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
                    try{
                    if (pinkSquare[randomsquare].GetComponent<Identity>().GetIden() == Identity.iden.Pink)
                    {
                        List<GameObject> TypeOfAttack = pinkSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                        for (int i = 0; i < TypeOfAttack.Count; i++)
                        {
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() == Identity.iden.Pink)
                            {
                                    int AttackDamage = pinkSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    int CountOfattacking = TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount;
                                if (Random.Range(0, 100) < 10 && (AttackDamage += CountOfattacking) < 1000)
                                {
                                    AttackDamage = pinkSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    pinkSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
                                    isAttackDone = true;
                                }
                            }
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() != Identity.iden.Pink)
                            {
                                if (pinkSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                {
                                    int AttackDamage = pinkSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    pinkSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
                                    isAttackDone = true;
                                    if (TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount <= 0)
                                    {
                                        TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount =
                                            Mathf.Abs(TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount);
                                        TypeOfAttack[i].GetComponent<Identity>().SetIden(Identity.iden.Pink);
                                        thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                        TypeOfAttack[i].GetComponent<Image>().color = thePlayer.PinkColor;
                                        TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.PinkColorText;
                                        TypeOfAttack[i].GetComponent<SquareClass>().TurboMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CopacityMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().RandomChangeMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().AllAttackMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().X2Mortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().MaxSpaceMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CheckAllSkills();
                                        if (thePlayer.MyRed != null)
                                        {
                                            thePlayer.MyRed.GetComponent<SquareClass>().CheckCanWhoAttack();
                                        }
                                        isAttackDone = true;
                                    }
                                }
                            }
                        }
                    }
                    }catch(System.ArgumentOutOfRangeException){

                    }
                }
                if (dif == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (dif == Difficault.Hard)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Green)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Green)
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
                    try{
                    if (greenSquare[randomsquare].GetComponent<Identity>().GetIden() == Identity.iden.Green)
                    {
                        List<GameObject> TypeOfAttack = greenSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                        for (int i = 0; i < TypeOfAttack.Count; i++)
                        {
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() == Identity.iden.Green)
                            {
                                int AttackDamage = greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                int CountOfattacking = TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount;
                                if (Random.Range(0, 100) < 10 && (AttackDamage += CountOfattacking) < 1000)
                                {
                                    AttackDamage = greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
                                    isAttackDone = true;
                                }
                            }
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() != Identity.iden.Green)
                            {
                                if (greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                {
                                    int AttackDamage = greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    greenSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
                                    isAttackDone = true;
                                    if (TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount <= 0)
                                    {
                                        TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount =
                                            Mathf.Abs(TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount);
                                        TypeOfAttack[i].GetComponent<Identity>().SetIden(Identity.iden.Green);
                                        thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                        TypeOfAttack[i].GetComponent<Image>().color = thePlayer.GreenColor;
                                        TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.GreenColorText;
                                        TypeOfAttack[i].GetComponent<SquareClass>().TurboMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CopacityMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().RandomChangeMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().AllAttackMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().X2Mortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().MaxSpaceMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CheckAllSkills();
                                        if (thePlayer.MyRed != null)
                                        {
                                            thePlayer.MyRed.GetComponent<SquareClass>().CheckCanWhoAttack();
                                        }
                                        isAttackDone = true;
                                    }
                                }
                            }
                        }
                    }
                    }catch(System.ArgumentOutOfRangeException){

                    }
                }
                if (dif == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (dif == Difficault.Hard)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Orange)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.Orange)
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
                    try{
                    if (OrangeSquare[randomsquare].GetComponent<Identity>().GetIden() == Identity.iden.Orange)
                    {
                        List<GameObject> TypeOfAttack = OrangeSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                        for (int i = 0; i < TypeOfAttack.Count; i++)
                        {
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() == Identity.iden.Orange)
                            {
                                int AttackDamage = OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                int CountOfattacking = TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount;
                                if (Random.Range(0, 100) < 10 && (AttackDamage += CountOfattacking) < 1000)
                                {
                                    AttackDamage = OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
                                    isAttackDone = true;
                                }
                            }
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() != Identity.iden.Orange)
                            {
                                if (OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                {
                                    int AttackDamage = OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    OrangeSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
                                    isAttackDone = true;
                                    if (TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount <= 0)
                                    {
                                        TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount =
                                            Mathf.Abs(TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount);
                                        TypeOfAttack[i].GetComponent<Identity>().SetIden(Identity.iden.Orange);
                                        thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                        TypeOfAttack[i].GetComponent<Image>().color = thePlayer.OrangeColor;
                                        TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.OrangeColorText;
                                        TypeOfAttack[i].GetComponent<SquareClass>().TurboMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CopacityMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().RandomChangeMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().AllAttackMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().X2Mortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().MaxSpaceMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CheckAllSkills();
                                        if (thePlayer.MyRed != null)
                                        {
                                            thePlayer.MyRed.GetComponent<SquareClass>().CheckCanWhoAttack();
                                        }
                                        isAttackDone = true;
                                    }
                                }
                            }
                        }
                    }
                    }catch(System.ArgumentOutOfRangeException){

                    }
                }
                if (dif == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (dif == Difficault.Hard)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
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
                    if (allsquare[i].GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
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
                    try{
                    if (purpleSquare[randomsquare].GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
                    {
                        List<GameObject> TypeOfAttack = purpleSquare[randomsquare].GetComponent<StateMortal>().MyTypeOfAttack;
                        for (int i = 0; i < TypeOfAttack.Count; i++)
                        {
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
                            {
                                    int AttackDamage = purpleSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                     int CountOfattacking = TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount;
                                if (Random.Range(0, 100) < 10 && (AttackDamage += CountOfattacking) < 1000)
                                {
                                    AttackDamage = purpleSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    purpleSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
                                    isAttackDone = true;
                                }
                            }
                            if (TypeOfAttack[i].GetComponent<Identity>().GetIden() != Identity.iden.LastColor)
                            {
                                if (purpleSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount > TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount || Random.Range(0, 100) < 40)
                                {
                                    int AttackDamage = purpleSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount;
                                    purpleSquare[randomsquare].GetComponent<IncreaseMortal>().CurrentCount = 0;
                                    TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
                                    isAttackDone = true;
                                    if (TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount <= 0)
                                    {
                                        TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount =
                                            Mathf.Abs(TypeOfAttack[i].GetComponent<IncreaseMortal>().CurrentCount);
                                        TypeOfAttack[i].GetComponent<Identity>().SetIden(Identity.iden.LastColor);
                                        thePlayer.RenederAllAgain(TypeOfAttack[i]);
                                        TypeOfAttack[i].GetComponent<Image>().color = thePlayer.LastColor;
                                        TypeOfAttack[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.LastColorText;
                                        TypeOfAttack[i].GetComponent<SquareClass>().TurboMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CopacityMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().RandomChangeMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().AllAttackMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().X2Mortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().MaxSpaceMortal = false;
                                        TypeOfAttack[i].GetComponent<SquareClass>().CheckAllSkills();
                                        if (thePlayer.MyRed != null)
                                        {
                                            thePlayer.MyRed.GetComponent<SquareClass>().CheckCanWhoAttack();
                                        }
                                        isAttackDone = true;
                                    }
                                }
                            }
                        }
                    }
                    }catch(System.ArgumentOutOfRangeException){

                    }
                }
                if (dif == Difficault.Meduim)
                {
                    if (Random.Range(0, 100) > 80)
                    {
                        isAttackDone = false;
                    }
                }
                else if (dif == Difficault.Hard)
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
