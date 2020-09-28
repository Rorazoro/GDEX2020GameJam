using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapPandaScript : MonoBehaviour {

    public GameObject pointPrefab;
    public Transform PandaContainer;
    public Transform PandaLocation;

    public int TotalPandaNumber = 0;
    public float circleRadius = 6;

    int CollectedPandaCount = 0;

    public GameObject redX;
    void Start () {
        PandaContainer.Rotate (90, 0, 0);
        instantiateInCircle (pointPrefab, new Vector3 (0, 0, 0), TotalPandaNumber);

    }

    public void instantiateInCircle (GameObject obj, Vector3 location, int howMany) {
        for (int i = 0; i < howMany; i++) {
            float radius = howMany;
            float angle = i * Mathf.PI * 2f / radius;
            Vector3 newPos = transform.position + (new Vector3 (Mathf.Cos (angle) * radius * circleRadius, -2, Mathf.Sin (angle) * radius * circleRadius));
            Instantiate (obj, newPos, Quaternion.Euler (90, 0, 0)).transform.SetParent (PandaContainer);

        }
    }

    public void AddPanda () {
        CollectedPandaCount++;
        if (CollectedPandaCount <= TotalPandaNumber)
            PandaProgress (CollectedPandaCount);
    }

    public void PandaProgress (int count) {
        int CollectedPandaCount = count;

        for (int i = 0; i < PandaContainer.childCount; i++) {
            if (i < count) {
                GameObject child = PandaContainer.gameObject.transform.GetChild (i).gameObject;
                child.SetActive (true);
            }
        }
    }

    public void MarkPandaLocation (Vector3 pandaLocation) {
        GameObject redXClone = Instantiate (redX, pandaLocation, Quaternion.Euler (0, 0, 0)).gameObject;
        redXClone.transform.SetParent (PandaLocation);
        redXClone.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

        redXClone.transform.position = new Vector3 (pandaLocation.x, 70f, pandaLocation.z);
        redXClone.transform.Rotate (90, 0, 0);
        Invoke ("ResetLocations", 3);
    }

    private void ResetLocations () {
        for (int i = 0; i < PandaLocation.childCount; i++) {
            GameObject child = PandaLocation.gameObject.transform.GetChild (i).gameObject;
            Destroy (child);
        }
    }
}