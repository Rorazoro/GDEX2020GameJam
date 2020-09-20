using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetismSpell : MonoBehaviour, ICastSpell
{
    [SerializeField] string mySpellPattern;

    private bool isControlling = false;

    public void CastSpell(DiscreteMagicController magicController)
    {
        if (magicController.SpellId.Equals(mySpellPattern))
        {
            isControlling = true;
        }
        
        
    }

    void Update()
    {
        if (isControlling)
        {
            
        }
    }
}
