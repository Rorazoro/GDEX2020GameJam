using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour {
    void OnTriggerEnter (Collider col) {
        PlayerMovement player = col.GetComponent<PlayerMovement> ();

        ThirdPersonController player2 = col.GetComponent<ThirdPersonController> ();

        if (player != null || player2 != null) {
            Debug.Log ("Panda Collected");
           // player.CollectPanda (this.gameObject);
            player2.CollectPanda (this.gameObject);
        }
    }
}