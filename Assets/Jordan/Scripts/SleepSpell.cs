using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSpell : MonoBehaviour, ICastable
{
    private static string SpellId = "1596B";
    public float InteractDist;
    public float MaxRange => InteractDist;
    private bool isSpellActive = false;
    public void CastSpell() //Change so it uses IReceiveCast
    {
        isSpellActive = true;
            StartCoroutine(Sleeping());
    }

    IEnumerator Sleeping()
    {
        gameObject.GetComponent<EvilPanda>().Sleep();
        yield return new WaitForSeconds(5f);
        //gameObject.GetComponent<EvilPanda>().WakeUp();
    }
    public string GetSpellId()
    {
        return SpellId;
    }
    public void OnInteract()
    {
        MagicManager.Instance.StartCasting(this);

    }
    public void EndSpell()
    {
        throw new System.NotImplementedException();
    }
    public void OnEndHover()
    {
        throw new System.NotImplementedException();
    }
    public void OnStartHover()
    {
        throw new System.NotImplementedException();
    }
}
