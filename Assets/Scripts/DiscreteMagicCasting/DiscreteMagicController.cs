using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscreteMagicController : MonoBehaviour
{
    public bool IsCasting = false;
    public bool IsSpellActive = false;
    public Texture2D CursorTexture;
    public Transform[] Nodes;
    public UnityEvent OnStartCast, OnEndCast, OnEndSpell;


    private Camera camera;
    private List<Vector3> linePoints;
    private LineRenderer lineRenderer;
    private Vector2 castPoint;

    void Start()
    {
        Cursor.SetCursor(CursorTexture, new Vector2(16, 16), CursorMode.Auto);
        lineRenderer = GetComponent<LineRenderer>();
        camera = Camera.main;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!IsCasting && !IsSpellActive)
            {
                StartCasting();
            }else if(IsCasting || IsSpellActive)
            {
                //This is how a spell can be cancelled, either during cast or during a spell
                EndSpell();
            }
        }

        if(Input.GetMouseButtonUp(0) && IsCasting)
        {
            EndCast();
            CastSpell();
        }
    }

    private void CastSpell()
    {
        RaycastHit hit;

        if(castPoint==null)
        {
            castPoint = Input.mousePosition;
            Debug.LogWarning("Cast point was not properly set before casting.");
        }
        Ray ray = camera.ScreenPointToRay(castPoint);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            IReceiveCast[] castReceivers = objectHit.GetComponents<IReceiveCast>();

            if (castReceivers.Length >0)
            {
                //We hit something(s) that can receive a spell
                foreach(var castReceiver in castReceivers)
                {
                    castReceiver.CastSpell(this, "HELLOWORLD");
                }
            }
            else
            {
                //Nothing to recieve the cast! end the spell here
                EndSpell();
            }
        }
        else
        {
            //Nothing to recieve the cast! end the spell here
            EndSpell();
        }
    }

    private void StartCasting()
    {
        IsCasting = true;
        IsSpellActive = true;
        OnStartCast?.Invoke();
        Cursor.lockState = CursorLockMode.Confined;
        castPoint = Input.mousePosition;
    }

    private void EndCast()
    {
        if (IsCasting)
        {
            IsCasting = false;
            OnEndCast?.Invoke();
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
