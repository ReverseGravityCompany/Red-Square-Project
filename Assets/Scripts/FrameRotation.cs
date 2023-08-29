using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRotation : MonoBehaviour
{
    public RectTransform rectTransform;
    public float speed = 15;
    void LateUpdate()
    {
        rectTransform.Rotate(new Vector3(0,0,speed * Time.deltaTime));
    }
}
