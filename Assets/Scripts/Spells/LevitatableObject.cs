using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LevitatableObject : MonoBehaviour, ICastSpell
{
    static string SpellId = "159AB";

    public bool IsLevitating;
    public float MinY, MaxY;

    private Rigidbody rb;
    private DiscreteMagicController magicController;
    private float distanceToObject;
    


    public void CastSpell(DiscreteMagicController magicController)
    {
        if (!magicController.SpellId.Equals(SpellId))
        {
            magicController.EndSpell();
            return;
        }

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

    private void StartLevitating()
    {
        //disable gravity and freeze the object
        distanceToObject = Vector3.Distance(magicController.transform.position, transform.position);
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

        //I dont usually use null like this for program flow but I'm just making an example spell
        this.magicController.OnEndSpell.RemoveListener(OnEndSpell);
        magicController = null;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(magicController != null && magicController.IsSpellActive && IsLevitating && magicController.SpellId.Equals(SpellId))
        {
            // update y position of levitation
            var screenPoint = Input.mousePosition;
            screenPoint.z = distanceToObject;
            float newY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(screenPoint).y, MinY, MaxY);
            transform.position = new Vector3(transform.position.x,newY, transform.position.z);
        }
    }
}
