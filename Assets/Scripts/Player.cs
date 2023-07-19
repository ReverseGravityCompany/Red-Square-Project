using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Color PickColor, PickColorText;
    public Color NoneColor, NoneColorText;
    public Color BlueColor, BlueColorText;
    public Color BlueColorPick, BlueColorPickText;
    public Color RedColor, RedColorText;
    public Color YellowColor, YellowColorText;
    public Color PinkColor, PinkColorText;
    public Color GreenColor, GreenColorText;
    public Color OrangeColor, OrangeColorText;
    public Color LastColor, LastColorText;


    public bool CanPush = false;
    public GameObject MyBlue;
    public bool PickUp = true;
    [HideInInspector]
    public AllMortal allMortal;
    //public GameObject[] AllSkilles;
    private MenuSetting menuSetting;
    [HideInInspector] public CameraMovement CamMove;
    public AudioSource SelectMortalSound,AttackNoneColorSound, AttackRedColorSound;
    private LevelManager theLevelManager;
    private Skills skills;

    private bool BlueStop;

    private EnemySystem enemySystem;

    private GameObject SelectedObject;
    private bool CanUseDrag;

    private void Start()
    {
        allMortal = FindObjectOfType<AllMortal>();
        menuSetting = FindObjectOfType<MenuSetting>();
        CamMove = CameraMovement._Instance;
        theLevelManager = FindObjectOfType<LevelManager>();
        skills = FindObjectOfType<Skills>();
        enemySystem = FindObjectOfType<EnemySystem>();
        PickUp = true;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
        RaycastHit2D hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);

        if (Input.GetMouseButtonDown(0))
        {
            CamMove.isCameraMoveingWaitToClickOver = false;

            if(hit.collider != null)
            {
                GameObject draggingObj = hit.collider.gameObject;
                if (draggingObj.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
                {
                    SelectedObject = draggingObj;
                    CanUseDrag = true;
                    CamMove.isDragging = true;
                }
                else
                {
                    SelectedObject = null;
                    CanUseDrag = false;
                    CamMove.isDragging = false;
                }
            }
            else
            {
                SelectedObject = null;
                CanUseDrag = false;
                CamMove.isDragging = false;
            }
        }


        if (Input.GetMouseButtonUp(0) && menuSetting.GameStarted && Time.timeScale > 0)
        {
            //print(hit.collider);
            if (hit.collider != null && !BlueStop && hit2.collider == null && !CamMove.isCameraMoving && !CamMove.isCameraMoveingWaitToClickOver)
            {
                
                GameObject obj = hit.collider.gameObject;

                BlueStop = true;

                if (CanUseDrag && !MyBlue)
                {
                    if (SelectedObject != obj)
                    {
                        MyBlue = SelectedObject;
                        PickUp = !PickUp;
                        MyBlue.GetComponent<SquareClass>().CanPush = true;
                        MyBlue.GetComponent<SquareClass>().CanAttack = true;
                        MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorPickText;
                        MyBlue.transform.DOScale(0.85f, 0.3f).SetEase(Ease.Linear).From();

                        #region [AllMorta]
                        GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                        for (int i = 0; i < ObjectsMortal.Length; i++)
                        {
                            ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                        }

                        for (int i = 0; i < ObjectsMortal.Length; i++)
                        {
                            if (ObjectsMortal[i] == MyBlue)
                            {
                                ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
                                ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, MyBlue);
                            }
                        }
                        #endregion

                    }
                }

                if (MyBlue != null)
                {
                    if (MyBlue != obj)
                    {
                        List<GameObject> stateBlue = MyBlue.GetComponent<StateMortal>().CantAttack;
                        for (int i = 0; i < stateBlue.Count; i++)
                        {
                            if (stateBlue[i] == obj)
                            {
                                BlueStop = false;
                                return;
                            }
                        }
                    }
                }

                

                #region Blue to Blue
                if (MyBlue != null)
                {
                    if (MyBlue != obj)
                    {
                        if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
                        {
                            AddBlueToBlue(obj);
                            return;
                        }
                    }
                }
                #endregion
                #region Blue to none
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.None)
                {
                    BlueToNone(obj);
                    return;
                }
                #endregion
                #region Blue to red
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Red)
                {
                    BlueToRed(obj);
                    return;
                }
                #endregion
                #region Blue to Yellow
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Yellow)
                {
                    BlueToYellow(obj);
                    return;
                }
                #endregion
                #region Blue to Pink
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Pink)
                {
                    BlueToPink(obj);
                    return;
                }
                #endregion
                #region Blue to Green
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Green)
                {
                    BlueToGreen(obj);
                    return;
                }
                #endregion
                #region Blue to Orange
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Orange)
                {
                    BlueToOrange(obj);
                    return;
                }
                #endregion
                #region Blue to LastColor
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.LastColor)
                {
                    BlueToLastColor(obj);
                    return;
                }
                #endregion

                #region Blue 
                if (obj.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
                {
                    BlueCheck(obj);
                    return;
                }
                #endregion
            }
            else if (hit.collider == null && hit2.collider == null && !CamMove.isCameraMoving && !CamMove.isCameraMoveingWaitToClickOver)
            {
                if (MyBlue != null)
                {
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
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
                MyBlue = null;
                BlueStop = false;
                return;
            }
        }
    }

    public void RenederAllAgain(GameObject obj)
    {
        if (!Object.ReferenceEquals(MyBlue, null))
        {
            if (Object.ReferenceEquals(MyBlue, obj))
            {
                if (MyBlue != null)
                {
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
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
                MyBlue = null;
                BlueStop = false;
                return;
            }
        }
    }


    #region Blue
    private void BlueCheck(GameObject obj)
    {
        if (Identity.iden.Blue == obj.GetComponent<Identity>().GetIdentity())
        {
            SelectMortalSound.Play();
            if (MyBlue != null)
            {
                MyBlue.GetComponent<Image>().color = BlueColor;
                MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
            }
            if (PickUp)
            {
                PickUp = !PickUp;
                MyBlue = obj;
                MyBlue.GetComponent<SquareClass>().CanPush = true;
                MyBlue.GetComponent<SquareClass>().CanAttack = true;
                MyBlue.GetComponent<Image>().color = BlueColorPick;
                MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorPickText;
                theLevelManager.SkillsState = true;
                skills.SelectedMortal = MyBlue.GetComponent<SquareClass>();

                MyBlue.transform.DOScale(0.85f, 0.3f).SetEase(Ease.Linear).From();

                #region [AllMorta]
                GameObject[] ObjectsMortal = new GameObject[FindObjectsOfType<Identity>().Length];
                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    ObjectsMortal[i] = FindObjectsOfType<Identity>()[i].gameObject;
                }

                for (int i = 0; i < ObjectsMortal.Length; i++)
                {
                    if (ObjectsMortal[i] == MyBlue)
                    {
                        ObjectsMortal[i].GetComponent<StateMortal>().ShowTypeOfAttack();
                        ObjectsMortal[i].GetComponent<StateMortal>().WhoCantAttack(ObjectsMortal, MyBlue);
                    }
                }
                #endregion
                BlueStop = false;
            }
            else
            {
                PickUp = !PickUp;
                MyBlue.GetComponent<SquareClass>().CanPush = false;
                MyBlue.GetComponent<SquareClass>().CanAttack = false;
                theLevelManager.SkillsState = false;
                if (MyBlue != null && MyBlue.GetComponent<Identity>().GetIdentity() == Identity.iden.Blue)
                {
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                }
                MyBlue = null;
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
        BlueStop = false;
    }
    #endregion

    #region BLUE TO BLUE
    private void AddBlueToBlue(GameObject obj)
    {       
        if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
        {
            int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
            MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;
            obj.GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
            PickUp = !PickUp;
            SelectMortalSound.Play();
            MyBlue.GetComponent<SquareClass>().CanPush = false;
            MyBlue.GetComponent<Image>().color = BlueColor;
            MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
            theLevelManager.SkillsState = false;
            skills.allAttack = false;
            skills.copacity = false;
            skills.turbo = false;
            skills.randomChange = false;
            skills.X2 = false;
            skills.MaxSpace = false;


            CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);
            MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

            MyBlue = null;
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
            BlueStop = false;
            return;
        }
        else
        {
            PickUp = !PickUp;
            MyBlue.GetComponent<SquareClass>().CanPush = false;
            theLevelManager.SkillsState = false;
            MyBlue = null;
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
            BlueStop = false;
            return;
        }
    }
    #endregion

    #region Blue TO NONE
    private void BlueToNone(GameObject obj)
    {
        if (Identity.iden.None == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && MyBlue.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;
                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = 0;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }
                    else
                        maxDamage = AttackDamage;

                    int objBurnValue = objData.CurrentCount + maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, NoneColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);


                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;

                    AttackNoneColorSound.Play();
                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO RED
    private void BlueToRed(GameObject obj)
    {
        if (Identity.iden.Red == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && MyBlue.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = objData.CurrentCount + maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj, Identity.iden.Red);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;

                    AttackRedColorSound.Play();
                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, RedColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Yellow
    private void BlueToYellow(GameObject obj)
    {
        if (Identity.iden.Yellow == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = 0;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }
                    else
                        maxDamage = AttackDamage;

                    int objBurnValue = objData.CurrentCount + maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj, Identity.iden.Yellow);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;

                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, YellowColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Pink
    private void BlueToPink(GameObject obj)
    {
        if (Identity.iden.Pink == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = objData.CurrentCount + maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj, Identity.iden.Pink);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;


                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, PinkColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Green
    private void BlueToGreen(GameObject obj)
    {
        if (Identity.iden.Green == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = objData.CurrentCount + maxDamage;

                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj, Identity.iden.Green);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;

                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, GreenColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Orange
    private void BlueToOrange(GameObject obj)
    {
        if (Identity.iden.Orange == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = objData.CurrentCount + maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj, Identity.iden.Orange);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;

                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, OrangeColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region Blue TO LastColor
    private void BlueToLastColor(GameObject obj)
    {
        if (Identity.iden.LastColor == obj.GetComponent<Identity>().GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.GetComponent<SquareClass>().CanPush && obj.GetComponent<SquareClass>().CanAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = objData.CurrentCount + maxDamage;

                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.GetComponent<Identity>().SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj, Identity.iden.LastColor);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    skills.allAttack = false;
                    skills.copacity = false;
                    skills.turbo = false;
                    skills.randomChange = false;
                    skills.X2 = false;
                    skills.MaxSpace = false;


                    CameraMovement._Instance.Shake(0.15f, 0.25f, 0.08f, 0.25f);

                    MyBlue.GetComponent<StateMortal>().ArmyBurning(obj, BlueColor, LastColor, objBurnValue / 2);
                    MyBlue.GetComponent<StateMortal>().LineConnections(obj, MyBlue.transform.position, BlueColor);

                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
                else
                {
                    PickUp = !PickUp;
                    MyBlue.GetComponent<SquareClass>().CanPush = false;
                    theLevelManager.SkillsState = false;
                    MyBlue = null;
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
                    BlueStop = false;
                    return;
                }
            }
            SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion
}
