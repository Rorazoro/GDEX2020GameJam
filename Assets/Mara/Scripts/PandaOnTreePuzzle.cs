using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaOnTreePuzzle : MonoBehaviour {
    public GameObject bambooPiece;
    public GameObject pandaOnTree;
    public GameObject pandaOnGround;

    void OnTriggerEnter (Collider col) {
        if (col.name == bambooPiece.name) {
            pandaOnTree.SetActive (true);
        }
    }

    private void Update () {
        if (bambooPiece.transform.position.y > 13) {
            pandaOnGround.SetActive (true);
        }
    }
}