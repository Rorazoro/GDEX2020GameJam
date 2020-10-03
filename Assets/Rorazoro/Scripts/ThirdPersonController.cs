using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (CharacterController))]
public class ThirdPersonController : MonoBehaviour {
    private CharacterController _controller;
    private Animator _animator;

    [SerializeField]
    private float vSpeed;
    [SerializeField]
    private float velocity;
    private Vector3 followTransformAngles;

    public GameObject followTransform;
    [Range (0f, 100f)] public float MoveSpeed = 30f;
    [Range (0f, 100f)] public float LookSpeed = 10f;
    [Range (0f, 100f)] public float Gravity = 20f;
    [Range (0f, 100f)] public float PushPower = 10f;
    public bool InvertY = false;

    public MinimapPandaScript minimapPandaScript;
    public MinimapScript minimapScript;

    private void Awake () {
        _controller = GetComponent<CharacterController> ();
        _animator = GetComponentInChildren<Animator> ();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update () {
        Rotate ();
        Move ();
        Animate ();

        if (Input.GetKey (KeyCode.Space)) {
            DetectClosestPanda (this.gameObject);
        }
        if (Input.GetKey (KeyCode.Equals)) {
            minimapScript.ZoomIn ();
        }
        if (Input.GetKey (KeyCode.Minus)) {
            minimapScript.ZoomOut ();
        }
    }

    private void Rotate () {
        Vector2 lookInput = InputManager.Instance.LookInput;
        Vector2 moveInput = InputManager.Instance.MoveInput;

        float lookSpeed = LookSpeed / 100;

        //Rotate the Follow Target transform based on the input
        float lookInputY = InvertY ? -lookInput.y : lookInput.y;
        followTransform.transform.rotation *= Quaternion.AngleAxis (lookInput.x * lookSpeed, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis (lookInputY * lookSpeed, Vector3.right);

        followTransformAngles = followTransform.transform.localEulerAngles;
        followTransformAngles.z = 0;

        clampYRotation ();

        if (moveInput.x != 0f || moveInput.y != 0f) {
            //Move the player based on the X input
            transform.rotation *= Quaternion.AngleAxis (lookInput.x * lookSpeed, Vector3.up);
        }
    }

    private void Move () {
        Vector2 moveInput = InputManager.Instance.MoveInput;

        if (moveInput.x != 0f || moveInput.y != 0f) {
            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler (0, followTransform.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3 (followTransformAngles.x, 0, 0);
        }

        Vector3 position = (transform.forward * moveInput.y * MoveSpeed) + (transform.right * moveInput.x * MoveSpeed);

        //Calculate vertical movement and gravity
        if (_controller.isGrounded) {
            vSpeed = 0;
        }

        vSpeed -= Gravity * Time.deltaTime;
        position.y = vSpeed;

        //Check for steps

        RaycastHit hit;
        if (Physics.Raycast (transform.position, transform.forward, out hit, 1)) {
            if (hit.collider != null && hit.collider.gameObject.CompareTag ("Step")) {
                _controller.stepOffset = 0.4f;
            }
        }

        //Move player
        _controller.Move (position.normalized * MoveSpeed * Time.deltaTime);
        velocity = _controller.velocity.magnitude;

        _controller.stepOffset = 0f;
    }

    private void Animate () {
        Vector2 moveInput = InputManager.Instance.MoveInput;

        _animator.SetFloat ("velocity", velocity);
        _animator.SetFloat ("xInput", moveInput.x);
        _animator.SetFloat ("yInput", moveInput.y);
    }

    private void clampYRotation () {
        float angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340) {
            followTransformAngles.x = 340;
        } else if (angle < 180 && angle > 40) {
            followTransformAngles.x = 40;
        }

        followTransform.transform.localEulerAngles = followTransformAngles;
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
    public void DetectClosestPanda (GameObject player) {
        float distanceToClosestPanda = Mathf.Infinity;
        Panda closest = null;

        Panda[] allPandas = GameObject.FindObjectsOfType<Panda> ();

        foreach (Panda currentPanda in allPandas) {
            float distanceToPanda = (currentPanda.transform.position - player.transform.position).sqrMagnitude;
            if (distanceToPanda < distanceToClosestPanda) {
                distanceToClosestPanda = distanceToPanda;
                closest = currentPanda;
            }

        }
        minimapPandaScript.MarkPandaLocation (closest.transform.position);
        //Debug.DrawLine (player.transform.position, closest.transform.position);

    }

    public void CollectPanda (GameObject panda) {
        minimapPandaScript.AddPanda ();
        Destroy (panda);
    }
}