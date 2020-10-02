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
    public Camera UICamera;
    public float MinTraceDistance = 0.1f;

    private List<Transform> magicNodes = new List<Transform> ();
    private List<Vector3> tracePoints = new List<Vector3>();
    private LineRenderer lineRenderer;
    private ICastable CurrentSpell;
    [SerializeField]
    private string CurrentSpellId;

    private bool startedDrawing = false;

    void Start () {
        lineRenderer = NodePanel.GetComponent<LineRenderer> ();
        lineRenderer.positionCount = 0;
    }

    void Update () {
        if (InputManager.Instance.EndCastingInput) {
            EndCasting ();
            EndSpell ();
        } else if (InputManager.Instance.DrawInput && IsCasting) {
            startedDrawing = true;
            UpdateTrace ();
        } else if (!InputManager.Instance.DrawInput && IsCasting) {
            CheckForActiveSpell ();
            if (startedDrawing)
            {
                EndCasting();
            }
            if (IsSpellActive) {
                EndCasting ();
                CastSpell ();
            }
        }
    }

    private void CheckForActiveSpell () {
        string SpellId = "";
        foreach (var node in magicNodes) {
            SpellId += node.name;
        }

        var spellIds = new List<string>(CurrentSpellId.Split(','));
        if (spellIds.Contains(SpellId)) {
            IsSpellActive = true;
        }
    }

    private void CastSpell () {
        CurrentSpell.CastSpell ();
    }

    public void StartCasting (ICastable spellObject) {
        if (!IsCasting) {
            startedDrawing = false;
            InputManager.Instance.SwitchCursorLockState(CursorLockMode.Confined);
            CurrentSpell = spellObject;
            CurrentSpellId = spellObject.GetSpellId ();
            IsCasting = true;
            InputManager.Instance.SwitchInputMap ("SpellCasting");
            CameraManager.Instance.ToggleCamera (1);
            NodePanel.SetActive (true);
            magicNodes.Clear ();
            tracePoints.Clear();
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
        if (IsSpellActive)
        {
            IsSpellActive = false;
            CurrentSpell.EndSpell();
            CurrentSpell = null;
            CurrentSpellId = string.Empty;
            CameraManager.Instance.ToggleCamera(0);
            InputManager.Instance.SwitchInputMap("Player");
            InputManager.Instance.SwitchCursorLockState(CursorLockMode.Locked);
        }
    }

    private void UpdateTrace () {
        Vector2 mousePosition = InputManager.Instance.PointInput;

        //check if we need to add a new node to out line definition
        //var eventData = new PointerEventData (EventSystem.current);
        //eventData.position = mousePosition;
        //var results = new List<RaycastResult> ();
        //EventSystem.current.RaycastAll (eventData, results);
        //if (results.Count > 0 &&
        //    results[0].gameObject.CompareTag ("MagicNode") &&
        //    (lineNodes.Count == 0 || results[0].gameObject.transform != lineNodes[lineNodes.Count - 1])) {
        //    lineNodes.Add (results[0].gameObject.transform);
        //}

        Ray ray = UICamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("MagicNode"))
            {
                if (magicNodes.Count == 0 || !magicNodes.Contains(hit.collider.transform))
                {
                    magicNodes.Add(hit.collider.transform);
                    tracePoints.Add(hit.collider.transform.position + hit.transform.forward * .2f);
                }
            }
            else if (tracePoints.Count == 0 || Vector3.Distance(hit.point, tracePoints[tracePoints.Count - 1]) > MinTraceDistance)
            {
                //Order of if statement logic here is important so we dont try to access a point if tracepoints<1
                tracePoints.Add(hit.point+hit.transform.forward*.2f);
            }
        }
            

        if (Physics.Raycast(ray, out hit) &&
            hit.collider.gameObject.CompareTag("MagicNode") &&
            (magicNodes.Count == 0 || !magicNodes.Contains(hit.collider.transform)))
        {
            magicNodes.Add(hit.collider.transform);
        }




        //for (int i = 0; i< magicNodes.Count; i++)
        //{
        //    var node = line
        //}

        //generate line to render
        List<Vector3> linePoints = new List<Vector3> ();

        foreach (var node in tracePoints) {
            linePoints.Add (node);
        }

        Vector3 screenPoint = mousePosition;
        screenPoint.z = 4.0f;
        //transform.position = Camera.main.ScreenToWorldPoint (screenPoint);
        //linePoints.Add (UICamera.ScreenToWorldPoint (screenPoint));

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions (linePoints.ToArray ());
    }
}