using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    #region Properties
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
    public StateMortal MyBlue;
    public bool PickUp = true;
    private MenuSetting menuSetting;
    [HideInInspector] public CameraMovement CamMove;

    private LevelManager theLevelManager;
    private Skills skills;

    private bool BlueStop;

    private EnemySystem enemySystem;

    private StateMortal SelectedObject;
    private bool CanUseDrag;

    // Optimazie

    private RaycastHit2D hit;
    private RaycastHit2D hit2;

    private StateMortal hitObjectStats;

    private Camera camera;

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
        hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
        hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);

        if (Input.GetMouseButtonDown(0))
        {
            CamMove.isCameraMoveingWaitToClickOver = false;

            if (hit.collider != null)
            {
                GameObject draggingObj = hit.collider.gameObject;
                if (draggingObj.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Blue)
                {
                    SelectedObject = draggingObj.GetComponent<StateMortal>();
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
            if (hit.collider != null && !BlueStop && hit2.collider == null && !CamMove.isCameraMoving && !CamMove.isCameraMoveingWaitToClickOver)
            {
                hitObjectStats = hit.collider.gameObject.GetComponent<StateMortal>();

                BlueStop = true;

                // IF YOU DRAG TO ATTACK
                if (CanUseDrag && !MyBlue)
                {
                    if (SelectedObject != hitObjectStats && SelectedObject.GetIdentity() == StateMortal.iden.Blue)
                    {
                        PickUp = false;

                        MyBlue = SelectedObject;

                        MyBlue.CanPush = true;
                        MyBlue.CanAttack = true;
                        MyBlue.SetMortalColors(BlueColorPick, BlueColorPickText);

                        MyBlue.transform.DOScale(0.85f, 0.3f).SetEase(Ease.Linear).From();

                        theLevelManager.InitializeAttack(MyBlue);
                    }
                    else
                    {
                        SelectedObject = null;
                        CanUseDrag = false;
                        CamMove.isDragging = false;
                    }
                }

                // IF YOU HIT THE SQUARE THAT YOU HAVE NOT ALLOWED TO ATTACK
                if (MyBlue != null)
                {
                    if (MyBlue != hitObjectStats)
                    {
                        List<StateMortal> stateBlue = MyBlue.N_Attack_list;
                        for (int i = 0; i < stateBlue.Count; i++)
                        {
                            if (stateBlue[i].gameObject == hitObjectStats)
                            {
                                BlueStop = false;
                                return;
                            }
                        }
                    }
                }


                StateMortal.iden identity = hitObjectStats.GetIdentity();

                #region Blue to Blue
                if (MyBlue != null)
                {
                    if (MyBlue != hitObjectStats)
                    {
                        if (identity == StateMortal.iden.Blue)
                        {
                            AddBlueToBlue(hitObjectStats);
                            return;
                        }
                    }
                }
                #endregion

                if (identity == StateMortal.iden.Blue)
                {
                    BlueCheck(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.None)
                {
                    BlueToNone(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.Red)
                {
                    BlueToRed(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.Yellow)
                {
                    BlueToYellow(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.Pink)
                {
                    BlueToPink(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.Green)
                {
                    BlueToGreen(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.Orange)
                {
                    BlueToOrange(hitObjectStats);
                    return;
                }
                else if (identity == StateMortal.iden.LastColor)
                {
                    BlueToLastColor(hitObjectStats);
                    return;
                }
            }
            else if (hit.collider == null && hit2.collider == null && !CamMove.isCameraMoving && !CamMove.isCameraMoveingWaitToClickOver)
            {
                if (MyBlue != null)
                {
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
                    MyBlue.CanPush = false;
                }
                PickUp = true;
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                SelectedObject = null;
                CanUseDrag = false;
                CamMove.isDragging = false;

                theLevelManager.DeserilaizeAttack();
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
            if (Object.ReferenceEquals(MyBlue.gameObject, obj))
            {
                if (MyBlue != null)
                {
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
                    MyBlue.CanPush = false;
                }
                PickUp = true;
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.DeserilaizeAttack();
                MyBlue = null;
                BlueStop = false;
                return;
            }
        }
    }

    #region Blue
    private void BlueCheck(StateMortal obj)
    {
        if (StateMortal.iden.Blue == obj.GetIdentity())
        {
            theLevelManager.SelectMortalSound.Play();
            if (MyBlue != null)
            {
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
            }
            if (PickUp)
            {
                // Set My Blue
                PickUp = false;
                MyBlue = obj;

                MyBlue.CanPush = true;
                MyBlue.CanAttack = true;
                MyBlue.SetMortalColors(BlueColorPick, BlueColorPickText);
                theLevelManager.SkillsState = true;
                theLevelManager.UpdateSkills();
                MyBlue.UpdateSkills();

                skills.SelectedMortal = MyBlue;

                MyBlue.transform.DOScale(0.85f, 0.3f).SetEase(Ease.Linear).From();

                theLevelManager.InitializeAttack(MyBlue);

                BlueStop = false;
            }
            else
            {
                PickUp = true;

                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                if (MyBlue != null && MyBlue.GetIdentity() == StateMortal.iden.Blue)
                {
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
                }
                MyBlue = null;

                theLevelManager.DeserilaizeAttack();
            }
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO BLUE
    private void AddBlueToBlue(StateMortal obj)
    {
        if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
        {
            obj.isUnderAttack = true;
            int AttackDamage = MyBlue.CurrentCount;
            MyBlue.CurrentCount = 0;
            obj.CurrentCount += AttackDamage;
            PickUp = true;
            theLevelManager.SelectMortalSound.Play();
            MyBlue.CanPush = false;
            MyBlue.CanAttack = false;
            MyBlue.SetMortalColors(BlueColor, BlueColorText);
            theLevelManager.SkillsState = false;
            theLevelManager.UpdateSkills();


            MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

            MyBlue = null;

            theLevelManager.DeserilaizeAttack();

            BlueStop = false;
            obj.isUnderAttack = false;
            return;
        }
        else
        {
            PickUp = true;

            if (MyBlue != null)
            {
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
            }
            theLevelManager.SkillsState = false;
            theLevelManager.UpdateSkills();
            MyBlue = null;

            theLevelManager.DeserilaizeAttack();

            BlueStop = false;
            return;
        }
    }
    #endregion

    #region Blue TO NONE
    private void BlueToNone(StateMortal obj)
    {
        if (StateMortal.iden.None == obj.GetIdentity())
        {
            if (MyBlue != null && obj.gameObject != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = 0;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }
                else
                    maxDamage = AttackDamage;

                int objBurnValue = maxDamage;
                obj.CurrentCount -= AttackDamage;


                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();
                    obj.CurrentCount =
                        Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.AttackNoneColorSound.Play();
                CamMove.Shake(0.07f, 0.13f, 0.03f, 0.2f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, NoneColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                MyBlue = null;

                theLevelManager.DeserilaizeAttack();

                BlueStop = false;
                obj.isUnderAttack = false;
                return;
            }
            else
            {
                PickUp = true;

                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                MyBlue = null;

                theLevelManager.DeserilaizeAttack();

                BlueStop = false;
                return;
            }
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO RED
    private void BlueToRed(StateMortal obj)
    {
        if (StateMortal.iden.Red == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = AttackDamage;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }

                int objBurnValue = maxDamage;
                obj.CurrentCount -= AttackDamage;

                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();

                    obj.CurrentCount =
                        Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    enemySystem.LookAtList(obj, StateMortal.iden.Red);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                    obj.ResetEnemyTurbo();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.AttackRedColorSound.Play();
                CamMove.Shake(0.086f, 0.15f, 0.05f, 0.1f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, RedColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                obj.isUnderAttack = false;
                return;

            }
            else
            {
                PickUp = true;

                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                return;
            }
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Yellow
    private void BlueToYellow(StateMortal obj)
    {
        if (StateMortal.iden.Yellow == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = 0;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }
                else
                    maxDamage = AttackDamage;

                int objBurnValue = maxDamage;
                obj.CurrentCount -= AttackDamage;

                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();
                    obj.CurrentCount =
                        Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    enemySystem.LookAtList(obj, StateMortal.iden.Yellow);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                    obj.ResetEnemyTurbo();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.AttackYellowColorSound.Play();
                CamMove.Shake(0.09f, 0.14f, 0.03f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, YellowColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                obj.isUnderAttack = false;
                return;
            }
            else
            {
                PickUp = true;

                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                return;
            }
            BlueStop = false;
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Pink
    private void BlueToPink(StateMortal obj)
    {
        if (StateMortal.iden.Pink == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = AttackDamage;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }

                int objBurnValue = maxDamage;
                obj.CurrentCount -= AttackDamage;

                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();
                    obj.CurrentCount =
                            Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    enemySystem.LookAtList(obj, StateMortal.iden.Pink);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                    obj.ResetEnemyTurbo();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();


                theLevelManager.AttackSound.Play();
                CamMove.Shake(0.08f, 0.13f, 0.03f, 0.2f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, PinkColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);


                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                obj.isUnderAttack = false;
                return;

            }
            else
            {
                PickUp = true;
                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                return;
            }
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Green
    private void BlueToGreen(StateMortal obj)
    {
        if (StateMortal.iden.Green == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = AttackDamage;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }

                int objBurnValue = maxDamage;

                obj.CurrentCount -= AttackDamage;

                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();
                    obj.CurrentCount =
                            Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    enemySystem.LookAtList(obj, StateMortal.iden.Green);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                    obj.ResetEnemyTurbo();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.AttackSound.Play();
                CamMove.Shake(0.085f, 0.125f, 0.03f, 0.2f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, GreenColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                obj.isUnderAttack = false;
                return;
            }
            else
            {
                PickUp = true;
                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                return;
            }
        }
        BlueStop = false;
    }
    #endregion

    #region BLUE TO Orange
    private void BlueToOrange(StateMortal obj)
    {
        if (StateMortal.iden.Orange == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = AttackDamage;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }

                int objBurnValue = maxDamage;
                obj.CurrentCount -= AttackDamage;

                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();
                    obj.CurrentCount =
                            Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    enemySystem.LookAtList(obj, StateMortal.iden.Orange);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                    obj.ResetEnemyTurbo();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.AttackYellowColorSound.Play();
                CamMove.Shake(0.09f, 0.15f, 0.03f, 0.2f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, OrangeColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                obj.isUnderAttack = false;
                return;
            }
            else
            {
                PickUp = true;
                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                return;
            }
        }
        BlueStop = false;
    }
    #endregion

    #region Blue TO LastColor
    private void BlueToLastColor(StateMortal obj)
    {
        if (StateMortal.iden.LastColor == obj.GetIdentity())
        {
            if (MyBlue != null && obj != null && MyBlue.CanPush && obj.CanAttack && !obj.isUnderAttack)
            {
                obj.isUnderAttack = true;
                int AttackDamage = MyBlue.CurrentCount;
                MyBlue.CurrentCount = 0;

                int maxDamage = AttackDamage;
                if (AttackDamage > obj.CurrentCount)
                {
                    maxDamage = obj.CurrentCount;
                }

                int objBurnValue = maxDamage;

                obj.CurrentCount -= AttackDamage;

                if (obj.CurrentCount <= 0)
                {
                    if (obj.AllAttackMortal)
                        obj.ResetTypeOfAttackData();

                    obj.CurrentCount =
                        Mathf.Abs(obj.CurrentCount);
                    obj.SetIdentity(StateMortal.iden.Blue);
                    enemySystem.LookAtList(obj, StateMortal.iden.LastColor);
                    obj.SetMortalColors(BlueColor, BlueColorText);
                    obj.ValidateMortal();
                    obj.ResetEnemyTurbo();
                }

                PickUp = true;
                MyBlue.CanPush = false;
                MyBlue.CanAttack = false;
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();

                theLevelManager.AttackYellowColorSound.Play();
                CamMove.Shake(0.1f, 0.12f, 0.03f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, LastColor, objBurnValue / 2);

                MyBlue.LineConnections(obj.gameObject, MyBlue.transform.position, BlueColor);

                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                return;
            }
            else
            {
                PickUp = true;
                if (MyBlue != null)
                {
                    MyBlue.CanPush = false;
                    MyBlue.CanAttack = false;
                }
                theLevelManager.SkillsState = false;
                theLevelManager.UpdateSkills();
                MyBlue = null;
                theLevelManager.DeserilaizeAttack();
                BlueStop = false;
                obj.isUnderAttack = false;
                return;
            }
        }
        BlueStop = false;
    }
    #endregion
}