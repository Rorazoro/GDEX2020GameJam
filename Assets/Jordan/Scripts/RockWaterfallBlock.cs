using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWaterfallBlock : MonoBehaviour
{
    [SerializeField] private MoveWater[] lakeWater;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            foreach (var lake in lakeWater)
            {
                lake.WaterMovement();
            }
            //ToDo: Trigger Panda
        }
    }
}
