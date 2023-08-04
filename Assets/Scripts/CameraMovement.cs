using UnityEngine;
using CnControls;
using EZCameraShake;

public class CameraMovement : MonoBehaviour
{
    public float Speed = 4f;
    [HideInInspector] public bool isCameraMoving = false;

    public float zoomOutMin = 3;
    public float zoomOutMax = 8;

    [HideInInspector] public bool isCameraMoveingWaitToClickOver;

    [HideInInspector] public bool isDragging;

    [HideInInspector] public Camera cam;

    CameraShaker cameraShaker;

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
        cam = Camera.main;
    }

    private void Start()
    {
        Application.targetFrameRate = 31;

        
        cameraShaker = CameraShaker.Instance;
    }

    private void LateUpdate()
    {
        float MoveX = CnInputManager.GetAxis("Horizontal");
        float MoveY = CnInputManager.GetAxis("Vertical");

        if (CnInputManager.TouchCount == 2)
        {
            MoveX = 0;
            MoveY = 0;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            
            Vector2 touchZeroPrev = (touchZero.position - touchZero.deltaPosition);
            Vector2 touchOnePrev = (touchOne.position - touchOne.deltaPosition);

            float prevMagnitude = (touchOnePrev - touchZeroPrev).magnitude;
            float CurrentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = CurrentMagnitude - prevMagnitude;

            if (difference > 0.5f) difference = 16f;
            else if (difference < -0.5f) difference = -16f;

            zoom(difference * 0.01f);
        }
        else
        {
            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }


        Vector3 Move = Vector3.zero;

        if (Mathf.Abs(MoveX) > 0.7f || Mathf.Abs(MoveY) > 0.7f)
            Move = new Vector3(MoveX * Speed * Time.deltaTime, MoveY * Speed * Time.deltaTime, 0f);
        else
            Move = Vector3.zero;

        if (!isDragging && Move != Vector3.zero)
            cam.transform.Translate(Move);

        if (MoveX == 0 && MoveY == 0)
        {
            isCameraMoving = false;
        }
        else
        {
            if (!isDragging)
            {
                isCameraMoving = true;
                isCameraMoveingWaitToClickOver = true;
            }
        }   
    }

    void zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + increment, zoomOutMin, zoomOutMax);
    }

    public void Shake(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
    {
        cameraShaker.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}

