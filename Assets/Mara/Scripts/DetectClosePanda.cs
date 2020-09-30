using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClosePanda : MonoBehaviour {

    // void Update () {

    //     DetectClosestPanda ();
    // }

    public void DetectClosestPanda (GameObject player) {
        float distanceToClosestPanda = Mathf.Infinity;
        Panda closest = null;

        Panda[] allPandas = GameObject.FindObjectsOfType<Panda> ();

        foreach (Panda currentPanda in allPandas) {
            float distanceToPanda = (currentPanda.transform.position - player.transform.position).sqrMagnitude;
            if (distanceToPanda < distanceToClosestPanda) {
                distanceToClosestPanda = distanceToPanda;
                closest = currentPanda;
            }

        }
        Debug.DrawLine (player.transform.position, closest.transform.position);

    }
}