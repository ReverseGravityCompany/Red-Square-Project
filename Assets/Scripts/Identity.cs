using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Identity : MonoBehaviour
{
    public enum iden { Red, Blue, None, Yellow , Pink , Green , Orange , LastColor }
    [SerializeField] iden MyIden;


    public iden GetIdentity() { return MyIden; }
    public iden SetIdentity(iden newIden) { return MyIden = newIden; } 
}