using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class CameraMovement : MonoBehaviour
{
    public float Speed = 5;
    public bool isCameraMoving = false;

    public float zoomOutMin = 3;
    public float zoomOutMax = 8;

    public bool isCameraMoveingWaitToClickOver;

    private void Start() {
        Speed = 4;
    }

    private void Update()
    {
        float MoveX = CnInputManager.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float MoveY = CnInputManager.GetAxis("Vertical") * Speed * Time.deltaTime;

        if (Input.touchCount == 2)
        {
            MoveX = 0;
            MoveY = 0;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchOnePrev - touchZeroPrev).magnitude;
            float CurrentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = CurrentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);


            
        }

       Vector3 Move = new Vector3(MoveX, MoveY , 0f);


        Camera.main.transform.Translate(Move);

        if (MoveX == 0 && MoveY == 0)
            isCameraMoving = false;
        else{
            isCameraMoving = true;
            isCameraMoveingWaitToClickOver = true;
        }
         



        zoom(Input.GetAxis("Mouse ScrollWheel"));

    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}

