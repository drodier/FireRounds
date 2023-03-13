using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 mapSize;
    public float cameraSpeed;
    public bool isLocked = true;
    public float mouseSensitivity = 2f;
    public float cameraVerticalRotation = 0f;
    public float cameraHorizontalRotation = 0f;

    private bool boosting;
    private bool isHeld;


    void Start()
    {
        
    }

    void Update()
    {
        boosting = Input.GetKey(KeyCode.LeftShift);
        isHeld = Input.GetKey(KeyCode.LeftAlt);

        if(!isHeld && !isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
            GetComponentInChildren<Camera>().transform.localEulerAngles = Vector3.right*cameraVerticalRotation;

            cameraHorizontalRotation += inputX;
            transform.localEulerAngles = Vector3.up*cameraHorizontalRotation;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        if(!isHeld && !isLocked)
        {
            transform.position += transform.forward * Input.GetAxis("Vertical") * cameraSpeed * (boosting ? 2 : 1) * Time.deltaTime;
            transform.position += transform.right * Input.GetAxis("Horizontal") * cameraSpeed * (boosting ? 2 : 1) * Time.deltaTime;
        }

    }
}
