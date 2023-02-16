using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private bool locked;
    private float maxZoom = 2f;
    private float minZoom = 8f;
    private float amountToMove;
    private float horizontalAxis = 0f;
    private float verticalAxis = 0f;

    public float cameraSpeed = 0.5f;
    public float zoomSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!locked)
        {
            horizontalAxis = Input.GetAxis("Horizontal Camera");
            verticalAxis = Input.GetAxis("Vertical Camera");

            if(Input.mousePosition.x <= 50)
                horizontalAxis = -1f;
            if(Input.mousePosition.x >= 1050)
                horizontalAxis = 1f;
            
            if(Input.mousePosition.y <= 50)
                verticalAxis = -1f;
            if(Input.mousePosition.y >= 480)
                verticalAxis = 1f;

            amountToMove = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;

            if(cam.orthographicSize == maxZoom && amountToMove > 0)
            {
                amountToMove = 0;
            }
            else if(cam.orthographicSize == minZoom && amountToMove < 0)
            {
                amountToMove = 0;
            }

            cam.orthographicSize -= amountToMove;
        }
    }

    void FixedUpdate()
    {
        if(!locked)
        {
            cam.orthographicSize = cam.orthographicSize < maxZoom ? maxZoom : cam.orthographicSize > minZoom ? minZoom : cam.orthographicSize;

            cam.transform.position += new Vector3(horizontalAxis, verticalAxis, 0) * cameraSpeed * Time.deltaTime;
        }
    }

    public void toggleCameraLock()
    {
        locked = !locked;
    }
}
