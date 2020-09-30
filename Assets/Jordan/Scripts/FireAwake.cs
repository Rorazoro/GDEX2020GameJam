using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAwake : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private AudioClip fireStartSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        fire.Play();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(fireStartSound);
       // fire.Play();
    }

    private void OnDisable()
    {
        //fire.Stop();
    }
}
