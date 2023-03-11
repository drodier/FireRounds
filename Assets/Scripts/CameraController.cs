using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 mapSize;
    public float cameraSpeed;

    private Vector3 middlePoint;
    private bool boosting;


    void Start()
    {
        middlePoint = new Vector3(mapSize.x/2-0.5f, 4, mapSize.y/2-0.5f);
    }

    void Update()
    {
        boosting = Input.GetKey(KeyCode.LeftShift);
    }

    void FixedUpdate()
    {
        transform.RotateAround(middlePoint,
                                new Vector3(0, 1, 0),
                                Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime * -1 * (boosting ? 2 : 1));
    }
}
