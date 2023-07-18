using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInLearnScene : MonoBehaviour
{
    public Color PickColor, PickColorText;
    public Color NoneColor, NoneColorText;
    public Color BlueColor, BlueColorText;
    public Color RedColor, RedColorText;
    public Color RedColorPick, RedColorPickText;
    public Color YellowColor, YellowColorText;
    public Color PinkColor, PinkColorText;
    public Color GreenColor, GreenColorText;
    public Color OrangeColor, OrangeColorText;
    public Color LastColor, LastColorText;
    public bool CanPush = false;
    public GameObject MyRed;
    public bool PickUp = true;
    [HideInInspector]
    public AllMortal allMortal;
    public GameObject[] AllSkilles;
    private MenuSetting menuSetting;
    public CameraMovement CamMove;
    public AudioSource ClickMortalSound;
    private LevelManager theLevelManager;
    private Skills skills;

    private bool redStop;

    private EnemySystem enemySystem;

    private void Start()
    {
        allMortal = FindObjectOfType<AllMortal>();
        CamMove = CameraMovement._Instance;
        theLevelManager = FindObjectOfType<LevelManager>();
        skills = FindObjectOfType<Skills>();
        enemySystem = FindObjectOfType<EnemySystem>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
        RaycastHit2D hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);


        if (Input.GetMouseButtonUp(0) && Time.timeScale > 0)
        {
            //print(hit.collider);
            if (hit.collider != null && !redStop && hit2.collider == null && !CamMove.isCameraMoving)
            {
                GameObject obj = hit.collider.gameObject;
                redStop = true;
                ClickMortalSound.Play();

                if (MyRed != null)
                {
                    if (MyRed != obj)
                    {
                        List<GameObject> stateRed = MyRed.GetComponent<StateMortal>().CantAttack;
                        for(int i = 0 ; i < stateRed.Count;i++){
                            if(stateRed[i] == obj){
                                redStop = false;
                                return;
                            }
                        }
                    }
                }

                #region red to red
                if (MyRed != null)
                {
                    if (MyRed != obj)
                    {
                        if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
                        {
                            AddRedToRed(obj);
                            return;
                        }
                    }
                }
                #endregion
                #region red to none
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.None)
                {
                    RedToNone(obj);
                    return;
                }
                #endregion
                #region red to blue
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
                {
                    RedToBlue(obj);
                    return;
                }
                #endregion
                #region red to Yellow
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
                {
                    RedToYellow(obj);
                    return;
                }
                #endregion
                #region red to Pink
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
                {
                    RedToPink(obj);
                    return;
                }
                #endregion
                #region red to Green
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
                {
                    RedToGreen(obj);
                    return;
                }
                #endregion
                #region red to Orange
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
                {
                    RedToOrange(obj);
                    return;
                }
                #endregion
                #region red to LastColor
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
                {
                    RedToLastColor(obj);
                    return;
                }
                #endregion

                #region Red 
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
                {
                    RedCheck(obj);
                    return;
                }
                #endregion
            }
            else if (hit.collider == null && hit2.collider == null && !CamMove.isCameraMoving)
            {
                if (MyRed != null)
                {
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                }
                PickUp = true;
                GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                }

                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                }
                MyRed = null;
                redStop = false;
                return;
            }
        }else if(Time.timeScale > 0) {
            
        }
    }


    public void RenederAllAgain(GameObject obj)
    {
        if(!Object.ReferenceEquals(MyRed,null))
        {
            if (Object.ReferenceEquals(MyRed, obj))
            {
                if (MyRed != null)
                {
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                }
                PickUp = true;
                theLevelManager.SkillsState = false;
                skills.allAttack = false;
                skills.copacity = false;
                skills.turbo = false;
                skills.randomChange = false;
                skills.X2 = false;
                skills.MaxSpace = false;
                GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                }

                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                }
                MyRed = null;
                redStop = false;
                return;
            }
        }
    }




    #region RED
    private void RedCheck(GameObject obj)
    {
        if (Identity.iden.Red == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null)
            {
                MyRed.GetComponent<Image>().color = RedColor;
                MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
            }
            if (PickUp)
            {
                PickUp = !PickUp;
                MyRed = obj;
                MyRed.GetComponent<SquareClass>().CanPush = true;
                MyRed.GetComponent<SquareClass>().CanAttack = true;
                MyRed.GetComponent<Image>().color = RedColorPick;
                MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorPickText;
                #region [AllMorta]
                GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                }

                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    if (ObjectsMortal[i] == MyRed)
                    {
                        ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
                        ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, MyRed);
                    }
                }
                #endregion
                redStop = false;
            }
            else
            {
                PickUp = !PickUp;
                MyRed.GetComponent<SquareClass>().CanPush = false;
                MyRed.GetComponent<SquareClass>().CanAttack = false;
                if (MyRed != null && MyRed.GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
                {
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                }
                MyRed = null;
                #region [All Mortal]
                GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                }

                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                }
                #endregion
            }
        }
        redStop = false;
    }
    #endregion

    #region RED TO RED
    private void AddRedToRed(GameObject obj)
    {
        
        if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
        {
            int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
            MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
            obj.GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
            PickUp = !PickUp;
            MyRed.GetComponent<SquareClass>().CanPush = false;
            MyRed.GetComponent<Image>().color = RedColor;
            MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
            MyRed = null;
            #region [All Mortal]
            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
            for (int i = 0; i < ObjectsMortal.Length; i++)
            {
                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
            }

            for (int i = 0; i < ObjectsMortal.Length; i++)
            {
                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
            }
            #endregion
            redStop = false;
            return;
        }
        else
        {
            PickUp = !PickUp;
            MyRed.GetComponent<SquareClass>().CanPush = false;
            theLevelManager.SkillsState = false;
            MyRed = null;
            #region [All Mortal]
            GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
            for (int i = 0; i < ObjectsMortal.Length; i++)
            {
                ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
            }

            for (int i = 0; i < ObjectsMortal.Length; i++)
            {
                ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
            }
            #endregion
            redStop = false;
            return;
        }
    }
    #endregion

    #region RED TO NONE
    private void RedToNone(GameObject obj)
    {
        if (Identity.iden.None == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion

    #region RED TO Blue
    private void RedToBlue(GameObject obj)
    {
        if (Identity.iden.Blue == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        //enemySystem.LookAtList(obj,Identity.iden.Blue);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion

    #region RED TO Yellow
    private void RedToYellow(GameObject obj)
    {
        if (Identity.iden.Yellow == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        enemySystem.LookAtList(obj,Identity.iden.Yellow);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion

    #region RED TO Pink
    private void RedToPink(GameObject obj)
    {
        if (Identity.iden.Pink == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        enemySystem.LookAtList(obj,Identity.iden.Pink);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion

    #region RED TO Green
    private void RedToGreen(GameObject obj)
    {
        if (Identity.iden.Green == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        enemySystem.LookAtList(obj,Identity.iden.Green);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion

    #region RED TO Orange
    private void RedToOrange(GameObject obj)
    {
        if (Identity.iden.Orange == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        enemySystem.LookAtList(obj,Identity.iden.Orange);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion

      #region RED TO LastColor
    private void RedToLastColor(GameObject obj)
    {
        if (Identity.iden.LastColor == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyRed != null && obj != null && MyRed.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyRed.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyRed.GetComponent<IncreaseMortal>().CurrentCount;
                    MyRed.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    obj.GetComponent<IncreaseMortal>().CurrentCount -= AttackDamage;

                    if (obj.GetComponent<IncreaseMortal>().CurrentCount <= 0)
                    {
                        obj.GetComponent<IncreaseMortal>().CurrentCount =
                            Mathf.Abs(obj.GetComponent<IncreaseMortal>().CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Red);
                        enemySystem.LookAtList(obj,Identity.iden.LastColor);
                        obj.GetComponent<Image>().color = RedColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                        obj.GetComponent<IncreaseMortal>().ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    MyRed.GetComponent<Image>().color = RedColor;
                    MyRed.transform.Find("CountMortal").GetComponent<Text>().color = RedColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyRed.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyRed = null;
                    #region [All Mortal]
                    GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                    }

                    for (int i = 0; i < ObjectsMortal.Length; i++)
                    {
                        ObjectsMortal[i].gameObject.GetComponent<StateMortal>().HideTypeOfAttack();
                    }
                    #endregion
                    redStop = false;
                    return;
                }
            }
            redStop = false;
        }
        redStop = false;
    }
    #endregion
}
