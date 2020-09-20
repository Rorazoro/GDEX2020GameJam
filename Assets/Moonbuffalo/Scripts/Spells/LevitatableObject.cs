using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LevitatableObject : MonoBehaviour, ICastSpell
{
    static string SpellId = "159AB";

    public bool IsLevitating;
    public float MinY, MaxY;
    public float LevitateRateMetersPerSecond;

    private Rigidbody rb;
    private DiscreteMagicController magicController;

    public void CastSpell(DiscreteMagicController magicController)
    {
        this.magicController = magicController;
        this.magicController.OnEndSpell.AddListener(OnEndSpell);

        if (IsLevitating)
        {
            StopLevitating();
        }
        else
        {
            StartLevitating();
        }
    }

    public string GetSpellId()
    {
        return SpellId;
    }

    private void StartLevitating()
    {
        //disable gravity and freeze the object
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        IsLevitating = true;
    }

    private void StopLevitating()
    {
        //End the spell right away if they are de-activating the levitation
        IsLevitating = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        magicController?.EndSpell();
    }

    void OnEndSpell()
    {
        this.magicController.OnEndSpell.RemoveListener(OnEndSpell);
        magicController = null;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(magicController != null && magicController.IsSpellActive && IsLevitating)
        {
            // update y position of levitation
            //Vector3 posXZ = new Vector3(transform.position.x, 0, transform.position.z);
            //Vector3 otherPosXZ = new Vector3(magicController.transform.position.x, 0, magicController.transform.position.z);
            //float distanceXZ = Vector3.Distance(posXZ, otherPosXZ);

            //var screenPoint = Input.mousePosition;
            //screenPoint.z = distanceToObject;
            //float newY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(screenPoint).y, MinY, MaxY);
            //transform.position = new Vector3(transform.position.x, newY, transform.position.z);


            float maxDeltaMovement = Time.deltaTime * LevitateRateMetersPerSecond * Input.GetAxis("Mouse Y");

            float newY = Mathf.Clamp(transform.position.y+ maxDeltaMovement, MinY, MaxY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        }
    }

    public bool DoLockMouse()
    {
        return true;
    }
}
