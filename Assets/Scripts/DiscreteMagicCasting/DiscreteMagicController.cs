using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DiscreteMagicController : MonoBehaviour
{
    public bool IsCasting = false;
    public bool IsSpellActive = false;
    public string SpellId = "";
    public Texture2D CursorTexture;
    public UnityEvent OnStartCast, OnEndCast, OnEndSpell;
    public ICastSpell[] SpellCasters { get; private set; } = new ICastSpell[0];



    private List<Transform> lineNodes = new List<Transform>();
    private LineRenderer lineRenderer;
    private Vector2 castPoint;



    void Start()
    {
        Cursor.SetCursor(CursorTexture, new Vector2(16, 16), CursorMode.Auto);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if(!IsCasting && !IsSpellActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SpellCasters = hit.transform.GetComponents<ICastSpell>();
            }
            else
            {
                SpellCasters = new ICastSpell[0];
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (!IsCasting && !IsSpellActive && SpellCasters.Length > 0)
            {
                StartCasting();
            }
            else if(IsCasting)
            {
                CastSpell();
                EndCast();
            }else if (IsSpellActive)
            {
                EndSpell();
            }
        }

        if(IsCasting && Input.GetMouseButton(0))
        {
            UpdateTrace();
        }
    }

    private void UpdateTrace()
    {
        //check if we need to add a new node to out line definition
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        if (results.Count > 0 &&
            results[0].gameObject.CompareTag("MagicNode") &&
           (lineNodes.Count == 0 || results[0].gameObject.transform != lineNodes[lineNodes.Count - 1]))
        {
            Debug.Log("Add Node: " + results[0].gameObject.name);
            lineNodes.Add(results[0].gameObject.transform);
        }

        //generate line to render
        List<Vector3> linePoints = new List<Vector3>();

        foreach (var node in lineNodes)
        {
            linePoints.Add(node.position);
        }

        var screenPoint = Input.mousePosition;
        screenPoint.z = 4.0f; 
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        linePoints.Add(Camera.main.ScreenToWorldPoint(screenPoint));

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }

    private void CastSpell()
    {
        SpellId = "";
        foreach(var node in lineNodes)
        {
            SpellId += node.name;
        }

        bool spellCast = false;
        if (SpellCasters.Length >0)
        {
            foreach(var spellCaster in SpellCasters)
            {
                if (spellCaster.GetSpellId().Equals(SpellId))
                {
                    spellCaster.CastSpell(this);
                    spellCast = true;
                    if (spellCaster.DoLockMouse())
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                    //we can only cast one spell per pattern.
                    break;
                }
            }
        }

        if (!spellCast)
        {
            //Nothing to recieve the cast! end the spell here
            EndSpell();
        }
    }

    private void StartCasting()
    {
        if (!IsCasting)
        {
            Cursor.lockState = CursorLockMode.Confined;
            IsCasting = true;
            IsSpellActive = true;
            SpellId = "";
            OnStartCast?.Invoke();
            castPoint = Input.mousePosition;
            lineNodes.Clear();
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = true;
        }
    }

    private void EndCast()
    {
        if (IsCasting)
        {
            IsCasting = false;
            OnEndCast?.Invoke();
            lineRenderer.enabled = false;
        }
    }

    public void EndSpell()
    {
        //if for some reason the cast is still going on
        EndCast();

        //This check is important so we dont trigger infinite loops
        if (IsSpellActive)
        {
            OnEndSpell?.Invoke();
            IsSpellActive = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
