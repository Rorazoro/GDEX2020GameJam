using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagicManager : SingletonBehaviour<MagicManager> {

    public bool IsCasting = false;
    public bool IsSpellActive = false;
    public GameObject NodePanel;
    public GameObject MoveCam;
    public GameObject SpellCam;

    private List<Transform> lineNodes = new List<Transform> ();
    private LineRenderer lineRenderer;
    private ICastable CurrentSpell;
    private string CurrentSpellId;

    void Start () {
        lineRenderer = NodePanel.GetComponent<LineRenderer> ();
        lineRenderer.positionCount = 0;
    }

    void Update () {
        if (InputManager.Instance.EndCastingInput) {
            EndCasting ();
        } else if (InputManager.Instance.DrawInput && IsCasting) {
            UpdateTrace ();
        } else if (!InputManager.Instance.DrawInput && IsCasting) {
            CurrentSpell.CastSpell ();
            EndCasting ();
        }
    }

    private void CastSpell () {
        string SpellId = "";
        foreach (var node in lineNodes) {
            SpellId += node.name;
        }

        bool spellCast = false;

        if (CurrentSpellId.Equals (SpellId)) {
            CurrentSpell.CastSpell ();
            spellCast = true;
        }

        if (!spellCast) {
            //Nothing to recieve the cast! end the spell here
            EndSpell ();
        }
    }

    public void StartCasting (ICastable spellObject) {
        if (!IsCasting) {
            //Cursor.lockState = CursorLockMode.Confined;
            CurrentSpell = spellObject;
            CurrentSpellId = spellObject.GetSpellId ();
            IsCasting = true;
            IsSpellActive = true;
            InputManager.Instance.SwitchInputMap ("SpellCasting");
            ToggleCameraView ();
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
            ToggleCameraView ();
            InputManager.Instance.SwitchInputMap ("Player");
        }
    }

    public void EndSpell () {
        //if for some reason the cast is still going on
        EndCasting ();

        //This check is important so we dont trigger infinite loops
        if (IsSpellActive) {
            IsSpellActive = false;
            CurrentSpell.CastSpell ();
            CurrentSpell = null;
            //Cursor.lockState = CursorLockMode.Locked;
        }

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

    private void ToggleCameraView () {
        MoveCam.SetActive (!MoveCam.activeSelf);
        SpellCam.SetActive (!SpellCam.activeSelf);
    }
}