using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;

public class StateMortal : MonoBehaviour
{
    #region Propreties StateMortal
    public List<StateMortal> MyTypeOfAttack;
    public List<StateMortal> N_Attack_list;
    private BoxCollider2D boxCollider2d;
    [SerializeField] LayerMask SquareLayer;

    public Dictionary<GameObject, string> FixedAttackTypes;

    ObjectPooler objectPooler;

    RaycastHit2D raycastHitDown;
    RaycastHit2D raycastHitUp;
    RaycastHit2D raycastHitRight;
    RaycastHit2D raycastHitLeft;

    ParticleSystem ArmyParticle;
    ParticleSystem.MainModule ArmyMain;
    ParticleSystem.MinMaxGradient MinMaxGradient;
    ParticleSystem.EmissionModule ArmyEmmision;
    ParticleSystem.Burst burst;

    [HideInInspector] public GameObject Out; // transform.Find("Out").gameObject
    [HideInInspector] public GameObject Fx; // transform.Find("Fx").gameObject
    [HideInInspector] public Image StateMortalImage;
    [HideInInspector] public Text StateMortalText;

    private LevelManager theLevelManager;

    #endregion

    #region Properties SquareClass
    [HideInInspector] public bool CanPush = false;
    [HideInInspector] public bool CanAttack = false;
    [HideInInspector] public bool isUnderAttack = false;
    private Player thePlayer;

    [HideInInspector] public bool TurboMortal, CopacityMortal, RandomChangeMortal, AllAttackMortal, X2Mortal, MaxSpaceMortal;
    public Color WhiteLow;
    public Color InvisibaleColor;

    private Skills skills;

    [HideInInspector] public Image star1, star2, star3;

    private int targetFrame;
    private int frameCount;

    #endregion

    #region Propreties IncreaseMortal
    [HideInInspector] public Text ShowMortal;
    public int MaxSpace = 100;
    public int CurrentCount;
    public int AmountIncrease = 1;
    public bool NoneAmount = false;
    [HideInInspector] public bool isHaveAnySpace;
    #endregion

    #region Propreties Identity
    public enum iden { Red, Blue, None, Yellow, Pink, Green, Orange, LastColor }
    [SerializeField] private iden MyIden;


    public iden GetIdentity() { return MyIden; }
    public iden SetIdentity(iden newIden) { return MyIden = newIden; }

    #endregion

    private void Awake()
    {
        Out = transform.Find("Out").gameObject;
        Fx = transform.Find("Fx").gameObject;
        StateMortalImage = GetComponent<Image>();
        StateMortalText = transform.Find("CountMortal").GetComponent<Text>();
    }

    private void Start()
    {
        theLevelManager = LevelManager._Instance;
        objectPooler = ObjectPooler._Instance;


        #region Raycast To Detect Attack Directions
        boxCollider2d = GetComponent<BoxCollider2D>();
        FixedAttackTypes = new Dictionary<GameObject, string>();
        float extraHeightText = 0.2f;
        MyTypeOfAttack.Clear();
        raycastHitDown = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y - boxCollider2d.bounds.extents.y - 0.1f, boxCollider2d.bounds.center.z), Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        raycastHitUp = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y + boxCollider2d.bounds.extents.y + 0.1f, boxCollider2d.bounds.center.z), Vector2.up, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        raycastHitRight = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x + boxCollider2d.bounds.extents.x + 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.right, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
        raycastHitLeft = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x - boxCollider2d.bounds.extents.x - 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.left, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
        if (raycastHitDown.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitDown.collider.GetComponent<StateMortal>());
            FixedAttackTypes.Add(raycastHitDown.collider.gameObject, "DOWN");
        }
        if (raycastHitUp.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitUp.collider.GetComponent<StateMortal>());
            FixedAttackTypes.Add(raycastHitUp.collider.gameObject, "UP");
        }
        if (raycastHitRight.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitRight.collider.GetComponent<StateMortal>());
            FixedAttackTypes.Add(raycastHitRight.collider.gameObject, "RIGHT");
        }
        if (raycastHitLeft.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitLeft.collider.GetComponent<StateMortal>());
            FixedAttackTypes.Add(raycastHitLeft.collider.gameObject, "LEFT");
        }

        #endregion

        ArmyParticle = GameObject.Find("BurnArmyParticleEffect").GetComponent<ParticleSystem>();
        ArmyMain = ArmyParticle.main;
        MinMaxGradient = new MinMaxGradient(Color.white, Color.blue);
        ArmyEmmision = ArmyParticle.emission;

        // SQUARE CLASS
        thePlayer = FindObjectOfType<Player>();
        skills = Skills._Instance;

        star1 = transform.Find("Star1").GetComponent<Image>();
        star2 = transform.Find("Star2").GetComponent<Image>();
        star3 = transform.Find("Star3").GetComponent<Image>();

        star1.GetComponent<Image>().color = thePlayer.BlueColorText;
        star2.GetComponent<Image>().color = thePlayer.BlueColorText;
        star3.GetComponent<Image>().color = thePlayer.BlueColorText;

        // INCREASE MORTAL

        if (AmountIncrease == 2)
        {
            UpdateDoubleX();
        }

        ShowMortal = transform.Find("CountMortal").GetComponent<Text>();

        if (NoneAmount && GetIdentity() == iden.None)
        {
            AmountIncrease = 0;
        }

        targetFrame = Random.Range(10, 30);
    }

    private void Update()
    {
        ShowMortal.text = CurrentCount.ToString();

        if (CurrentCount >= 1000)
        {
            CurrentCount = 1000;
            if (StateMortalText.fontSize != 60)
                StateMortalText.fontSize = 60;
        }
        else
        {
            if (StateMortalText.fontSize != 70)
                StateMortalText.fontSize = 70;
        }
    }

    private void LateUpdate()
    {
        frameCount++;
        if (frameCount % targetFrame == 0)
        {
            if (StateMortalImage.color == thePlayer.BlueColor)
            {
                if(GetIdentity() != iden.Blue)
                {
                    SetMortalColors(thePlayer.BlueColor, thePlayer.BlueColorText);
                }
            }
            else if(StateMortalImage.color == thePlayer.RedColor)
            {
                if (GetIdentity() != iden.Red)
                {
                    SetMortalColors(thePlayer.RedColor, thePlayer.RedColorText);
                }
            }
            else if (StateMortalImage.color == thePlayer.YellowColor)
            {
                if (GetIdentity() != iden.Yellow)
                {
                    SetMortalColors(thePlayer.YellowColor, thePlayer.YellowColorText);
                }
            }
            else if (StateMortalImage.color == thePlayer.PinkColor)
            {
                if (GetIdentity() != iden.Pink)
                {
                    SetMortalColors(thePlayer.PinkColor, thePlayer.PinkColorText);
                }
            }
            else if (StateMortalImage.color == thePlayer.GreenColor)
            {
                if (GetIdentity() != iden.Green)
                {
                    SetMortalColors(thePlayer.GreenColor, thePlayer.GreenColorText);
                }
            }
            else if (StateMortalImage.color == thePlayer.OrangeColor)
            {
                if (GetIdentity() != iden.Orange)
                {
                    SetMortalColors(thePlayer.OrangeColor, thePlayer.OrangeColorText);
                }
            }
            else if (StateMortalImage.color == thePlayer.LastColor)
            {
                if (GetIdentity() != iden.LastColor)
                {
                    SetMortalColors(thePlayer.LastColor, thePlayer.LastColorText);
                }
            }

            frameCount = 0;
        }
    }

    public void ValidateMortal()
    {
        AmountIncrease = 1;
    }

    public void UpdateDoubleX()
    {
        StateMortal squareClass = GetComponent<StateMortal>();
        Player player = FindObjectOfType<Player>();
        squareClass.star1.gameObject.SetActive(true);
        StateMortal.iden EnemyColor = gameObject.GetComponent<StateMortal>().GetIdentity();
        switch (EnemyColor)
        {
            case StateMortal.iden.Red:
                squareClass.star1.GetComponent<Image>().color = player.RedColorText;
                break;
            case StateMortal.iden.Yellow:
                squareClass.star1.GetComponent<Image>().color = player.YellowColorText;
                break;
            case StateMortal.iden.Pink:
                squareClass.star1.GetComponent<Image>().color = player.PinkColorText;
                break;
            case StateMortal.iden.Green:
                squareClass.star1.GetComponent<Image>().color = player.GreenColorText;
                break;
            case StateMortal.iden.Orange:
                squareClass.star1.GetComponent<Image>().color = player.OrangeColorText;
                break;
            case StateMortal.iden.LastColor:
                squareClass.star1.GetComponent<Image>().color = player.LastColorText;
                break;
        }
        squareClass.TurboMortal = true;
    }

    public void ShowTypeOfAttack()
    {
        for (int i = 0; i < MyTypeOfAttack.Count; i++)
        {
            MyTypeOfAttack[i].Out.SetActive(true);
            MyTypeOfAttack[i].GetComponent<StateMortal>().CanAttack = true;
        }
    }

    public void Attack_N(List<StateMortal> AllMortal, StateMortal OwnGameObject)
    {
        N_Attack_list.Clear();

        foreach (StateMortal g in AllMortal)
        {
            N_Attack_list.Add(g.GetComponent<StateMortal>());
        }

        foreach (StateMortal m in MyTypeOfAttack)
        {
            N_Attack_list.Remove(m);
            m.Fx.gameObject.SetActive(false);
        }

        N_Attack_list.Remove(OwnGameObject);

        foreach (StateMortal g in N_Attack_list)
        {
            g.Fx.gameObject.SetActive(true);
        }
    }

    public void HideTypeOfAttack()
    {
        for (int i = 0; i < MyTypeOfAttack.Count; i++)
        {
            MyTypeOfAttack[i].Out.SetActive(false);
            MyTypeOfAttack[i].GetComponent<StateMortal>().CanAttack = false;
        }

        foreach (StateMortal g in N_Attack_list)
        {
            g.Fx.SetActive(false);
        }
    }

    public void ResetTypeOfAttackData()
    {
        MyTypeOfAttack.Clear();
        foreach (var item in FixedAttackTypes)
        {
            MyTypeOfAttack.Add(item.Key.GetComponent<StateMortal>());
        }
    }

    public void MaxTypeOfAttack(GameObject obj)
    {
        for (int i = 0; i < theLevelManager.AllMortalObjects.Count; i++)
        {
            MyTypeOfAttack.Add(theLevelManager.AllMortalObjects[i].GetComponent<StateMortal>());
        }
    }

    public void UpdateSkills()
    {
        //Skills
        if (thePlayer.MyBlue != null)
        {
            if (!TurboMortal)
            {
                skills.TurboObj.color = Color.white;
                star1.gameObject.SetActive(false);
                AmountIncrease = 1;
                skills.TurboObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                star1.gameObject.SetActive(true);
                skills.TurboObj.color = WhiteLow;
                skills.TurboObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }

            if (!CopacityMortal)
            {
                skills.CopacityObj.color = Color.white;
                star2.gameObject.SetActive(false);
                MaxSpace = 100;
                skills.CopacityObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                skills.CopacityObj.color = WhiteLow;
                star2.gameObject.SetActive(true);
                skills.CopacityObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!RandomChangeMortal)
            {
                skills.RandomChangeObj.color = Color.white;
                skills.RandomChangeObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                skills.RandomChangeObj.color = WhiteLow;
                skills.RandomChangeObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!AllAttackMortal)
            {
                skills.AllAttackObj.color = Color.white;
                star3.gameObject.SetActive(false);
                skills.AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

            }
            else
            {
                skills.AllAttackObj.color = WhiteLow;
                star3.gameObject.SetActive(true);
                skills.AllAttackObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();

            }

            if (!X2Mortal)
            {
                skills.X2Obj.color = Color.white;
                skills.X2Obj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();

            }
            else
            {
                skills.X2Obj.color = WhiteLow;
                skills.X2Obj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }

            if (!MaxSpaceMortal)
            {
                skills.MaxSpaceObj.color = Color.white;
                skills.MaxSpaceObj.transform.GetChild(0).GetComponent<MaskAnimate>().EnableMask();
            }
            else
            {
                skills.MaxSpaceObj.color = WhiteLow;
                skills.MaxSpaceObj.transform.GetChild(0).GetComponent<MaskAnimate>().disableMask();
            }
        }
    }

    public void ResetEnemyTurbo()
    {
        star1.color = thePlayer.BlueColorText;
        star1.gameObject.SetActive(false);
        TurboMortal = false;
    }

    public void SetMortalColors(Color PrimirayColor, Color SecondaryColor)
    {
        StateMortalImage.color = PrimirayColor;
        StateMortalText.color = SecondaryColor;
    }

    public void ResetInitializeAttack(StateMortal obj)
    {
        theLevelManager.InitializeAttack(obj);
    }

    public void ResetAllSkills()
    {
        TurboMortal = false;
        CopacityMortal = false;
        RandomChangeMortal = false;
        AllAttackMortal = false;
        X2Mortal = false;
        MaxSpaceMortal = false;

        star1.gameObject.SetActive(false);
        gameObject.GetComponent<StateMortal>().AmountIncrease = 1;

        star2.gameObject.SetActive(false);
        gameObject.GetComponent<StateMortal>().MaxSpace = 100;


        star3.gameObject.SetActive(false);
        gameObject.GetComponent<StateMortal>().ResetTypeOfAttackData();
    }

    public void LineConnections(GameObject targetMortal, Vector3 pos, Color LineColor)
    {
        foreach (var obj in FixedAttackTypes)
        {
            if (obj.Key == targetMortal)
            {
                if (obj.Value == "DOWN")
                {
                    GameObject Line = objectPooler.SpawnFromPool("LineConnection", pos + new Vector3(0, -0.5f, 0), new Vector3(0, 0, 0));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
                else if (obj.Value == "UP")
                {
                    GameObject Line = objectPooler.SpawnFromPool("LineConnection", pos + new Vector3(0, +0.5f, 0), new Vector3(0, 0, 0));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
                else if (obj.Value == "RIGHT")
                {
                    GameObject Line = objectPooler.SpawnFromPool("LineConnection", pos + new Vector3(+0.5f, 0, 0), new Vector3(0, 0, 90));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
                else if (obj.Value == "LEFT")
                {
                    GameObject Line = objectPooler.SpawnFromPool("LineConnection", pos + new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 90));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
            }
        }
    }

    public void ArmyBurning(GameObject targetMortal, Color mainColor, Color targetColor, int burstCount)
    {
        if (burstCount >= 100)
        {
            burstCount = 100;
        }

        ArmyParticle.transform.position = targetMortal.transform.position;

        MinMaxGradient.colorMin = mainColor;
        MinMaxGradient.colorMax = targetColor;

        ArmyMain.startColor = MinMaxGradient;
        burst.count = burstCount;
        ArmyEmmision.SetBurst(0, burst);

        ArmyParticle.Play();
    }
}
