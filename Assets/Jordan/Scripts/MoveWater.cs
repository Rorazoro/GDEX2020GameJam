using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWater : MonoBehaviour
{
    [Tooltip("Create a cube destination for where you want the water to go")]
    [SerializeField] private Transform destination;
    [SerializeField] private float speed = 1f;
    private bool canMove = false;
    private float duration;

    private AudioSource audioSource;
    [SerializeField] private AudioClip waterDrain;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void WaterMovement() //Call this if a puzzle requires water movement
    {
        canMove = true;
        audioSource.PlayOneShot(waterDrain);
    }

    private void Update()
    {
        if(canMove){
            transform.position =
                Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination.position) < 0.2f)
            {
                if (duration <= 0)
                {
                    canMove = false;
                    //Make call to panda tracker
                    print("Water Movement done");
                }
                else
                {
                    duration -= Time.deltaTime;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            WaterMovement();
        }
    }
}
