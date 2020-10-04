﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWaterfallBlock : MonoBehaviour
{
    [SerializeField] private MoveWater[] lakeWater;
    [SerializeField] private GameObject PandaToEnable;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Rock")
        {
            foreach (var lake in lakeWater)
            {
                lake.WaterMovement();
            }
            PandaToEnable.SetActive(true);
        }
    }
}
