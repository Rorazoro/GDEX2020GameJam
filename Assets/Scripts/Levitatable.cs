using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Levitatable : MonoBehaviour, ICastable {
    static string SpellId = "159AB";

    public bool IsLevitating;
    public float MinY, MaxY;
    public float LevitateRateMetersPerSecond;

    private Rigidbody rb;

    public float MaxRange => 5;

    public void CastSpell () {
        if (IsLevitating) {
            StopLevitating ();
        } else {
            StartLevitating ();
        }
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

    void EndSpell () {
        //if it's kinematic, we want to turn off levitating. 
        //This makes kinematic add the option for puzzles that just need to set the position of something 
        //but dont want it to alternate between gravity enabled physics and kinematic
        if (rb.isKinematic) {
            IsLevitating = false;
        }
    }

    void Start () {
        rb = GetComponent<Rigidbody> ();
    }

    void Update () {
        if (IsLevitating) {
            // update y position of levitation
            //Vector3 posXZ = new Vector3(transform.position.x, 0, transform.position.z);
            //Vector3 otherPosXZ = new Vector3(magicController.transform.position.x, 0, magicController.transform.position.z);
            //float distanceXZ = Vector3.Distance(posXZ, otherPosXZ);

            //var screenPoint = Input.mousePosition;
            //screenPoint.z = distanceToObject;
            //float newY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(screenPoint).y, MinY, MaxY);
            //transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            float maxDeltaMovement = Time.deltaTime * LevitateRateMetersPerSecond * InputManager.Instance.LookInput.y;

            float newY = Mathf.Clamp (transform.position.y + maxDeltaMovement, MinY, MaxY);
            transform.position = new Vector3 (transform.position.x, newY, transform.position.z);

        }
    }

    // public bool DoLockMouse () {
    //     return true;
    // }

    public void OnStartHover () {
        Debug.Log ("Interaction OnStartHover!");
    }

    public void OnInteract () {
        MagicManager.Instance.StartCasting (this);
    }

    public void OnEndHover () {
        Debug.Log ("Interaction OnEndHover!");
    }
}