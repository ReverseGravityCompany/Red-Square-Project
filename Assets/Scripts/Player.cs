using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    #region Properties
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
    private MenuSetting menuSetting;
    [HideInInspector] public CameraMovement CamMove;

    private LevelManager theLevelManager;
    private Skills skills;

    private bool BlueStop;

    private EnemySystem enemySystem;

    private GameObject SelectedObject;
    private bool CanUseDrag;

    // Optimazie

    private RaycastHit2D hit;
    private RaycastHit2D hit2;

    private Camera camera;


    private Identity objIdentity;
    [HideInInspector] public SquareClass MyBlueSquareClass;
    private StateMortal MyBlueStateMortal;

    #endregion

    private void Start()
    {
        menuSetting = FindObjectOfType<MenuSetting>();
        CamMove = CameraMovement._Instance;
        theLevelManager = FindObjectOfType<LevelManager>();
        skills = FindObjectOfType<Skills>();
        enemySystem = FindObjectOfType<EnemySystem>();
        PickUp = true;

        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
            hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);

            CamMove.isCameraMoveingWaitToClickOver = false;

            if (hit.collider != null)
            {
                CamMove.Focusing = false;
                CamMove.Target = Vector2.zero;

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
        if ((Input.GetMouseButtonUp(0) && theLevelManager.LearningLevels) || Input.GetMouseButtonUp(0) && menuSetting.GameStarted && Time.timeScale > 0)
        {
            hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
            hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);
            if (hit.collider != null && !BlueStop && hit2.collider == null && !CamMove.isCameraMoving && !CamMove.isCameraMoveingWaitToClickOver)
            {
                GameObject obj = hit.collider.gameObject;
                objIdentity = obj.GetComponent<Identity>();

                BlueStop = true;

                // IF YOU DRAG TO ATTACK
                if (CanUseDrag && !MyBlue)
                {
                    if (SelectedObject != obj)
                    {
                        PickUp = !PickUp;

                        MyBlue = SelectedObject;
                        MyBlueSquareClass = MyBlue.GetComponent<SquareClass>();
                        MyBlueStateMortal = MyBlue.GetComponent<StateMortal>();

                        MyBlueSquareClass.CanPush = true;
                        MyBlueSquareClass.CanAttack = true;
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
                                ObjectsMortal[i].GetComponent<StateMortal>().Attack_N(ObjectsMortal, MyBlue);
                            }
                        }
                        #endregion

                    }
                }

                // IF YOU HIT THE SQUARE THAT YOU HAVE NOT ALLOWED TO ATTACK
                if (MyBlue != null)
                {
                    if (MyBlue != obj)
                    {
                        List<GameObject> stateBlue = MyBlue.GetComponent<StateMortal>().N_Attack_list;
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
                        if (objIdentity.GetIdentity() == Identity.iden.Blue)
                        {
                            AddBlueToBlue(objIdentity);
                            return;
                        }
                    }
                }
                #endregion


                if (objIdentity.GetIdentity() == Identity.iden.Blue)
                {
                    BlueCheck(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.None)
                {
                    BlueToNone(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.Red)
                {
                    BlueToRed(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.Yellow)
                {
                    BlueToYellow(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.Pink)
                {
                    BlueToPink(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.Green)
                {
                    BlueToGreen(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.Orange)
                {
                    BlueToOrange(objIdentity);
                    return;
                }
                else if (objIdentity.GetIdentity() == Identity.iden.LastColor)
                {
                    BlueToLastColor(objIdentity);
                    return;
                }

            }
            else if (hit.collider == null && hit2.collider == null && !CamMove.isCameraMoving && !CamMove.isCameraMoveingWaitToClickOver)
            {
                if (MyBlue != null)
                {
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    MyBlueSquareClass.CanPush = false;
                }
                PickUp = true;
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                //skills.allAttack = false;
                //skills.copacity = false;
                //skills.turbo = false;
                //skills.randomChange = false;
                //skills.X2 = false;
                //skills.MaxSpace = false;

                SelectedObject = null;
                CanUseDrag = false;
                CamMove.isDragging = false;

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
                    MyBlueSquareClass.CanPush = false;
                }
                PickUp = true;
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                //skills.allAttack = false;
                //skills.copacity = false;
                //skills.turbo = false;
                //skills.randomChange = false;
                //skills.X2 = false;
                //skills.MaxSpace = false;
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
    private void BlueCheck(Identity obj)
    {
        if (Identity.iden.Blue == obj.GetIdentity())
        {
            theLevelManager.SelectMortalSound.Play();
            if (MyBlue != null)
            {
                MyBlue.GetComponent<Image>().color = BlueColor;
                MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
            }
            if (PickUp)
            {
                // Set My Blue
                PickUp = !PickUp;
                MyBlue = obj.gameObject;
                MyBlueSquareClass = MyBlue.GetComponent<SquareClass>();
                MyBlueStateMortal = MyBlue.GetComponent<StateMortal>();

                MyBlueSquareClass.CanPush = true;
                MyBlueSquareClass.CanAttack = true;
                MyBlue.GetComponent<Image>().color = BlueColorPick;
                MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorPickText;
                theLevelManager.SkillsState = true;
                theLevelManager.UpdateSkills();
                MyBlueSquareClass.UpdateSkills();

                skills.SelectedMortal = MyBlueSquareClass;

                MyBlue.transform.DOScale(0.85f, 0.3f).SetEase(Ease.Linear).From();

                if (Vector2.Distance(CamMove.transform.position, MyBlue.transform.position) > CamMove.TargetOffset)
                {
                    CamMove.Focusing = true;
                    CamMove.Target = MyBlue.transform.position;
                }

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
                        ObjectsMortal[i].GetComponent<StateMortal>().Attack_N(ObjectsMortal, MyBlue);
                    }
                }
                #endregion
                BlueStop = false;
            }
            else
            {
                PickUp = !PickUp;
                MyBlueSquareClass.CanPush = false;
                MyBlueSquareClass.CanAttack = false;
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
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
    private void AddBlueToBlue(Identity obj)
    {
        if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
        {
            obj.GetComponent<SquareClass>().isUnderAttack = true;

            int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
            MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;
            obj.GetComponent<IncreaseMortal>().CurrentCount += AttackDamage;
            PickUp = !PickUp;
            theLevelManager.SelectMortalSound.Play();
            MyBlueSquareClass.CanPush = false;
            MyBlue.GetComponent<Image>().color = BlueColor;
            MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
            theLevelManager.SkillsState = false;
            theLevelManager.UpdateSkills();


            MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

            obj.GetComponent<SquareClass>().isUnderAttack = false;
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
            MyBlueSquareClass.CanPush = false;
            theLevelManager.SkillsState = false;
            theLevelManager.UpdateSkills();
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
    private void BlueToNone(Identity obj)
    {
        if (Identity.iden.None == obj.GetIdentity())
        {
            if (MyBlue != null && obj.gameObject != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

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

                    int objBurnValue = maxDamage;
                    objData.CurrentCount -= AttackDamage;


                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();

                    theLevelManager.AttackNoneColorSound.Play();
                    CamMove.Shake(0.07f, 0.13f, 0.03f, 0.2f);
                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, NoneColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO RED
    private void BlueToRed(Identity obj)
    {
        if (Identity.iden.Red == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj.gameObject, Identity.iden.Red);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                        obj.GetComponent<SquareClass>().ResetEnemyTurbo();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();

                    theLevelManager.AttackRedColorSound.Play();
                    CamMove.Shake(0.085f, 0.14f, 0.05f, 0.22f);

                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, RedColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Yellow
    private void BlueToYellow(Identity obj)
    {
        if (Identity.iden.Yellow == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

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

                    int objBurnValue = maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj.gameObject, Identity.iden.Yellow);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                        obj.GetComponent<SquareClass>().ResetEnemyTurbo();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();

                    theLevelManager.AttackYellowColorSound.Play();
                    CamMove.Shake(0.1f, 0.15f, 0.03f, 0.25f);

                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, YellowColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Pink
    private void BlueToPink(Identity obj)
    {
        if (Identity.iden.Pink == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj.gameObject, Identity.iden.Pink);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                        obj.GetComponent<SquareClass>().ResetEnemyTurbo();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();


                    theLevelManager.AttackSound.Play();
                    CamMove.Shake(0.1f, 0.13f, 0.03f, 0.2f);

                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, PinkColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);


                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Green
    private void BlueToGreen(Identity obj)
    {
        if (Identity.iden.Green == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = maxDamage;

                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj.gameObject, Identity.iden.Green);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                        obj.GetComponent<SquareClass>().ResetEnemyTurbo();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();

                    theLevelManager.AttackSound.Play();
                    CamMove.Shake(0.1f, 0.15f, 0.03f, 0.2f);

                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, GreenColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Orange
    private void BlueToOrange(Identity obj)
    {
        if (Identity.iden.Orange == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = maxDamage;
                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj.gameObject, Identity.iden.Orange);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                        obj.GetComponent<SquareClass>().ResetEnemyTurbo();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();

                    theLevelManager.AttackYellowColorSound.Play();
                    CamMove.Shake(0.11f, 0.15f, 0.03f, 0.2f);

                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, OrangeColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);


                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region Blue TO LastColor
    private void BlueToLastColor(Identity obj)
    {
        if (Identity.iden.LastColor == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlueSquareClass.CanPush && obj.GetComponent<SquareClass>().CanAttack && !obj.GetComponent<SquareClass>().isUnderAttack)
            {
                if (MyBlue.GetComponent<IncreaseMortal>().CurrentCount > 0)
                {
                    obj.GetComponent<SquareClass>().isUnderAttack = true;

                    int AttackDamage = MyBlue.GetComponent<IncreaseMortal>().CurrentCount;
                    MyBlue.GetComponent<IncreaseMortal>().CurrentCount = 0;

                    IncreaseMortal objData = obj.GetComponent<IncreaseMortal>();

                    int maxDamage = AttackDamage;
                    if (AttackDamage > objData.CurrentCount)
                    {
                        maxDamage = objData.CurrentCount;
                    }

                    int objBurnValue = maxDamage;

                    objData.CurrentCount -= AttackDamage;

                    if (objData.CurrentCount <= 0)
                    {
                        obj.GetComponent<StateMortal>().ResetTypeOfAttackData();
                        objData.CurrentCount =
                            Mathf.Abs(objData.CurrentCount);
                        obj.SetIdentity(Identity.iden.Blue);
                        enemySystem.LookAtList(obj.gameObject, Identity.iden.LastColor);
                        obj.GetComponent<Image>().color = BlueColor;
                        obj.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                        objData.ValidateMortal();
                        obj.GetComponent<SquareClass>().ResetEnemyTurbo();
                    }

                    PickUp = !PickUp;
                    MyBlueSquareClass.CanPush = false;
                    MyBlue.GetComponent<Image>().color = BlueColor;
                    MyBlue.transform.Find("CountMortal").GetComponent<Text>().color = BlueColorText;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();

                    theLevelManager.AttackSound.Play();
                    CamMove.Shake(0.11f, 0.13f, 0.03f, 0.25f);

                    if (objBurnValue > 12)
                        MyBlueStateMortal.ArmyBurning(obj.gameObject, BlueColor, LastColor, objBurnValue / 4);

                    MyBlueStateMortal.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);


                    obj.GetComponent<SquareClass>().isUnderAttack = false;
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
                    MyBlueSquareClass.CanPush = false;
                    theLevelManager.SkillsState = false;
                    theLevelManager.UpdateSkills();
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
            theLevelManager.SelectMortalSound.Play();
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion
}
