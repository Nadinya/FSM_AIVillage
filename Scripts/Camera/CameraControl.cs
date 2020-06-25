using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float CameraMoveSensitivity;
    public float CameraRotateSensitivity;
    public float CameraZoomSensitivity;
    public bool InvertCamera;

    private float TranZ;
    private float TranX;
    private float RotateY;
    private float ZoomAmount;
    private float mouseRotateX;
    private float mouseRotateY;

    void Start()
    {

    }

    void Update()
    {
        Move();
        Rotate();
        Zoom();
    }

    void Move()
    {
        if (Input.GetButton("Vertical"))
        {
            TranZ = ((Time.deltaTime * CameraMoveSensitivity) * Input.GetAxisRaw("Vertical"));
            Vector3 _Forward = transform.forward * TranZ;
            transform.position = new Vector3(transform.position.x + _Forward.x, transform.position.y, transform.position.z + _Forward.z);
        }
        if (Input.GetButton("Horizontal"))
        {
            TranX = ((Time.deltaTime * CameraMoveSensitivity) * Input.GetAxisRaw("Horizontal"));
            Vector3 _Sideways = transform.right * TranX;
            transform.position = new Vector3(transform.position.x + _Sideways.x, transform.position.y, transform.position.z + _Sideways.z);
        }
    }

    void Rotate()
    {
        if (Input.GetButton("Yaw"))
        {
            mouseRotateY += CameraRotateSensitivity * Input.GetAxisRaw("Yaw");

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, mouseRotateY, 0.0f);
        }
        if (Input.GetMouseButton(2))
        {
            mouseRotateY += CameraRotateSensitivity * Input.GetAxis("Mouse X");
            mouseRotateX -= CameraRotateSensitivity * Input.GetAxis("Mouse Y");

            mouseRotateX = Mathf.Clamp(mouseRotateX, -70, 70);

            transform.eulerAngles = new Vector3(mouseRotateX, mouseRotateY, 0.0f);
        }
    }

    void Zoom()
    {
        float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scrollWheel != 0)
        {
            ZoomAmount = (Time.deltaTime * CameraZoomSensitivity) * scrollWheel;
            Vector3 _Zoom = transform.forward * ZoomAmount;
            transform.position += _Zoom;

        }
    }
}
