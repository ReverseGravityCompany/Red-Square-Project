using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mortal : StrategyMortal
{
    // // public StrategyMortal.Strategy thisMortalStrategy;

    // // private Player thePlayer;
    // // private AllMortal allMortal;
    // // private Collider2D col;
    // // public bool CanAttack = false;
    // // private MenuSetting menuSetting;
    // // private LevelManager theLevelManager;
    // // public GameObject Turbo, Copacity, RandomChange, AllAttack, X2, MaxSpace;
    // // private Skills skills;
    // // public bool TurboMortal, CopacityMortal, RandomChangeMortal, AllAttackMortal, X2Mortal, MaxSpaceMortal;
    // // public Color WhiteLow;
    // // public GameObject TouchScreen;
    // // [HideInInspector] public GameObject star1, star2, star3;
    // // [HideInInspector] public Color MyCountNumberColor;
    // // public Color InvisibaleColor;

    // // public bool StopCouting;
    // // public bool StopCounting2;
    // // public bool StopCounting3;
    // // public bool DontWorkForNow;

    // // public AudioSource ClickMortalSound;

    // // public GameObject[] stateMortal;
    // // public bool StateWork;
    // // private bool redStop;
    // // public CameraMovement CamMove;



    // // void Awake()
    // // {
    // //     CamMove = FindObjectOfType<CameraMovement>();
    // //     skills = FindObjectOfType<Skills>();
    // //     thePlayer = FindObjectOfType<Player>();
    // //     allMortal = FindObjectOfType<AllMortal>();
    // //     col = GetComponent<Collider2D>();
    // //     menuSetting = FindObjectOfType<MenuSetting>();
    // //     theLevelManager = FindObjectOfType<LevelManager>();
    // //     if (StateWork)
    // //     {
    // //         stateMortal = new GameObject[gameObject.GetComponent<StateMortal>().MyTypeOfAttack.Length];
    // //         for (int i = 0; i < stateMortal.Length; i++)
    // //         {
    // //             stateMortal[i] = gameObject.GetComponent<StateMortal>().MyTypeOfAttack[i];
    // //         }
    // //     }
    // //     star1 = transform.Find("Star1").gameObject;
    // //     star2 = transform.Find("Star2").gameObject;
    // //     star3 = transform.Find("Star3").gameObject;


    // // }


    // //public void OnPointerClicking()
    // //{
    // //    GameObject eventData = EventSystem.current.currentSelectedGameObject;
    // //    print(eventData);
    // //    if (Time.timeScale != 0)
    // //    {
    // //        if (theLevelManager.Win.activeInHierarchy || theLevelManager.Lose.activeInHierarchy)
    // //        {
    // //            return;
    // //        }
    // //        GameObject ImTouched = eventData;
    // //        if (ImTouched == null)
    // //        {
    // //            return;
    // //        }
    // //        if (ImTouched.gameObject.name == "CountMortal")
    // //        {
    // //            thePlayer.MyRed = null;
    // //            return;
    // //        }
    // //        if (!menuSetting.Started)
    // //        {
    // //            return;
    // //        }

    // //        print("What");



    // //        #region None Attack
    // //        if (Identity.iden.None == GetComponent<Identity>().GetIden())
    // //        {
    // //            if (thePlayer.CanPush && CanAttack)
    // //            {
    // //                if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    // //                {
    // //                    if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    // //                    {
    // //                        if (Identity.iden.None == GetComponent<Identity>().GetIden())
    // //                        {
    // //                            int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    // //                            thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    // //                            if (ImTouched != null)
    // //                                ImTouched.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    // //                        }
    // //                        if (ImTouched.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    // //                        {
    // //                            ImTouched.GetComponent<IncreaseMortal>().CurrentCount =
    // //                                Mathf.Abs(ImTouched.GetComponent<IncreaseMortal>().CurrentCount);
    // //                            ImTouched.GetComponent<Identity>().SetIden(Identity.iden.Red);
    // //                            ImTouched.GetComponent<Image>().color = thePlayer.RedColor;
    // //                            ImTouched.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        }

    // //                        thePlayer.PickUp = !thePlayer.PickUp;
    // //                        thePlayer.CanPush = false;
    // //                        thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                        thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        theLevelManager.SkillsOn = false;
    // //                        skills.allAttack = false;
    // //                        skills.copacity = false;
    // //                        skills.turbo = false;
    // //                        skills.randomChange = false;
    // //                        skills.X2 = false;
    // //                        skills.MaxSpace = false;
    // //                        #region[2,2]
    // //                        if (allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                            for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                            {
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                            }

    // //                        }
    // //                        thePlayer.MyRed = null;
    // //                        #endregion
    // //                        #region [All Mortal]
    // //                        if (!allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                            }

    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                            }
    // //                        }
    // //                        #endregion
    // //                        return;
    // //                    }

    // //                }
    // //                else
    // //                {
    // //                    thePlayer.PickUp = !thePlayer.PickUp;
    // //                    thePlayer.CanPush = false;
    // //                    #region [2,2]
    // //                    if (allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                        for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                        {
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                        }

    // //                    }
    // //                    thePlayer.MyRed = null;
    // //                    #endregion
    // //                    #region [All Mortal]
    // //                    if (!allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                        }

    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                        }
    // //                    }
    // //                    #endregion
    // //                    return;
    // //                }

    // //            }
    // //        }
    // //        #endregion

    // //        #region Blue Attack
    // //        if (Identity.iden.Blue == GetComponent<Identity>().GetIden())
    // //        {
    // //            if (thePlayer.CanPush && CanAttack)
    // //            {
    // //                if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    // //                {
    // //                    if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    // //                    {
    // //                        if (Identity.iden.Blue == GetComponent<Identity>().GetIden())
    // //                        {
    // //                            int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    // //                            thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    // //                            if (ImTouched != null)
    // //                                ImTouched.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    // //                        }
    // //                        if (ImTouched.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    // //                        {
    // //                            ImTouched.GetComponent<IncreaseMortal>().CurrentCount =
    // //                                Mathf.Abs(ImTouched.GetComponent<IncreaseMortal>().CurrentCount);
    // //                            ImTouched.GetComponent<Identity>().SetIden(Identity.iden.Red);
    // //                            ImTouched.GetComponent<Image>().color = thePlayer.RedColor;
    // //                            ImTouched.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        }

    // //                        thePlayer.PickUp = !thePlayer.PickUp;
    // //                        thePlayer.CanPush = false;
    // //                        thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                        thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        theLevelManager.SkillsOn = false;
    // //                        skills.allAttack = false;
    // //                        skills.copacity = false;
    // //                        skills.turbo = false;
    // //                        skills.randomChange = false;
    // //                        skills.X2 = false;
    // //                        skills.MaxSpace = false;
    // //                        #region[2,2]
    // //                        if (allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                            for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                            {
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                            }

    // //                        }
    // //                        thePlayer.MyRed = null;
    // //                        #endregion
    // //                        #region [All Mortal]
    // //                        if (!allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                            }

    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                            }
    // //                        }
    // //                        #endregion
    // //                        return;
    // //                    }
    // //                }
    // //                else
    // //                {

    // //                    thePlayer.PickUp = !thePlayer.PickUp;
    // //                    thePlayer.CanPush = false;
    // //                    #region[2,2]
    // //                    if (allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                        for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                        {
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                        }

    // //                    }
    // //                    thePlayer.MyRed = null;
    // //                    #endregion
    // //                    #region [All Mortal]
    // //                    if (!allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                        }

    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                        }
    // //                    }
    // //                    #endregion
    // //                    return;
    // //                }
    // //            }
    // //        }
    // //        #endregion

    // //        #region Yellow Attack
    // //        if (Identity.iden.Yellow == GetComponent<Identity>().GetIden())
    // //        {
    // //            if (thePlayer.CanPush && CanAttack)
    // //            {
    // //                if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    // //                {
    // //                    if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    // //                    {
    // //                        if (Identity.iden.Yellow == GetComponent<Identity>().GetIden())
    // //                        {
    // //                            int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    // //                            thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    // //                            if (ImTouched != null)
    // //                                ImTouched.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    // //                        }
    // //                        if (ImTouched.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    // //                        {
    // //                            ImTouched.GetComponent<IncreaseMortal>().CurrentCount =
    // //                                Mathf.Abs(ImTouched.GetComponent<IncreaseMortal>().CurrentCount);
    // //                            ImTouched.GetComponent<Identity>().SetIden(Identity.iden.Red);
    // //                            ImTouched.GetComponent<Image>().color = thePlayer.RedColor;
    // //                            ImTouched.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        }

    // //                        thePlayer.PickUp = !thePlayer.PickUp;
    // //                        thePlayer.CanPush = false;
    // //                        thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                        thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        theLevelManager.SkillsOn = false;
    // //                        skills.allAttack = false;
    // //                        skills.copacity = false;
    // //                        skills.turbo = false;
    // //                        skills.randomChange = false;
    // //                        skills.X2 = false;
    // //                        skills.MaxSpace = false;
    // //                        #region[2,2]
    // //                        if (allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                            for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                            {
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                            }

    // //                        }
    // //                        thePlayer.MyRed = null;
    // //                        #endregion
    // //                        #region [All Mortal]
    // //                        if (!allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                            }

    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                            }
    // //                        }
    // //                        #endregion
    // //                        return;
    // //                    }
    // //                }
    // //                else
    // //                {

    // //                    thePlayer.PickUp = !thePlayer.PickUp;
    // //                    thePlayer.CanPush = false;
    // //                    #region[2,2]
    // //                    if (allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                        for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                        {
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                        }

    // //                    }
    // //                    thePlayer.MyRed = null;
    // //                    #endregion
    // //                    #region [All Mortal]
    // //                    if (!allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                        }

    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                        }
    // //                    }
    // //                    #endregion
    // //                    return;
    // //                }
    // //            }
    // //        }
    // //        #endregion

    // //        #region Pink Attack
    // //        if (Identity.iden.Pink == GetComponent<Identity>().GetIden())
    // //        {
    // //            if (thePlayer.CanPush && CanAttack)
    // //            {
    // //                if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    // //                {
    // //                    if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    // //                    {
    // //                        if (Identity.iden.Pink == GetComponent<Identity>().GetIden())
    // //                        {
    // //                            int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    // //                            thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    // //                            if (ImTouched != null)
    // //                                ImTouched.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    // //                        }
    // //                        if (ImTouched.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    // //                        {
    // //                            ImTouched.GetComponent<IncreaseMortal>().CurrentCount =
    // //                                Mathf.Abs(ImTouched.GetComponent<IncreaseMortal>().CurrentCount);
    // //                            ImTouched.GetComponent<Identity>().SetIden(Identity.iden.Red);
    // //                            ImTouched.GetComponent<Image>().color = thePlayer.RedColor;
    // //                            ImTouched.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        }

    // //                        thePlayer.PickUp = !thePlayer.PickUp;
    // //                        thePlayer.CanPush = false;
    // //                        thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                        thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        theLevelManager.SkillsOn = false;
    // //                        skills.allAttack = false;
    // //                        skills.copacity = false;
    // //                        skills.turbo = false;
    // //                        skills.randomChange = false;
    // //                        skills.X2 = false;
    // //                        skills.MaxSpace = false;
    // //                        #region[2,2]
    // //                        if (allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                            for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                            {
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                            }

    // //                        }
    // //                        thePlayer.MyRed = null;
    // //                        #endregion
    // //                        #region [All Mortal]
    // //                        if (!allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                            }

    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                            }
    // //                        }
    // //                        #endregion
    // //                        return;
    // //                    }
    // //                }
    // //                else
    // //                {

    // //                    thePlayer.PickUp = !thePlayer.PickUp;
    // //                    thePlayer.CanPush = false;
    // //                    #region[2,2]
    // //                    if (allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                        for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                        {
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                        }

    // //                    }
    // //                    thePlayer.MyRed = null;
    // //                    #endregion
    // //                    #region [All Mortal]
    // //                    if (!allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                        }

    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                        }
    // //                    }
    // //                    #endregion
    // //                    return;
    // //                }
    // //            }
    // //        }
    // //        #endregion

    // //        #region RedAdd
    // //        if (Identity.iden.Red == GetComponent<Identity>().GetIden())
    // //        {
    // //            ClickMortalSound.Play();
    // //            if (thePlayer.CanPush && CanAttack)
    // //            {
    // //                if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    // //                {
    // //                    if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    // //                    {
    // //                        if (Identity.iden.Red == GetComponent<Identity>().GetIden())
    // //                        {
    // //                            int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    // //                            thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    // //                            if (ImTouched != null)
    // //                                ImTouched.GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
    // //                        }
    // //                        thePlayer.PickUp = !thePlayer.PickUp;
    // //                        thePlayer.CanPush = false;
    // //                        thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                        thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                        theLevelManager.SkillsOn = false;
    // //                        skills.allAttack = false;
    // //                        skills.copacity = false;
    // //                        skills.turbo = false;
    // //                        skills.randomChange = false;
    // //                        skills.X2 = false;
    // //                        skills.MaxSpace = false;
    // //                        #region[2,2]
    // //                        if (allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                            for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                            {
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                                TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                            }

    // //                        }
    // //                        thePlayer.MyRed = null;
    // //                        #endregion
    // //                        #region [All Mortal]
    // //                        if (!allMortal.TwoAndTwo)
    // //                        {
    // //                            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                            }

    // //                            for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                            {
    // //                                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                            }
    // //                        }
    // //                        #endregion
    // //                        return;
    // //                    }

    // //                }
    // //                else
    // //                {
    // //                    thePlayer.PickUp = !thePlayer.PickUp;
    // //                    thePlayer.CanPush = false;
    // //                    #region[2,2]
    // //                    if (allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                        for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                        {
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;

    // //                        }

    // //                    }
    // //                    thePlayer.MyRed = null;
    // //                    #endregion
    // //                    #region [All Mortal]
    // //                    if (!allMortal.TwoAndTwo)
    // //                    {
    // //                        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                        }

    // //                        for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                        {
    // //                            ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                        }
    // //                    }
    // //                    #endregion
    // //                    return;
    // //                }
    // //            }
    // //        }
    // //        #endregion

    // //        if (Identity.iden.Red == GetComponent<Identity>().GetIden())
    // //        {
    // //            if (thePlayer.MyRed != null)
    // //            {
    // //                thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                thePlayer.MyRed = null;
    // //            }
    // //            if (ImTouched == null)
    // //            {
    // //                return;
    // //            }
    // //            if (thePlayer.PickUp)
    // //            {
    // //                thePlayer.PickUp = !thePlayer.PickUp;
    // //                thePlayer.CanPush = true;
    // //                thePlayer.MyRed = ImTouched;
    // //                thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColorPick;
    // //                thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorPickText;
    // //                theLevelManager.SkillsOn = true;
    // //                skills.ChoiceMortal = gameObject.GetComponent<Mortal>();
    // //                #region [2,2]
    // //                if (allMortal.TwoAndTwo)
    // //                {
    // //                    GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                    for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                    {
    // //                        TwoAndTwoObject[i].GetComponent<Mortal>().CanAttack = true;
    // //                        if (TwoAndTwoObject[i].GetComponent<Identity>().GetIden() != Identity.iden.Red)
    // //                        {
    // //                            //TwoAndTwoObject[i].GetComponent<Image>().color = thePlayer.PickColor;
    // //                            //TwoAndTwoObject[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.PickColor;
    // //                            TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = true;
    // //                        }
    // //                    }
    // //                }
    // //                #endregion
    // //                #region [AllMorta]
    // //                if (!allMortal.TwoAndTwo)
    // //                {
    // //                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                    for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                    {
    // //                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                    }

    // //                    for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                    {
    // //                        if (ObjectsMortal[i] == ImTouched)
    // //                        {
    // //                            ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
    // //                            ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, gameObject);
    // //                        }
    // //                    }
    // //                }
    // //                #endregion
    // //            }
    // //            else
    // //            {
    // //                thePlayer.PickUp = !thePlayer.PickUp;
    // //                thePlayer.CanPush = false;
    // //                if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    // //                {
    // //                    thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    // //                    thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    // //                }
    // //                thePlayer.MyRed = null;
    // //                #region [2,2]
    // //                if (allMortal.TwoAndTwo)
    // //                {
    // //                    GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    // //                    for (int i = 0; i < TwoAndTwoObject.Length; i++)
    // //                    {
    // //                        TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    // //                        TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    // //                    }

    // //                }
    // //                #endregion
    // //                #region [All Mortal]
    // //                if (!allMortal.TwoAndTwo)
    // //                {
    // //                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    // //                    for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                    {
    // //                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    // //                    }

    // //                    for (int i = 0; i < ObjectsMortal.Length; i++)
    // //                    {
    // //                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    // //                    }
    // //                }
    // //                #endregion
    // //            }
    // //        }


    // //    }
    // //}



    // public void CheckCanWhoAttack()
    // {
    //     if (!allMortal.TwoAndTwo)
    //     {
    //         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //         for (int i = 0; i < ObjectsMortal.Length; i++)
    //         {
    //             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //         }

    //         for (int i = 0; i < ObjectsMortal.Length; i++)
    //         {
    //             if (thePlayer.MyRed != null && ObjectsMortal[i] == thePlayer.MyRed)
    //             {
    //                 ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
    //                 ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, gameObject);
    //             }
    //         }
    //     }
    // }




    // private void Update()
    // {
    //     if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //     {
    //         MyCountNumberColor = thePlayer.RedColorText;
    //     }
    //     else if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Blue)
    //     {
    //         MyCountNumberColor = thePlayer.BlueColorText;
    //     }
    //     else if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
    //     {
    //         MyCountNumberColor = thePlayer.YellowColorText;
    //     }
    //     else if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Pink)
    //     {
    //         MyCountNumberColor = thePlayer.PinkColorText;
    //     }
    //     else if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Green)
    //     {
    //         MyCountNumberColor = thePlayer.GreenColorText;
    //     }
    //     else if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Orange)
    //     {
    //         MyCountNumberColor = thePlayer.OrangeColorText;
    //     }
    //     else if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
    //     {
    //         MyCountNumberColor = thePlayer.LastColorText;
    //     }
    //     RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
    //     RaycastHit2D hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);

    //     if (Input.GetMouseButtonUp(0) && menuSetting.GameStarted && Time.timeScale > 0)
    //     {
    //         if (hit.collider != null && !redStop && hit2.collider == null)
    //         {
    //             if (hit.collider.gameObject == gameObject && !CamMove.isCameraMoving)
    //             {

    //                 redStop = true;

    //                 #region red to red
    //                 if (thePlayer.MyRed != null)
    //                 {
    //                     if (thePlayer.MyRed != gameObject)
    //                     {
    //                         if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //                         {
    //                             AddRedToRed();
    //                             return;
    //                         }
    //                     }
    //                 }
    //                 #endregion
    //                 #region red to none
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.None)
    //                 {
    //                     RedToNone();
    //                     return;
    //                 }
    //                 #endregion
    //                 #region red to blue
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Blue)
    //                 {
    //                     RedtoBlue();
    //                     return;
    //                 }
    //                 #endregion
    //                 #region red to Yellow
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Yellow)
    //                 {
    //                     RedToYellow();
    //                     return;
    //                 }
    //                 #endregion
    //                 #region red to Pink
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Pink)
    //                 {
    //                     RedToPink();
    //                     return;
    //                 }
    //                 #endregion
    //                 #region red to Green
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Green)
    //                 {
    //                     RedToGreen();
    //                     return;
    //                 }
    //                 #endregion
    //                 #region red to Orange
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Orange)
    //                 {
    //                     RedToOrange();
    //                     return;
    //                 }
    //                 #endregion
    //                 #region red to Orange
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.LastColor)
    //                 {
    //                     RedToLastColor();
    //                     return;
    //                 }
    //                 #endregion

    //                 #region Red 
    //                 if (gameObject.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //                 {
    //                     RedCheck();
    //                     return;
    //                 }
    //                 #endregion
    //             }
    //         }
    //         else if(hit.collider == null && hit2.collider == null && !CamMove.isCameraMoving)
    //         {
    //             if (allMortal.TwoAndTwo)
    //             {
    //                 if (!thePlayer.PickUp)
    //                 {
    //                     if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //                     {
    //                         thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                         thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }
    //                 }
    //                 thePlayer.PickUp = true;
    //                 thePlayer.CanPush = false;
    //                 theLevelManager.SkillsOn = false;
    //                 skills.allAttack = false;
    //                 skills.copacity = false;
    //                 skills.turbo = false;
    //                 skills.randomChange = false;
    //                 skills.X2 = false;
    //                 skills.MaxSpace = false;
    //                 GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                 for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                 {
    //                     TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                     TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                 }
    //                 thePlayer.MyRed = null;
    //                 redStop = false;
    //                 return;
    //             }
    //             else
    //             {
    //                 if (!thePlayer.PickUp)
    //                 {
    //                     if (thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //                     {
    //                         thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                         thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }
    //                 }
    //                 thePlayer.PickUp = true;
    //                 thePlayer.CanPush = false;
    //                 theLevelManager.SkillsOn = false;
    //                 skills.allAttack = false;
    //                 skills.copacity = false;
    //                 skills.turbo = false;
    //                 skills.randomChange = false;
    //                 GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                 for (int i = 0; i < ObjectsMortal.Length; i++)
    //                 {
    //                     ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                 }

    //                 for (int i = 0; i < ObjectsMortal.Length; i++)
    //                 {
    //                     ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                 }
    //                 thePlayer.MyRed = null;
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }


    //     //Skills
    //     if (thePlayer.MyRed != null && gameObject == thePlayer.MyRed)
    //     {

    //         if (!TurboMortal)
    //         {
    //             Turbo.GetComponent<Image>().color = Color.white;
    //             star1.SetActive(false);
    //             gameObject.GetComponent<IncreaseMortal>().AmountIncrease = 1;
    //             Turbo.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
    //         }
    //         else
    //         {
    //             star1.GetComponent<Image>().color = MyCountNumberColor;
    //             star1.SetActive(true);
    //             Turbo.gameObject.GetComponent<Image>().color = WhiteLow;
    //             Turbo.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

    //         }

    //         if (!CopacityMortal)
    //         {
    //             Copacity.GetComponent<Image>().color = Color.white;
    //             star2.SetActive(false);
    //             gameObject.GetComponent<IncreaseMortal>().MaxSpace = 100;
    //             Copacity.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
    //         }
    //         else
    //         {
    //             Copacity.GetComponent<Image>().color = WhiteLow;
    //             star2.GetComponent<Image>().color = MyCountNumberColor;
    //             star2.SetActive(true);
    //             Copacity.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

    //         }

    //         if (!RandomChangeMortal)
    //         {
    //             RandomChange.GetComponent<Image>().color = Color.white;
    //             RandomChange.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
    //         }
    //         else
    //         {
    //             RandomChange.GetComponent<Image>().color = WhiteLow;
    //             RandomChange.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

    //         }




    //         if (!AllAttackMortal)
    //         {
    //             AllAttack.GetComponent<Image>().color = Color.white;
    //             star3.SetActive(false);
    //             if (StateWork)
    //             {
    //                 StateMortal sm = GetComponent<StateMortal>();
    //                 sm.MyTypeOfAttack = new GameObject[stateMortal.Length];
    //                 for (int i = 0; i < sm.MyTypeOfAttack.Length; i++)
    //                 {
    //                     sm.MyTypeOfAttack[i] = stateMortal[i];
    //                 }
    //             }
    //             AllAttack.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

    //         }
    //         else
    //         {
    //             star3.GetComponent<Image>().color = MyCountNumberColor;
    //             AllAttack.GetComponent<Image>().color = WhiteLow;
    //             star3.SetActive(true);
    //             AllAttack.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

    //         }

    //         if (!X2Mortal)
    //         {
    //             X2.GetComponent<Image>().color = Color.white;
    //             StopCounting2 = false;
    //             X2.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

    //         }
    //         else
    //         {
    //             X2.GetComponent<Image>().color = WhiteLow;
    //             X2.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

    //         }

    //         if (!MaxSpaceMortal)
    //         {
    //             MaxSpace.GetComponent<Image>().color = Color.white;
    //             MaxSpace.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
    //         }
    //         else
    //         {
    //             MaxSpace.GetComponent<Image>().color = WhiteLow;
    //             MaxSpace.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
    //         }
    //     }
    // }




    // #region RED
    // private void RedCheck()
    // {
    //     if (Identity.iden.Red == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.MyRed != null)
    //         {
    //             thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //             thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //             thePlayer.MyRed = null;
    //         }
    //         if (thePlayer.PickUp)
    //         {
    //             thePlayer.PickUp = !thePlayer.PickUp;
    //             thePlayer.CanPush = true;
    //             thePlayer.MyRed = gameObject;
    //             thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColorPick;
    //             thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorPickText;
    //             theLevelManager.SkillsOn = true;
    //             //skills.ChoiceMortal = gameObject.GetComponent<Mortal>();
    //             #region [2,2]
    //             if (allMortal.TwoAndTwo)
    //             {
    //                 GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                 for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                 {
    //                     TwoAndTwoObject[i].GetComponent<Mortal>().CanAttack = true;
    //                     if (TwoAndTwoObject[i].GetComponent<Identity>().GetIden() != Identity.iden.Red)
    //                     {
    //                         //TwoAndTwoObject[i].GetComponent<Image>().color = thePlayer.PickColor;
    //                         //TwoAndTwoObject[i].transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.PickColor;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = true;
    //                     }
    //                 }
    //             }
    //             #endregion
    //             #region [AllMorta]
    //             if (!allMortal.TwoAndTwo)
    //             {
    //                 GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                 for (int i = 0; i < ObjectsMortal.Length; i++)
    //                 {
    //                     ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                 }

    //                 for (int i = 0; i < ObjectsMortal.Length; i++)
    //                 {
    //                     if (ObjectsMortal[i] == gameObject)
    //                     {
    //                         ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
    //                         ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, gameObject);
    //                     }
    //                 }
    //             }
    //             #endregion
    //         }
    //         else
    //         {
    //             thePlayer.PickUp = !thePlayer.PickUp;
    //             thePlayer.CanPush = false;
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                 thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //             }
    //             thePlayer.MyRed = null;
    //             #region [2,2]
    //             if (allMortal.TwoAndTwo)
    //             {
    //                 GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                 for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                 {
    //                     TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                     TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                 }

    //             }
    //             #endregion
    //             #region [All Mortal]
    //             if (!allMortal.TwoAndTwo)
    //             {
    //                 GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                 for (int i = 0; i < ObjectsMortal.Length; i++)
    //                 {
    //                     ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                 }

    //                 for (int i = 0; i < ObjectsMortal.Length; i++)
    //                 {
    //                     ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                 }
    //             }
    //             #endregion
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO RED
    // private void AddRedToRed()
    // {
    //     if (Identity.iden.Red == GetComponent<Identity>().GetIden())
    //     {
    //         ClickMortalSound.Play();
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.Red == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
    //                     }
    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }

    //             }
    //             else
    //             {
    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;

    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO NONE
    // private void RedToNone()
    // {
    //     if (Identity.iden.None == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.None == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                         gameObject.GetComponent<IncreaseMortal>().ValidateMortal();
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }

    //             }
    //             else
    //             {
    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region [2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO BLUE
    // private void RedtoBlue()
    // {
    //     if (Identity.iden.Blue == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.Blue == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }
    //             }
    //             else
    //             {

    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO YELLOW
    // private void RedToYellow()
    // {
    //     if (Identity.iden.Yellow == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.Yellow == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }
    //             }
    //             else
    //             {

    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO PINK 
    // private void RedToPink()
    // {
    //     if (Identity.iden.Pink == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.Pink == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }
    //             }
    //             else
    //             {

    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO Green 
    // private void RedToGreen()
    // {
    //     if (Identity.iden.Green == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.Green == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }
    //             }
    //             else
    //             {

    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO Orange 
    // private void RedToOrange()
    // {
    //     if (Identity.iden.Orange == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.Orange == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }
    //             }
    //             else
    //             {

    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion

    // #region RED TO LASTCOLOR 
    // private void RedToLastColor()
    // {
    //     if (Identity.iden.LastColor == GetComponent<Identity>().GetIden())
    //     {
    //         if (thePlayer.CanPush && CanAttack)
    //         {
    //             if (thePlayer.MyRed != null && thePlayer.MyRed.GetComponent<Identity>().GetIden() == Identity.iden.Red)
    //             {
    //                 if (thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
    //                 {
    //                     if (Identity.iden.LastColor == GetComponent<Identity>().GetIden())
    //                     {
    //                         int AttackDamage = thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount;
    //                         thePlayer.MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
    //                         if (gameObject != null)
    //                             gameObject.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;
    //                     }
    //                     if (gameObject.GetComponent<IncreaseMortal>().CurrentCount <= 0)
    //                     {
    //                         gameObject.GetComponent<IncreaseMortal>().CurrentCount =
    //                             Mathf.Abs(gameObject.GetComponent<IncreaseMortal>().CurrentCount);
    //                         gameObject.GetComponent<Identity>().SetIden(Identity.iden.Red);
    //                         gameObject.GetComponent<Image>().color = thePlayer.RedColor;
    //                         gameObject.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     }

    //                     thePlayer.PickUp = !thePlayer.PickUp;
    //                     thePlayer.CanPush = false;
    //                     thePlayer.MyRed.GetComponent<Image>().color = thePlayer.RedColor;
    //                     thePlayer.MyRed.transform.Find("CountMortal").GetComponent<Text>().color = thePlayer.RedColorText;
    //                     theLevelManager.SkillsOn = false;
    //                     skills.allAttack = false;
    //                     skills.copacity = false;
    //                     skills.turbo = false;
    //                     skills.randomChange = false;
    //                     skills.X2 = false;
    //                     skills.MaxSpace = false;
    //                     #region[2,2]
    //                     if (allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                         for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                         {
    //                             TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                             TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                         }

    //                     }
    //                     thePlayer.MyRed = null;
    //                     #endregion
    //                     #region [All Mortal]
    //                     if (!allMortal.TwoAndTwo)
    //                     {
    //                         GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                         }

    //                         for (int i = 0; i < ObjectsMortal.Length; i++)
    //                         {
    //                             ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                         }
    //                     }
    //                     #endregion
    //                     redStop = false;
    //                     return;
    //                 }
    //             }
    //             else
    //             {

    //                 thePlayer.PickUp = !thePlayer.PickUp;
    //                 thePlayer.CanPush = false;
    //                 #region[2,2]
    //                 if (allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] TwoAndTwoObject = new GameObject[] { allMortal.Be, allMortal.Re, allMortal.De, allMortal.Ge };

    //                     for (int i = 0; i < TwoAndTwoObject.Length; i++)
    //                     {
    //                         TwoAndTwoObject[i].gameObject.GetComponent<EPOOutline.Outlinable>().enabled = false;
    //                         TwoAndTwoObject[i].gameObject.GetComponent<Mortal>().CanAttack = false;
    //                     }

    //                 }
    //                 thePlayer.MyRed = null;
    //                 #endregion
    //                 #region [All Mortal]
    //                 if (!allMortal.TwoAndTwo)
    //                 {
    //                     GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
    //                     }

    //                     for (int i = 0; i < ObjectsMortal.Length; i++)
    //                     {
    //                         ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
    //                     }
    //                 }
    //                 #endregion
    //                 redStop = false;
    //                 return;
    //             }
    //         }
    //     }
    //     redStop = false;
    // }
    // #endregion




    // public void CheckAllSkills()
    // {
    //     if (!TurboMortal)
    //     {
    //         star1.SetActive(false);
    //         gameObject.GetComponent<IncreaseMortal>().AmountIncrease = 1;
    //     }

    //     if (!CopacityMortal)
    //     {
    //         star2.SetActive(false);
    //         gameObject.GetComponent<IncreaseMortal>().MaxSpace = 100;
    //     }

    //     if (!RandomChangeMortal)
    //     {

    //     }
    //     if (!AllAttackMortal)
    //     {
    //         star3.SetActive(false);
    //         if (StateWork)
    //         {
    //             StateMortal sm = GetComponent<StateMortal>();
    //             sm.MyTypeOfAttack = new GameObject[stateMortal.Length];
    //             for (int i = 0; i < sm.MyTypeOfAttack.Length; i++)
    //             {
    //                 sm.MyTypeOfAttack[i] = stateMortal[i];
    //             }
    //         }

    //     }

    //     if (!X2Mortal)
    //     {
    //         StopCounting2 = false;
    //     }

    //     if (!MaxSpaceMortal)
    //     {
    //         StopCounting3 = false;
    //     }
    // }
}