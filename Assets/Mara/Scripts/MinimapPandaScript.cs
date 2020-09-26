using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapPandaScript : MonoBehaviour {

    public GameObject pointPrefab;
    public Transform Parent;
    public int PandaNumber = 0;
    public float circleRadius = 6;

    void Start () {
        instantiateInCircle (pointPrefab, new Vector3 (0, 0, 0), PandaNumber);
        Parent.Rotate (90, 0, 0);
    }

    public void instantiateInCircle (GameObject obj, Vector3 location, int howMany) {
        for (int i = 0; i < howMany; i++) {
            float radius = howMany;
            float angle = i * Mathf.PI * 2f / radius;
            Vector3 newPos = transform.position + (new Vector3 (Mathf.Cos (angle) * radius * circleRadius, -2, Mathf.Sin (angle) * radius * circleRadius));
            Instantiate (obj, newPos, Quaternion.Euler (90, 0, 0)).transform.SetParent (Parent);

        }
    }

    public void PandaProgress (int count) {
        for (int i = 0; i < Parent.childCount; i++) {
            if (i < count) {
                GameObject child = Parent.gameObject.transform.GetChild (i).gameObject;
                child.SetActive (true);
            }
        }
    }
}