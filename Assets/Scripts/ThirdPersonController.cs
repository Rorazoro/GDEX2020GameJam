using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (CharacterController))]
public class ThirdPersonController : MonoBehaviour {
    private CharacterController _controller;
    private Vector2 _moveInput = new Vector2 ();
    private Vector2 _lookInput = new Vector2 ();
    private Quaternion nextRotation;
    private Vector3 nextPosition;
    private float verticalSpeed;

    public GameObject followTransform;
    [Range (0f, 10f)] public float MoveSpeed = 6f;
    [Range (0f, 1f)] public float LookSpeed = 0.5f;
    [Range (0f, 10f)] public float Gravity = 5f;
    [Range (0f, 10f)] public float PushPower = 5f;

    public void OnMove (InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2> ();
    }

    public void OnLook (InputAction.CallbackContext context) {
        _lookInput = context.ReadValue<Vector2> ();
    }

    private void Awake () {
        _controller = GetComponent<CharacterController> ();
    }

    private void Update () {
        Move ();
    }

    private void Move () {
        //Move the player based on the X input
        transform.rotation *= Quaternion.AngleAxis (_lookInput.x * LookSpeed, Vector3.up);

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis (_lookInput.x * LookSpeed, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis (_lookInput.y * LookSpeed, Vector3.right);

        Vector3 angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        float angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340) {
            angles.x = 340;
        } else if (angle < 180 && angle > 40) {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;

        nextRotation = Quaternion.Lerp (followTransform.transform.rotation, nextRotation, Time.deltaTime * LookSpeed);

        if (_moveInput.x == 0 && _moveInput.y == 0) {
            nextPosition = transform.position;

            // transform.rotation = Quaternion.Euler (0, followTransform.transform.rotation.eulerAngles.y, 0);
            // followTransform.transform.localEulerAngles = new Vector3 (angles.x, 0, 0);

            return;
        }

        Vector3 position = (transform.forward * _moveInput.y * MoveSpeed) + (transform.right * _moveInput.x * MoveSpeed);
        nextPosition = transform.position + position;

        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler (0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3 (angles.x, 0, 0);

        //Calculate vertical speed and gravity
        if (_controller.isGrounded) {
            verticalSpeed = 0;
        }

        verticalSpeed -= Gravity;
        position.y = verticalSpeed;

        //Check for steps

        RaycastHit hit;
        if (Physics.Raycast (transform.position, transform.forward, out hit, 1)) {
            if (hit.collider != null && hit.collider.gameObject.CompareTag ("Step")) {
                _controller.stepOffset = 0.4f;
            }
        }

        //Move player
        _controller.Move (position.normalized * MoveSpeed * Time.deltaTime);
        _controller.stepOffset = 0f;
    }

    private void OnControllerColliderHit (ControllerColliderHit hit) {
        Rigidbody targetrb = hit.collider.attachedRigidbody;

        //no rigidbody
        if (targetrb == null || targetrb.isKinematic) {
            return;
        }
        //We don't want to push objects below us
        // if (hit.moveDirection.y < -0.3) {
        //     return;
        // }

        //Get push direction
        Vector3 pushDir = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
        targetrb.velocity = pushDir * PushPower;
    }
}