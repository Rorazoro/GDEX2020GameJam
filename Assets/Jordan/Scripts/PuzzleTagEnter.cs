using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTagEnter : MonoBehaviour
{
    [Header("Trigger Handlers")]
    [SerializeField] private bool eyeFirePit;
    [SerializeField] private bool waterBucketDouse;
    [SerializeField] private bool rotisseriePit;

    [Header("Trigger Effectors")] 
    [SerializeField] private GameObject eyePitFire;
    [SerializeField] private GameObject hutFire;
    [SerializeField] private GameObject pitFire;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Torch")
        {
            if (eyeFirePit)
            {
                //SetPandaTrue
                eyePitFire.SetActive(true);
            }

            if (rotisseriePit)
            {
                //SetPandaTrue
                pitFire.SetActive(true);
            }
        }

        if (other.gameObject.tag == "FullBucket")
        {
            if (waterBucketDouse)
            {
                //SetPandaTrue
                hutFire.SetActive(false);
            }
        }
    }
}
