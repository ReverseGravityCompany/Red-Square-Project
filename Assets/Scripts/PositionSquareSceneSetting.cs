using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PositionSquareSceneSetting : MonoBehaviour
{
    public bool SetList;
    public bool GetList;
    public List<Vector3> SquareList  = new List<Vector3>();

    #if UNITY_EDITOR
    private void Update() {
        if(SetList){
         SetListMethod();
         SetList = false;
        }
        if(GetList){
           GetListMethod();
           GetList = false;
        }
    }
    private void SetListMethod(){
      for(int i = 0 ; i < SquareList.Count;i++){
           transform.GetChild(i).position = SquareList[i];
       }
    }

    private void GetListMethod(){
       for(int i = 0 ; i < transform.childCount;i++){
           SquareList.Add(transform.GetChild(i).position);
       }
    }
    #endif

}
