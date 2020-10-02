using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public FlowerManager flowerManager;

    public GameObject player;
    void OnTriggerExit (Collider col) {
        if (col.name == player.name) {
            flowerManager.CollectFlower (this.gameObject);
        }
    }
}