using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClosePanda : MonoBehaviour {

    void Update () {
        
        DetectClosestPanda ();
    }

    void DetectClosestPanda () {
        float distanceToClosestPanda = Mathf.Infinity;
        Panda closest = null;

        Panda[] allPandas = GameObject.FindObjectsOfType<Panda> ();

        foreach (Panda currentPanda in allPandas) {
            float distanceToPanda = (currentPanda.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToPanda < distanceToClosestPanda) {
                distanceToClosestPanda = distanceToPanda;
                closest = currentPanda;
            }

        }
        Debug.DrawLine (this.transform.position, closest.transform.position);

    }
}