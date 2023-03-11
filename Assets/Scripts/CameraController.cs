using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private int currentPosition = 0;

    public Vector3[] positions;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            currentPosition--;
            if(currentPosition < 0)
                currentPosition = positions.Length-1;
            transform.Rotate(new Vector3(0, 1, 0), 90);
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            currentPosition++;
            if(currentPosition >= positions.Length)
                currentPosition = 0;
            transform.Rotate(new Vector3(0, 1, 0), -90);
        }
    }

    void FixedUpdate()
    {
        transform.position = positions[currentPosition];
    }
}
