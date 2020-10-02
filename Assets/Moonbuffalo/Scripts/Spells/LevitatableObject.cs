using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class LevitatableObject : MonoBehaviour, ICastable
{
    static string SpellId = "157,751";

    public float InteractDist;
    public float YPosMin, YPosMax;
    public float LevitateRate;
    public Transform ViewCamera;

    private Rigidbody rb;
    private Outline outline;
    private bool isLevitating = false;
    private bool isSpellActive = false;
    private bool isHovering = false;
    private RigidbodyConstraints initConstraints;

    public float MaxRange => InteractDist;

    public string GetSpellId()
    {
        return SpellId;
    }

    private void StartLevitating()
    {
        //disable gravity and freeze the object
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        isLevitating = true;
    }

    private void StopLevitating()
    {
        //End the spell right away if they are de-activating the levitation
        isLevitating = false;
        rb.useGravity = true;
        rb.constraints = initConstraints;
        MagicManager.Instance.EndSpell();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        initConstraints = rb.constraints;
    }

    void Update()
    {        outline.enabled = isHovering || isLevitating;
        if(isSpellActive && isLevitating)
        {
            float maxDeltaMovement = Time.deltaTime * LevitateRate * InputManager.Instance.MouseDelta.y;

            float newY = Mathf.Clamp(transform.localPosition.y+ maxDeltaMovement, YPosMin, YPosMax);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }

        

    }


    public void CastSpell()
    {

        CameraManager.Instance.ToggleCamera(2);

        if(ViewCamera!=null)
        {
            CameraManager.Instance.Cameras[2].transform.position = ViewCamera.position;
            CameraManager.Instance.Cameras[2].transform.rotation = ViewCamera.rotation;
            CameraManager.Instance.SetCameraLookAt(null);
            CameraManager.Instance.SetCameraFollow(null);
        }
        else
        {
            CameraManager.Instance.SetCameraLookAt(transform);
        }

        InputManager.Instance.SwitchInputMap("SpellCasting");

        

        isSpellActive = true;
        if (isLevitating)
        {
            StopLevitating();
        }
        else
        {
            StartLevitating();
        }
    }

    public void EndSpell()
    {
        isSpellActive = false;
        //if it's kinematic, we want to turn off levitating. 
        //This makes kinematic add the option for puzzles that just need to set the position of something 
        //but dont want it to alternate between non-kinematic and kinematic
        if (rb.isKinematic)
        {
            isLevitating = false;
        }
    }

    public void OnStartHover()
    {
        isHovering = true;
    }

    public void OnInteract()
    {
        MagicManager.Instance.StartCasting(this);

    }

    public void OnEndHover()
    {
        isHovering = false;
    }
}
