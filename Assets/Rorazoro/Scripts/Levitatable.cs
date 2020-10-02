using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Levitatable : MonoBehaviour, ICastable {
    public string SpellId = "159AB";
    public CinemachineVirtualCamera cam;

    public bool IsLevitating;
    public float MinY, MaxY;
    public float LevitateRateMetersPerSecond;

    private Rigidbody rb;
    private Outline outline;

    public float MaxRange => 5;

    public void CastSpell () {
        StartLevitating ();
        CameraManager.Instance.ToggleCamera (2);
        CameraManager.Instance.SetCameraLookAt (transform);
    }

    public string GetSpellId () {
        return SpellId;
    }

    private void StartLevitating () {
        //disable gravity and freeze the object
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        IsLevitating = true;
    }

    private void StopLevitating () {
        //End the spell right away if they are de-activating the levitation
        IsLevitating = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void EndSpell () {
        StopLevitating ();
        CameraManager.Instance.SetCameraLookAt (null);
        CameraManager.Instance.ToggleCamera (0);
    }

    void Start () {
        rb = GetComponent<Rigidbody> ();
        outline = GetComponent<Outline> ();
    }

    void Update () {
        if (IsLevitating) {
            float maxDeltaMovement = Time.deltaTime * LevitateRateMetersPerSecond * InputManager.Instance.LookInput.y;

            float newY = Mathf.Clamp (transform.position.y + maxDeltaMovement, MinY, MaxY);
            transform.position = new Vector3 (transform.position.x, newY, transform.position.z);

        }
    }

    public void OnStartHover () {
        outline.enabled = true;
    }

    public void OnInteract () {
        MagicManager.Instance.StartCasting (this);
    }

    public void OnEndHover () {
        outline.enabled = false;
    }
}