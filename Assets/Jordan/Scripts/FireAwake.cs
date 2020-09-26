using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAwake : MonoBehaviour
{
    [SerializeField] private AudioClip fireStartSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(fireStartSound);
    }
}
