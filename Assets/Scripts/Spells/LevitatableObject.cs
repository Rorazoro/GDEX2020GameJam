using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LevitatableObject : MonoBehaviour, ICastSpell
{
    static string SpellId = "159AB";

    public bool IsLevitating;
    
    private Rigidbody rb;
    private DiscreteMagicController magicController;


    public void CastSpell(DiscreteMagicController magicController)
    {
        this.magicController = magicController;
        if (!magicController.SpellId.Equals(SpellId))
        {
            magicController.EndSpell();
        }

        IsLevitating = !IsLevitating;

        if (IsLevitating)
        {
            //disable gravity and freeze the object
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            //End the spell right away if they are de-activating the 
            rb.useGravity = true;
            magicController.EndSpell();
        }

 
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
        }
    }
}
