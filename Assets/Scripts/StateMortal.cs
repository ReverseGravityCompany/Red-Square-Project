﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMortal : MonoBehaviour
{
    public List<GameObject> MyTypeOfAttack;
    public List<GameObject> CantAttack;
    private BoxCollider2D boxCollider2d;
    [SerializeField] LayerMask SquareLayer;


    public Dictionary<GameObject, string> FixedAttackTypes;


    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
        FixedAttackTypes = new Dictionary<GameObject, string>();
        float extraHeightText = 0.2f;
        MyTypeOfAttack.Clear();
        RaycastHit2D raycastHitDown = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y - boxCollider2d.bounds.extents.y - 0.1f, boxCollider2d.bounds.center.z), Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        RaycastHit2D raycastHitUp = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y + boxCollider2d.bounds.extents.y + 0.1f, boxCollider2d.bounds.center.z), Vector2.up, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x + boxCollider2d.bounds.extents.x + 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.right, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x - boxCollider2d.bounds.extents.x - 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.left, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
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

    public void ShowTypeOfAttack()
    {
        for (int i = 0; i < MyTypeOfAttack.Count; i++)
        {
            MyTypeOfAttack[i].GetComponent<EPOOutline.Outlinable>().enabled = true;
            MyTypeOfAttack[i].GetComponent<SquareClass>().CanAttack = true;
        }
    }

    public void WhoCantAttack(GameObject[] AllMortal, GameObject OwnGameObject)
    {
        CantAttack.Clear();

        foreach (GameObject g in AllMortal)
        {
            CantAttack.Add(g);
        }

        foreach (GameObject m in MyTypeOfAttack)
        {
            CantAttack.Remove(m);
            m.transform.Find("Fx").gameObject.SetActive(false);
        }

        CantAttack.Remove(OwnGameObject);

        foreach (GameObject g in CantAttack)
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

        foreach (GameObject g in CantAttack)
        {
            g.transform.Find("Fx").gameObject.SetActive(false);
        }
    }

    public void ResetTypeOfAttackData()
    {
        float extraHeightText = 0.2f;
        MyTypeOfAttack.Clear();
        RaycastHit2D raycastHitDown = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y - boxCollider2d.bounds.extents.y - 0.1f, boxCollider2d.bounds.center.z), Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        RaycastHit2D raycastHitUp = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x, boxCollider2d.bounds.center.y + boxCollider2d.bounds.extents.y + 0.1f, boxCollider2d.bounds.center.z), Vector2.up, boxCollider2d.bounds.extents.y + extraHeightText, SquareLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x + boxCollider2d.bounds.extents.x + 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.right, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x - boxCollider2d.bounds.extents.x - 0.1f, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.left, boxCollider2d.bounds.extents.x + extraHeightText, SquareLayer);
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

    public void LineConnections(GameObject targetMortal, Vector3 pos , Color LineColor)
    {
        foreach (var obj in FixedAttackTypes)
        {
            if (obj.Key == targetMortal)
            {
                if (obj.Value == "DOWN")
                {
                    GameObject Line = ObjectPooler._Instance.SpawnFromPool("LineConnection", pos + new Vector3(0, -0.5f, 0), new Vector3(0, 0, 0));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
                else if (obj.Value == "UP")
                {
                    GameObject Line = ObjectPooler._Instance.SpawnFromPool("LineConnection", pos + new Vector3(0, +0.5f, 0), new Vector3(0, 0, 0));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
                else if (obj.Value == "RIGHT")
                {
                    GameObject Line = ObjectPooler._Instance.SpawnFromPool("LineConnection", pos + new Vector3(+0.5f, 0, 0), new Vector3(0, 0, 90));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
                else if (obj.Value == "LEFT")
                {
                    GameObject Line = ObjectPooler._Instance.SpawnFromPool("LineConnection", pos + new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 90));
                    Line.GetComponent<SpriteRenderer>().color = LineColor;
                }
            }
        }
    }




    // private void Update() {

    //             Debug.DrawRay(new Vector3(boxCollider2d.bounds.center.x,boxCollider2d.bounds.center.y - boxCollider2d.bounds.extents.y - 0.1f,boxCollider2d.bounds.center.z),Vector2.down * (boxCollider2d.bounds.extents.y + 0.2f),Color.yellow);
    //             Debug.DrawRay(new Vector3(boxCollider2d.bounds.center.x,boxCollider2d.bounds.center.y + boxCollider2d.bounds.extents.y + 0.1f,boxCollider2d.bounds.center.z),Vector2.up * (boxCollider2d.bounds.extents.y + 0.2f),Color.yellow);
    //             Debug.DrawRay(new Vector3(boxCollider2d.bounds.center.x + boxCollider2d.bounds.extents.x + 0.1f,boxCollider2d.bounds.center.y,boxCollider2d.bounds.center.z),Vector2.right * (boxCollider2d.bounds.extents.x + 0.2f),Color.yellow);
    //             Debug.DrawRay(new Vector3(boxCollider2d.bounds.center.x - boxCollider2d.bounds.extents.x - 0.1f,boxCollider2d.bounds.center.y,boxCollider2d.bounds.center.z),Vector2.left * (boxCollider2d.bounds.extents.x + 0.2f),Color.yellow);
    // }


}
