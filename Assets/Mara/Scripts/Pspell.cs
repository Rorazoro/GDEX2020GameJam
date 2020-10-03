using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pspell : MonoBehaviour, ICastable {
    public GameObject panda;

    static string SpellId = "51234,43215";

    public float InteractDist;

    private bool isSpellActive = false;

    public float MaxRange => InteractDist;

    public void CastSpell () //Change so it uses IReceiveCast
    {
        isSpellActive = true;
        StartCoroutine (PandaFinding ());
    }

    IEnumerator PandaFinding () {
        panda.SetActive (true);
        yield return new WaitForSeconds (5f);
    }
    public string GetSpellId () {
        return SpellId;
    }
    public void OnInteract () {
        MagicManager.Instance.StartCasting (this);

    }
    public void EndSpell () {
        throw new System.NotImplementedException ();
    }
    public void OnEndHover () {
        //throw new System.NotImplementedException ();
    }
    public void OnStartHover () {
     //   throw new System.NotImplementedException ();
    }
}