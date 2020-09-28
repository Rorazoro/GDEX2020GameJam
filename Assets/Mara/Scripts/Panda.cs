using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour {
    void OnTriggerEnter (Collider col) {
        PlayerMovement player = col.GetComponent<PlayerMovement> ();

        if (player != null) {
            Debug.Log ("Panda Collected");
            player.CollectPanda (this.gameObject);
        }
    }
}