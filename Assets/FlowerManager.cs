using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour {
    public GameObject[] FlowerContainer;

    public GameObject panda;
    private int FlowerCounter = 1;
    private GameObject prevFlower;
    private GameObject nextFlower;

    private int GotFlower = 0;

    private int prevIndex = 0;

    public void CollectFlower (GameObject flower) {

        if (FlowerCounter == FlowerContainer.Length) {
            nextFlower.SetActive (false);
            Debug.Log ("found panda");
            panda.SetActive (true);
        } else {

            prevFlower = flower;
            prevFlower.SetActive (false);
            nextFlower = FlowerContainer[FlowerCounter].gameObject;
            FlowerCounter++;
            nextFlower.SetActive (true);
            GotFlower = 0;
        }
    
    }

}