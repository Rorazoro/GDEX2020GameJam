using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class MagnetismSpell : MonoBehaviour, ICastable
{
    static string SpellId = "267A5";
    private bool isControlling = false;
    private Rigidbody rb;
    public float InteractDist;
    public Transform ViewCamera;

    private Outline outline;
    private bool isSpellActive = false;
    private RigidbodyConstraints initConstraints;
    //private bool isHovering = false;
    public float MaxRange => InteractDist;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    public string GetSpellId()
    {
        return SpellId;
    }

    public bool DoLockMouse()
    {
        return false;
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
        if (isControlling)
        {
            MagicManager.Instance.EndSpell();
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            isControlling = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        if (isControlling)
        {
            MoveObject(); //Moves along x & z axis
        }
    }

    public void EndSpell() //Activates when mouse is clicked during spell
    {
        isControlling = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void MoveObject()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        gameObject.transform.Translate(h, 0, v);
    }
    
    public void OnInteract()
    {
        MagicManager.Instance.StartCasting(this);

    }
    public void OnStartHover()
    {
        
    }

    public void OnEndHover()
    {
        
    }
}
