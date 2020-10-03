using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanda : MonoBehaviour {
    public GameObject player;
    public GameObject tutorialCanvas;
    void OnTriggerEnter (Collider col) {
        if (col.name == player.name) {
            tutorialCanvas.SetActive (true);
        }
    }

    void OnTriggerExit (Collider col) {
        if (col.name == player.name) {
            tutorialCanvas.SetActive (false);
        }
    }
}