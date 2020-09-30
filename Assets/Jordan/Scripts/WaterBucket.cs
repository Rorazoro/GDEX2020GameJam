using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] private GameObject innerWater;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            gameObject.tag = "FullBucket";
            innerWater.SetActive(true);
        }

        if (other.gameObject.tag == "Fire")
        {
            gameObject.tag = "Untagged";
            innerWater.SetActive(false);
        }
    }
}
