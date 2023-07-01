using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Identity : MonoBehaviour
{
    public enum iden { Red, Blue, None, Yellow , Pink , Green , Orange , LastColor }
    [SerializeField] iden MyIden;


    public iden GetIden() { return MyIden; }
    public iden SetIden(iden newIden) { return MyIden = newIden; } 
}