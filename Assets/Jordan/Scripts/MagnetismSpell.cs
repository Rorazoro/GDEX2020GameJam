using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetismSpell : MonoBehaviour, ICastSpell
{
    string mySpellPattern = "magnetize";
    private DiscreteMagicController myMagicController;
    private bool isControlling = false;
    private Collider collider;
    private Rigidbody rb;
    private void Awake()
    {
        collider = gameObject.GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void CastSpell(DiscreteMagicController magicController)
    {
        if (magicController.SpellId.Equals(mySpellPattern))
        {
            isControlling = true;
            collider.enabled = false;
            rb.useGravity = false;
        }

        myMagicController = magicController;
        myMagicController.OnEndSpell.AddListener(OnEndSpell);
    }

    void Update()
    {
        if (isControlling)
        {
            MoveObject(); //Moves along x & z axis
        }
    }

    void OnEndSpell() //Activates when mouse is clicked during spell
    {
        isControlling = false;
        collider.enabled = false;
        rb.useGravity = true;
        myMagicController.OnEndSpell.RemoveListener(OnEndSpell);
    }

    void MoveObject()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        gameObject.transform.Translate(h, 0, v);
    }
}
