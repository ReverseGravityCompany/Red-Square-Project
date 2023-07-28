using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using static EnemySystem;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator _Instance { get; private set; }

    public GameObject[] CenterMortals;
    public int MaxMortalGenerateCount;
    public int MinMortalGenerateCount;
    public int MortalGenerateCount;

    private float TimeBettweenAttacks;
    private float TimeForFirstTime;

    private EnemySystem MyEnemySystem;

    private bool Red, Yellow, Pink, Green, Orange, Purple;

    public List<GameObject> SelectedMortals;

    private int PathMoveAmount;
    private bool _PathEnding;

    private int _mistakeCount;

    private Identity[] AllMortalObjects;

    private int GeneratingCount;
    private int temp;


    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }
    }

    public void StartLevelGenerator()
    {
        MyEnemySystem = GetComponent<EnemySystem>();
        SelectedMortals = new List<GameObject>();
        _PathEnding = false;

        AllMortalObjects = new Identity[FindObjectsOfType<Identity>().Length];
        for (int i = 0; i < AllMortalObjects.Length; i++)
        {
            AllMortalObjects[i] = FindObjectsOfType<Identity>()[i];
        }

        // Mortal Count
        MortalGenerateCount = MortalGenerator_Count(MaxMortalGenerateCount, MinMortalGenerateCount);
        print(MortalGenerateCount);
        temp = 0;
        GeneratingCount = MortalGenerateCount;

        // Difficaulty System
        MyEnemySystem.Difficaulty = SelectDifficualty();

        // Enemy Timing
        SetEnemyAttackTiming();

        // Which Enemy ??
        SetEnemies();

        // Set Mortals
        int CenterMortal = Random.Range(0, CenterMortals.Length);
        PathMoveAmount = 0;
        _mistakeCount = 0;
        SelectedMortals.Add(CenterMortals[CenterMortal].gameObject);
        SetMortalPath(CenterMortals[CenterMortal].gameObject);
    }

    private int MortalGenerator_Count(int max, int min)
    {
        if (Random.Range(0, 100) < 50)
        {
            return Random.Range(min, 15);
        }
        else
        {
            return Random.Range(min, max);
        }
    }

    private Difficault SelectDifficualty()
    {
        int randomDif = Random.Range(0, 100);
        if (randomDif < 10)
        {
            return Difficault.Hard;
        }
        else if (randomDif >= 10 && randomDif <= 50)
        {
            return Difficault.Meduim;
        }
        else
        {
            return Difficault.Easy;
        }
    }

    private void SetEnemyAttackTiming()
    {
        TimeBettweenAttacks = Random.Range(0.95f, 4.35f);
        TimeForFirstTime = Random.Range(-0.5f, 1.5f);

        MyEnemySystem.TimebettweenAttacks = TimeBettweenAttacks;
        MyEnemySystem.TimeForFirstTime = TimeForFirstTime;
    }

    private void SetEnemies()
    {
        int enemyCount = Random.Range(1, 7);

        if (Random.Range(0, 100) <= 50)
        {
            enemyCount = 1;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            bool Summon = false;
            while (!Summon)
            {
                int EnemyIndex = Random.Range(1, 7);
                switch (EnemyIndex)
                {
                    case 1:
                        if (!Red)
                            Red = true;
                        else
                            continue;
                        break;

                    case 2:
                        if (!Yellow)
                            Yellow = true;
                        else
                            continue;
                        break;

                    case 3:
                        if (!Pink)
                            Pink = true;
                        else
                            continue;
                        break;

                    case 4:
                        if (!Green)
                            Green = true;
                        else
                            continue;
                        break;

                    case 5:
                        if (!Orange)
                            Orange = true;
                        else
                            continue;
                        break;

                    case 6:
                        if (!Purple)
                            Purple = true;
                        else
                            continue;
                        break;

                    default:
                        if (!Red)
                            Red = true;
                        else
                            continue;
                        break;
                }
                Summon = true;
            }
        }

        MyEnemySystem.Red = Red;
        MyEnemySystem.Yellow = Yellow;
        MyEnemySystem.Pink = Pink;
        MyEnemySystem.Green = Green;
        MyEnemySystem.Orange = Orange;
        MyEnemySystem.Purple = Purple;

        if (!Red && !Yellow && !Pink && !Green && !Orange && !Purple)
            MyEnemySystem.Red = true;
    }

    private void SetMortalPath(GameObject Mortal)
    {
        PathMoveAmount++;
        int pathMove = PathMoveAmount;
        Mortal.gameObject.SetActive(true);

        StartCoroutine(MortalPath(Mortal, pathMove));
    }

    private IEnumerator MortalPath(GameObject Mortal, int PathMove)
    {
        if (MortalGenerateCount <= 0)
        {
            PathEnding();
            yield return null;
        }

        StateMortal MortalStats = Mortal.GetComponent<StateMortal>();

        int randomAmount = 50;
        int PickedAmount = 0;

        for (int i = 0; i < MortalStats.MyTypeOfAttack.Count; i++)
        {
            if (MortalGenerateCount <= 0)
            {
                PathEnding();
                yield return null;
            }

            switch (PickedAmount)
            {
                case 0:
                    randomAmount = 50;
                    break;
                case 1:
                    randomAmount = 40;
                    break;
                case 2:
                    randomAmount = 30;
                    break;
                case 3:
                    randomAmount = 20;
                    break;
                default:
                    randomAmount = 20;
                    break;
            }
            if (Random.Range(0, 100) < randomAmount)
            {
                SelectedMortals.Add(MortalStats.MyTypeOfAttack[i].gameObject);
                MortalGenerateCount--;
                PickedAmount++;
                temp++;

                if (MortalGenerateCount <= 0)
                {
                    PathEnding();
                    yield return null;
                }
            }
        }

        //if (PathMoveAmount == 1 && SelectedMortals.Count <= 1)
        //{
        //    yield return StartCoroutine(MortalPath(Mortal, 1));
        //}

        //if (PathMove == SelectedMortals.Count)
        //{
        //    if (GeneratingCount == SelectedMortals.Count)
        //    {
        //        PathEnding();
        //        yield return null;
        //    }
        //    else
        //    {
        //        if (_mistakeCount < GeneratingCount)
        //        {
        //            SetMortalPath(SelectedMortals[_mistakeCount]);
        //            _mistakeCount++;
        //            yield return null;
        //        }
        //        else
        //        {
        //            PathEnding();
        //            yield return null;
        //        }
        //    }
        //}

        if (PathMove < temp)
            SetMortalPath(SelectedMortals[PathMove].gameObject);

        yield return null;
    }


    private void PathEnding()
    {
        if (!_PathEnding)
        {
            _PathEnding = true;
            int tempData = 0;
            // Delete All Mortals and keep the selectedMortals
            for (int i = 0; i < AllMortalObjects.Length; i++)
            {
                tempData = 0;
                for (int j = 0; j < SelectedMortals.Count; j++)
                {
                    if (AllMortalObjects[i].gameObject != SelectedMortals[j].gameObject)
                    {
                        tempData++;
                    }
                }
                if (tempData == SelectedMortals.Count)
                {
                    Destroy(AllMortalObjects[i].gameObject);
                }
            }

            // Reset All Mortal Data
            AllMortalObjects = new Identity[FindObjectsOfType<Identity>().Length];
            for (int i = 0; i < AllMortalObjects.Length; i++)
            {
                AllMortalObjects[i] = FindObjectsOfType<Identity>()[i];
                AllMortalObjects[i].SetIdentity(Identity.iden.None);
                AllMortalObjects[i].GetComponent<StateMortal>().ResetTypeOfAttackData();
                AllMortalObjects[i].GetComponent<IncreaseMortal>().CurrentCount = 0;
            }


            SelectedMortals[0].GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
            SelectedMortals[0].GetComponent<IncreaseMortal>().CurrentCount = 10;

            MyEnemySystem.Red = true;
            SelectedMortals[SelectedMortals.Count - 1].GetComponent<Identity>().SetIdentity(Identity.iden.Red);
            SelectedMortals[SelectedMortals.Count - 1].GetComponent<IncreaseMortal>().CurrentCount = 25;

            LevelManager._Instance.isGeneratingLevel = false;
        }
    }
}
