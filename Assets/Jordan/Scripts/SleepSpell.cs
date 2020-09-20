using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSpell : MonoBehaviour//, IReceiveCast
{
    [SerializeField] private string mySpellPattern;

    private bool isSpellActive = false;
    void CastSpell(string pattern) //Change so it uses IReceiveCast
    {
        if (mySpellPattern == pattern)
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
    
}
