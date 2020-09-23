using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonBehaviour<InputManager> {
    private PlayerInput playerInput;
    [SerializeField]
    private Vector2 _moveInput = new Vector2 ();
    [SerializeField]
    private Vector2 _lookInput = new Vector2 ();
    [SerializeField]
    private Vector2 _pointInput = new Vector2 ();
    [SerializeField]
    private bool _interactInput;
    [SerializeField]
    private bool _drawInput;
    [SerializeField]
    private bool _castInput;
    [SerializeField]
    private bool _endCastingInput;

    public Vector2 LookInput { get => _lookInput; }
    public Vector2 MoveInput { get => _moveInput; }
    public Vector2 PointInput { get => _pointInput; }
    public bool InteractInput { get => _interactInput; }
    public bool DrawInput { get => _drawInput; }
    public bool CastInput { get => _castInput; }
    public bool EndCastingInput { get => _endCastingInput; }

    private void Start () {
        playerInput = GetComponent<PlayerInput> ();
    }

    public void SwitchInputMap (string actionMapName) {
        playerInput.SwitchCurrentActionMap (actionMapName);
    }

    //Player ActionMap
    public void OnMove (InputAction.CallbackContext context) {
        _moveInput = context.ReadValue<Vector2> ();
    }

    public void OnLook (InputAction.CallbackContext context) {
        _lookInput = context.ReadValue<Vector2> ();
    }

    public void OnToggleConsole (InputAction.CallbackContext context) {
        DebugManager.Instance.ToggleConsole ();
    }

    public void OnReturn (InputAction.CallbackContext context) {
        DebugManager.Instance.ReturnInput ();
    }

    public void OnInteract (InputAction.CallbackContext context) {
        if (context.performed) {
            _interactInput = true;
        } else if (context.canceled) {
            _interactInput = false;
        }
    }
    public void OnCast (InputAction.CallbackContext context) {
        if (context.performed) {
            _castInput = true;
        } else if (context.canceled) {
            _castInput = false;
        }
    }

    //SpellCasting ActionMap
    public void OnPoint (InputAction.CallbackContext context) {
        _pointInput = context.ReadValue<Vector2> ();
    }

    public void OnDraw (InputAction.CallbackContext context) {
        if (context.performed) {
            _drawInput = true;
        } else if (context.canceled) {
            _drawInput = false;
        }
    }

    public void OnEndCasting (InputAction.CallbackContext context) {
        if (context.performed) {
            _endCastingInput = true;
        } else if (context.canceled) {
            _endCastingInput = false;
        }
    }
}