using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomethingWrongCode : MonoBehaviour
{

    private void LateUpdate() {
        if(gameObject.activeInHierarchy){
            StartCoroutine(tryDeactive());
        }
    }

    public IEnumerator tryDeactive(){
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);
    }

}
