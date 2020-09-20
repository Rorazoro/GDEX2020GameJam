using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForceSpell : MonoBehaviour
{
    string mySpellPattern = "force";
    private DiscreteMagicController myMagicController;
    private Collider collider;
    private Rigidbody rb;
    private float minForce = -3f;
    private float maxForce = 3f;
    private float yMinForce = 0.5f;
    private float heightBonus = 5f; //Push it up in the air a little bit more
    private void Awake()
    {
        collider = gameObject.GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
        MoveObject();
    }

    public void CastSpell(DiscreteMagicController magicController)
    {
        if (magicController.SpellId.Equals(mySpellPattern))
        {
            rb.useGravity = false;
            MoveObject(); //Launch object in random direction
        }
    }

    void MoveObject()
    {
        rb.velocity = RandomVector();
    }
    
    private Vector3 RandomVector() {
        var x = Random.Range(minForce, maxForce);
        var y = Random.Range(yMinForce, maxForce);
        var z = Random.Range(minForce, maxForce);
        return new Vector3(x, y + heightBonus, z);
    }

}
