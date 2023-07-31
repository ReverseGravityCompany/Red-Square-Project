using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMortal : MonoBehaviour
{
    public List<GameObject> MyTypeOfAttack;
    public List<GameObject> N_Attack_list;
    private BoxCollider2D boxCollider2d;
    [SerializeField] LayerMask SquareLayer;


    public Dictionary<GameObject, string> FixedAttackTypes;

    ObjectPooler objectPooler;

    RaycastHit2D raycastHitDown;
    RaycastHit2D raycastHitUp;
    RaycastHit2D raycastHitRight;
    RaycastHit2D raycastHitLeft;


    private void Awake()
    {
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
            MyTypeOfAttack.Add(raycastHitDown.collider.gameObject);
            FixedAttackTypes.Add(raycastHitDown.collider.gameObject, "DOWN");
        }
        if (raycastHitUp.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitUp.collider.gameObject);
            FixedAttackTypes.Add(raycastHitUp.collider.gameObject, "UP");
        }
        if (raycastHitRight.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitRight.collider.gameObject);
            FixedAttackTypes.Add(raycastHitRight.collider.gameObject, "RIGHT");
        }
        if (raycastHitLeft.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitLeft.collider.gameObject);
            FixedAttackTypes.Add(raycastHitLeft.collider.gameObject, "LEFT");
        }

    }

    private void Start()
    {
        objectPooler = ObjectPooler._Instance;
    }

    public void ShowTypeOfAttack()
    {
        for (int i = 0; i < MyTypeOfAttack.Count; i++)
        {
            MyTypeOfAttack[i].GetComponent<EPOOutline.Outlinable>().enabled = true;
            MyTypeOfAttack[i].GetComponent<SquareClass>().CanAttack = true;
        }
    }

    public void Attack_N(GameObject[] AllMortal, GameObject OwnGameObject)
    {
        N_Attack_list.Clear();

        foreach (GameObject g in AllMortal)
        {
            N_Attack_list.Add(g);
        }

        foreach (GameObject m in MyTypeOfAttack)
        {
            N_Attack_list.Remove(m);
            m.transform.Find("Fx").gameObject.SetActive(false);
        }

        N_Attack_list.Remove(OwnGameObject);

        foreach (GameObject g in N_Attack_list)
        {
            g.transform.Find("Fx").gameObject.SetActive(true);
        }
    }

    public void HideTypeOfAttack()
    {
        for (int i = 0; i < MyTypeOfAttack.Count; i++)
        {
            MyTypeOfAttack[i].GetComponent<EPOOutline.Outlinable>().enabled = false;
            MyTypeOfAttack[i].GetComponent<SquareClass>().CanAttack = false;
        }

        foreach (GameObject g in N_Attack_list)
        {
            g.transform.Find("Fx").gameObject.SetActive(false);
        }
    }

    public void ResetTypeOfAttackData()
    {
        float extraHeightText = 0.2f;
        MyTypeOfAttack.Clear();
        raycastHitDown = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y - boxCollider2d.bounds.extents.y - 0.1f, boxCollider2d.bounds.center.z), Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        raycastHitUp = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y + boxCollider2d.bounds.extents.y + 0.1f, boxCollider2d.bounds.center.z), Vector2.up, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        raycastHitRight = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x + boxCollider2d.bounds.extents.x + 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.right, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
        raycastHitLeft = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x - boxCollider2d.bounds.extents.x - 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.left, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
        if (raycastHitDown.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitDown.collider.gameObject);
        }
        if (raycastHitUp.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitUp.collider.gameObject);
        }
        if (raycastHitRight.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitRight.collider.gameObject);
        }
        if (raycastHitLeft.collider != null)
        {
            MyTypeOfAttack.Add(raycastHitLeft.collider.gameObject);
        }
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
        ParticleSystem ArmyParticle = objectPooler.SpawnFromPool("ArmyParticle",
            targetMortal.transform.position, new Vector3(0, 0, 0)).GetComponent<ParticleSystem>();

        ParticleSystem.MainModule ArmyMain = ArmyParticle.main;
        ArmyMain.startColor = new ParticleSystem.MinMaxGradient(mainColor, targetColor);

        ParticleSystem.EmissionModule ArmyEmmision = ArmyParticle.emission;
        ParticleSystem.Burst burst = new ParticleSystem.Burst();
        burst.count = burstCount;
        burst.time = 0;
        burst.cycleCount = 1;
        burst.probability = 1;
        ArmyEmmision.SetBurst(0, burst);

        ArmyParticle.Play();

    }
}
