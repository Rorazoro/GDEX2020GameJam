using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] private GameObject innerWater;
    public bool isFull = false;
    private void OnTriggerEnter(Collider other)
    {
        print("I hit " + other.gameObject.tag);
        if (other.gameObject.tag == "Water")
        {
            print("got water");
            other.gameObject.tag = "FullBucket";
            innerWater.SetActive(true);
            isFull = true;
        }

        if (other.gameObject.tag == "Fire")
        {
            gameObject.tag = "Untagged";
            innerWater.SetActive(false);
            isFull = false;
        }
    }
}
