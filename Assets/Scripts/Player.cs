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

    private LineRenderer lineRenderer;
    private bool LineEffectVisibiltiy;

    private StateMortal temporaryObject;
    private bool temprorayBool;

    private Tween blueTween;
    private Tween objectTween;

    private Tween L2T;

    ColorPicker ColorPicker;

    public bool LockCamera = true;

    #endregion

    private void Start()
    {
        menuSetting = FindObjectOfType<MenuSetting>();
        CamMove = CameraMovement._Instance;
        theLevelManager = FindObjectOfType<LevelManager>();
        skills = FindObjectOfType<Skills>();
        enemySystem = FindObjectOfType<EnemySystem>();
        lineRenderer = GameObject.FindWithTag("LineRenderer").GetComponent<LineRenderer>();
        ColorPicker = ColorPicker._Instance;
        PickUp = true;

        CamMove.LockCamera = LockCamera;
        if (menuSetting != null)
            menuSetting.CheckCameraState();

        camera = Camera.main;

        if (!PlayerPrefs.HasKey("Level14_CamerTut") && theLevelManager.CurrentLevel == 14)
        {
            StartCoroutine(CameraTutCoroutine());
            PlayerPrefs.SetInt("Level14_CamerTut", 1);
        }

        if (theLevelManager.LearningLevels)
        {
            int learnProcess = PlayerPrefs.GetInt("LearnProcess");

            if (learnProcess == 1)
            {
                gameObject.transform.Find("Mortal(BE)").Find("Hand").DOScale(1.2f, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
            }
            if (learnProcess == 2)
            {
                L2T = gameObject.transform.Find("Mortal(BE) (1)").Find("Hand").gameObject.GetComponent<RectTransform>().DOAnchorPosY(100, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Restart);
            }
        }
        else
        {
            if (theLevelManager.CurrentLevel == 1)
            {
                gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.GetComponent<RectTransform>().DOAnchorPosY(-207, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Restart);
            }
            else if (theLevelManager.CurrentLevel == 2)
            {
                gameObject.transform.Find("Mortal(RE)").Find("Hand").gameObject.GetComponent<RectTransform>().DOAnchorPosY(162, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Restart);
            }
            else if (theLevelManager.CurrentLevel == 3)
            {
                gameObject.transform.Find("Mortal(GE)").Find("Hand").gameObject.GetComponent<RectTransform>().DOAnchorPosY(144, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Restart);
            }
        }
    }

    private void Update()
    {
        if (ColorPicker.StopGame) return;

        hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
        hit2 = Physics2D.Raycast(Input.mousePosition, Vector3.zero);

        if (Input.GetMouseButtonDown(0))
        {
            CamMove.isCameraMoveingWaitToClickOver = false;
            theLevelManager.ControlArea.SetActive(false);
            if (hit.collider != null)
            {
                if (theLevelManager.LearningLevels || menuSetting.GameStarted)
                {
                    GameObject draggingObj = hit.collider.gameObject;
                    if (draggingObj.GetComponent<StateMortal>().GetIdentity() == StateMortal.iden.Blue)
                    {
                        SelectedObject = draggingObj.GetComponent<StateMortal>();
                        CanUseDrag = true;
                        CamMove.isDragging = true;

                        lineRenderer.positionCount = 2;
                        LineEffectVisibiltiy = true;
                        lineRenderer.SetPosition(0, SelectedObject.transform.position);
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
        }

        if (Input.GetMouseButton(0))
        {
            if (LineEffectVisibiltiy)
            {
                if (theLevelManager.LearningLevels || menuSetting.GameStarted)
                {
                    if (hit.collider != null && SelectedObject != null)
                    {
                        if (hit.collider != SelectedObject)
                        {
                            if(temporaryObject != null)
                            {
                                temporaryObject.Out.GetComponent<Image>().color = Color.white;
                                temporaryObject.Out.SetActive(false);
                            }

                            temporaryObject = hit.collider.GetComponent<StateMortal>();
                            if (temporaryObject == null) return;
                            lineRenderer.SetPosition(1, temporaryObject.transform.position);

                            bool temprorayBool = SelectedObject.MyTypeOfAttack.Contains(temporaryObject);

                            if (temprorayBool)
                            {
                                lineRenderer.startColor = theLevelManager.LineEffectCMColor;
                                lineRenderer.endColor = theLevelManager.LineEffectCMColor;
                                temporaryObject.Out.GetComponent<Image>().color = theLevelManager.LineEffectCMColor;
                                temporaryObject.Out.SetActive(true);
                            }
                            else
                            {
                                lineRenderer.startColor = theLevelManager.LineEffectMissColor;
                                lineRenderer.endColor = theLevelManager.LineEffectMissColor;
                                if (temporaryObject != SelectedObject)
                                {
                                    temporaryObject.Out.GetComponent<Image>().color = theLevelManager.LineEffectMissColor;
                                    temporaryObject.Out.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }

        if ((Input.GetMouseButtonUp(0) && theLevelManager.LearningLevels) || Input.GetMouseButtonUp(0) && menuSetting.GameStarted && Time.timeScale > 0)
        {
            if (LineEffectVisibiltiy)
            {
                LineEffectVisibiltiy = false;
                lineRenderer.positionCount = 0;
                if (temporaryObject != null)
                {
                    temporaryObject.Out.GetComponent<Image>().color = Color.white;
                    temporaryObject.Out.SetActive(false);
                }
            }

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

                        //MyBlue.transform.DOScale(0.85f, 0.3f).SetEase(Ease.Linear).From().SetUpdate(true);

                        MyBlue.DoScaleMortal();

                        theLevelManager.InitializeAttack(MyBlue);


                        if (theLevelManager.LearningLevels)
                        {
                            int learnProcess = PlayerPrefs.GetInt("LearnProcess");

                            if (learnProcess == 1)
                            {
                                gameObject.transform.Find("Mortal(BE)").Find("Hand").gameObject.SetActive(false);

                                gameObject.transform.Find("Mortal(DE)").Find("Hand").DOScale(1.2f, 1).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
                            }
                        }
                        else
                        {
                            if (theLevelManager.CurrentLevel == 1)
                            {
                                gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.SetActive(false);
                            }
                            else if (theLevelManager.CurrentLevel == 2)
                            {
                                gameObject.transform.Find("Mortal(RE)").Find("Hand").gameObject.SetActive(false);
                            }
                            else if (theLevelManager.CurrentLevel == 3)
                            {
                                gameObject.transform.Find("Mortal(GE)").Find("Hand").gameObject.SetActive(false);
                            }
                        }

                        if (theLevelManager.handOperaion != null && theLevelManager.handOperaion.gameObject.activeSelf)
                        {
                            theLevelManager.handOperaion.gameObject.SetActive(false);
                        }
                        CamMove.isDragging = false;

                    }
                    else
                    {
                        SelectedObject = null;
                        CanUseDrag = false;
                        CamMove.isDragging = false;
                    }
                }

                SelectedObject = null;
                CanUseDrag = false;
                CamMove.isDragging = false;

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
                CamMove.PressCount++;
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

    public IEnumerator CameraTutCoroutine()
    {
        yield return new WaitForSeconds(2f);
        menuSetting.ShowCameraTut();
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

                MyBlue.Out.SetActive(true);
                MyBlue.Out.GetComponent<Image>().color = Color.white;

                if (theLevelManager.handOperaion != null && theLevelManager.handOperaion.gameObject.activeSelf)
                {
                    theLevelManager.handOperaion.gameObject.SetActive(false);
                }

                if (theLevelManager.LearningLevels)
                {
                    int learnProcess = PlayerPrefs.GetInt("LearnProcess");

                    if (learnProcess == 1)
                    {
                        gameObject.transform.Find("Mortal(BE)").Find("Hand").gameObject.SetActive(false);
                        gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.SetActive(true);

                        gameObject.transform.Find("Mortal(DE)").Find("Hand").DOScale(1.2f, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
                    }
                }
                else
                {
                    if (theLevelManager.CurrentLevel == 1)
                    {
                        gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.SetActive(false);
                    }
                    else if (theLevelManager.CurrentLevel == 2)
                    {
                        gameObject.transform.Find("Mortal(RE)").Find("Hand").gameObject.SetActive(false);
                    }
                    else if (theLevelManager.CurrentLevel == 3)
                    {
                        gameObject.transform.Find("Mortal(GE)").Find("Hand").gameObject.SetActive(false);
                    }
                }

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 4, true);

                theLevelManager.ControlArea.SetActive(true);


                MyBlue.DoScaleMortal();

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

            //if (theLevelManager.AllowVibration)
            //    Vibration.Vibrate(40, 5, true);

            MyBlue.Out.SetActive(false);

            obj.DoShakeMortal();

            if (theLevelManager.LearningLevels)
            {
                int learnProcess = PlayerPrefs.GetInt("LearnProcess");

                if (learnProcess == 2)
                {
                    if (L2T != null)
                    {
                        L2T.Kill(true);

                        gameObject.transform.Find("Mortal(BE) (1)").Find("Hand").gameObject.GetComponent<RectTransform>().DOAnchorPosY(100, 0);
                        gameObject.transform.Find("Mortal(BE) (1)").Find("Hand").gameObject.GetComponent<RectTransform>().DOAnchorPosX(190, 1f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Restart);
                    }
                }
            }

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
                MyBlue.SetMortalColors(BlueColor, BlueColorText);
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

                MyBlue.Out.SetActive(false);

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                obj.DoShakeMortal();

                theLevelManager.AttackNoneColorSound.Play();


                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, NoneColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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

                MyBlue.Out.SetActive(false);

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                obj.DoShakeMortal();

                if (theLevelManager.LearningLevels)
                {
                    int learnProcess = PlayerPrefs.GetInt("LearnProcess");

                    if (learnProcess == 1)
                    {
                        if (gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.activeSelf == true)
                        {
                            gameObject.transform.Find("Mortal(DE)").Find("Hand").gameObject.SetActive(false);
                        }
                    }

                    if (learnProcess == 2)
                    {
                        gameObject.transform.Find("Mortal(BE) (1)").Find("Hand").gameObject.SetActive(false);
                    }
                }


                theLevelManager.AttackRedColorSound.Play();
                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, RedColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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
                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                MyBlue.Out.SetActive(false);

                obj.DoShakeMortal();

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, YellowColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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

                MyBlue.Out.SetActive(false);

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                obj.DoShakeMortal();

                theLevelManager.AttackSound.Play();
                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, PinkColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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

                MyBlue.Out.SetActive(false);

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                obj.DoShakeMortal();


                theLevelManager.AttackSound.Play();
                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, GreenColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                MyBlue.Out.SetActive(false);

                obj.DoShakeMortal();


                theLevelManager.AttackYellowColorSound.Play();
                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, OrangeColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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

                MyBlue.Out.SetActive(false);

                //if (theLevelManager.AllowVibration)
                //    Vibration.Vibrate(45, 5, true);

                obj.DoShakeMortal();

                theLevelManager.AttackYellowColorSound.Play();
                CamMove.Shake(0.11f, 0.15f, 0.05f, 0.25f);

                MyBlue.ArmyBurning(obj.gameObject, BlueColor, LastColor, objBurnValue);

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
                    MyBlue.SetMortalColors(BlueColor, BlueColorText);
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