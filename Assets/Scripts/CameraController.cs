using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    [Range (0f, 1f)] public float LookSpeed = 0.5f;
    public bool InvertY = false;
    private CinemachineVirtualCamera _vcam;

    public void Start () {
        _vcam = GetComponent<CinemachineVirtualCamera> ();
    }

    // Update the look movement each time the event is trigger
    public void OnLook (InputAction.CallbackContext context) {
        //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
        // Vector2 lookMovement = context.ReadValue<Vector2> ().normalized;
        // lookMovement.y = InvertY ? -lookMovement.y : lookMovement.y;

        // // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        // lookMovement.x = lookMovement.x * 180f;

        // //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        // _vcam.m_XAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
        // _vcam.m_YAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;
    }
}