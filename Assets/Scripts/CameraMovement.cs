using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using EZCameraShake;

public class CameraMovement : MonoBehaviour
{
    public float Speed = 5;
    [HideInInspector] public bool isCameraMoving = false;

    public float zoomOutMin = 3;
    public float zoomOutMax = 8;

    [HideInInspector] public bool isCameraMoveingWaitToClickOver;

    [HideInInspector] public bool isDragging;

    [HideInInspector] public Vector2 Target;
    public float TargetOffset;
    public float CameraLerpSpeed;
    [HideInInspector] public bool Focusing;

    public static CameraMovement _Instance { get; private set; }

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _Instance = this;
        }
    }

    private void Start()
    {
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

        Vector3 Move = new Vector3(MoveX, MoveY, 0f);

        if (!isDragging)
            Camera.main.transform.Translate(Move);

        if (MoveX == 0 && MoveY == 0)
        {
            isCameraMoving = false;
        }
        else
        {
            if (!isDragging)
            {
                Focusing = false;
                Target = Vector2.zero;
                isCameraMoving = true;
                isCameraMoveingWaitToClickOver = true;
            }
        }

        if (Focusing && !isCameraMoving)
        {
            if (Vector2.Distance(transform.position, Target) > 0.1f)
            {
                transform.position = Vector2.Lerp(transform.position, Target, CameraLerpSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            }
            else
            {
                Focusing = false;
                Target = Vector2.zero;
            }

        }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }


    public void Shake(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
    {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }


    //public IEnumerator Shake(float duration, float magnitude)
    //{
    //    Vector3 orignalPosition = transform.position;
    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;

    //        transform.position = new Vector3(x, y, -10f);
    //        elapsed += Time.deltaTime;
    //        yield return 0;
    //    }
    //    transform.position = orignalPosition;
    //}
}

