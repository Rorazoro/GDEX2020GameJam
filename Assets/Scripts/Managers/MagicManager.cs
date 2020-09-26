using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagicManager : SingletonBehaviour<MagicManager> {

    public bool IsCasting = false;
    public bool IsSpellActive = false;
    public GameObject NodePanel;
    public GameObject MoveCam;
    public GameObject CastingCam;
    public GameObject SpellCam;

    private List<Transform> lineNodes = new List<Transform> ();
    private LineRenderer lineRenderer;
    private ICastable CurrentSpell;
    [SerializeField]
    private string CurrentSpellId;

    void Start () {
        lineRenderer = NodePanel.GetComponent<LineRenderer> ();
        lineRenderer.positionCount = 0;
    }

    void Update () {
        if (InputManager.Instance.EndCastingInput) {
            EndCasting ();
            EndSpell ();
        } else if (InputManager.Instance.DrawInput && IsCasting) {
            UpdateTrace ();
        } else if (!InputManager.Instance.DrawInput && IsCasting) {
            CheckForActiveSpell ();
            if (IsSpellActive) {
                EndCasting ();
                CastSpell ();
            }
        }
    }

    private void CheckForActiveSpell () {
        string SpellId = "";
        foreach (var node in lineNodes) {
            SpellId += node.name;
        }
        if (CurrentSpellId.Equals (SpellId)) {
            IsSpellActive = true;
        }
    }

    private void CastSpell () {
        CurrentSpell.CastSpell ();
    }

    public void StartCasting (ICastable spellObject) {
        if (!IsCasting) {
            Cursor.lockState = CursorLockMode.Confined;
            CurrentSpell = spellObject;
            CurrentSpellId = spellObject.GetSpellId ();
            IsCasting = true;
            InputManager.Instance.SwitchInputMap ("SpellCasting");
            CameraManager.Instance.ToggleCamera (1);
            NodePanel.SetActive (true);
            lineNodes.Clear ();
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = true;
        }
    }

    private void EndCasting () {
        if (IsCasting) {
            IsCasting = false;
            lineRenderer.enabled = false;
            NodePanel.SetActive (false);
            CameraManager.Instance.ToggleCamera (0);
            InputManager.Instance.SwitchInputMap ("Player");
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EndSpell () {
        //This check is important so we dont trigger infinite loops
        IsSpellActive = false;
        CurrentSpell.EndSpell ();
        CurrentSpell = null;
        CurrentSpellId = string.Empty;
        CameraManager.Instance.ToggleCamera (0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateTrace () {
        Vector2 mousePosition = InputManager.Instance.PointInput;

        //check if we need to add a new node to out line definition
        var eventData = new PointerEventData (EventSystem.current);
        eventData.position = mousePosition;
        var results = new List<RaycastResult> ();
        EventSystem.current.RaycastAll (eventData, results);
        if (results.Count > 0 &&
            results[0].gameObject.CompareTag ("MagicNode") &&
            (lineNodes.Count == 0 || results[0].gameObject.transform != lineNodes[lineNodes.Count - 1])) {
            lineNodes.Add (results[0].gameObject.transform);
        }

        //generate line to render
        List<Vector3> linePoints = new List<Vector3> ();

        foreach (var node in lineNodes) {
            linePoints.Add (node.position);
        }

        Vector3 screenPoint = mousePosition;
        screenPoint.z = 4.0f;
        transform.position = Camera.main.ScreenToWorldPoint (screenPoint);
        linePoints.Add (Camera.main.ScreenToWorldPoint (screenPoint));

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions (linePoints.ToArray ());
    }
}