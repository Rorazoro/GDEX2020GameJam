using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoomControls : MonoBehaviour
{
    public float CameraSpeedMetersPerSecond = 1;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        float hSpeed = Input.GetAxis("Mouse X") * Time.deltaTime * CameraSpeedMetersPerSecond;
        float vSpeed = Input.GetAxis("Mouse Y") * Time.deltaTime * CameraSpeedMetersPerSecond;

        transform.RotateAround(transform.position, Vector3.up, hSpeed);
        transform.RotateAround(transform.position, transform.right, -vSpeed);
    }

}
