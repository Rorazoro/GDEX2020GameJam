using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class SleepSpell : MonoBehaviour, ICastSpell
{
    private string mySpellPattern = "sleep";

    private bool isSpellActive = false;
    public void CastSpell(DiscreteMagicController magicController) //Change so it uses IReceiveCast
    {
        if (magicController.SpellId.Equals(mySpellPattern))
        {
            isSpellActive = true;
            StartCoroutine(Sleeping());
        }
    }

    IEnumerator Sleeping()
    {
        gameObject.GetComponent<EvilPanda>().Sleep();
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<EvilPanda>().WakeUp();
    }
    
}*/
