using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAndZoom : MonoBehaviour
{
    Vector3 touchStart;
    Vector3 direction;
    public bool isCameraMoving = false;
    public bool TrytoGetOut = false;

    public float zoomOutMin = 3;
    public float zoomOutMax = 12;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TrytoGetOut = false;
        }
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchOnePrev - touchZeroPrev).magnitude;
            float CurrentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = CurrentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);

        }
        else if (Input.GetMouseButton(0))
        {
            direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(Mathf.Abs(direction.magnitude));
            if(direction != Vector3.zero && Mathf.Abs(direction.magnitude) >= 0.4f)
            {
                TrytoGetOut = true;
            }
            if(Mathf.Abs(direction.magnitude) >= 0.4f)
            {
                direction /= 6;
                transform.position += direction;

            }
        }


        if (direction == Vector3.zero) isCameraMoving = false;
        else isCameraMoving = true;


        zoom(Input.GetAxis("Mouse ScrollWheel"));

    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
