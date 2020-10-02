using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private GameObject fireAreaAudio;
    [SerializeField] private AudioSource fireAudio;
    [SerializeField] private GameObject waterAreaAudio;
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private GameObject stoneAreaAudio;
    [SerializeField] private AudioSource stoneAudio;
    [SerializeField] private GameObject forestAreaAudio;
    [SerializeField] private AudioSource forestAudio;
    [SerializeField] private GameObject startingAudio;
    [SerializeField] private AudioSource quarterCompleteAudio;
    [SerializeField] private AudioSource halfCompleteAudio;
    [SerializeField] private AudioSource threeQuartersCompleteAudio;
    [SerializeField] private AudioSource allCompleteAudio;

    [Header("Mixer Amounts")] 
    [SerializeField] private float fireVolume = 0f;
    [SerializeField] private float waterVolume = 0f;
    [SerializeField] private float stoneVolume = 0f;
    [SerializeField] private float forestVolume = 0f;
    [SerializeField] private float quarterVolume = 0f;
    [SerializeField] private float halfVolume = 0f;
    [SerializeField] private float thirdVolume = 0f;
    [SerializeField] private float allVolume = 0f;
    private void Start()
    {
        fireAudio.volume = -80f;
        waterAudio.volume = -80f;
        stoneAudio.volume = -80f;
        forestAudio.volume = -80f;
        quarterCompleteAudio.volume = -80f;
        halfCompleteAudio.volume = -80f;
        threeQuartersCompleteAudio.volume = -80f;
        allCompleteAudio.volume = -80f;
    }

    private int totalPandas = 0;

    public void SetPandaNum(int num) //Call at start of game by Game Manager
    {
        totalPandas = num;
    }

    public void PandaFound(int currentNum)
    {
        CheckFourth(currentNum);
        CheckHalf(currentNum);
        CheckThreeFourths(currentNum);
        CheckAll(currentNum);
    } //Call every time a puzzle is complete

    public void AreaComplete(string area)
    {
        switch (area)
        {
            case "fire":
                fireAreaAudio.SetActive(false);
                fireAudio.volume = fireVolume;
                break;
            case "water":
                waterAreaAudio.SetActive(false);
                waterAudio.volume = waterVolume;
                break;
            case "stone":
                stoneAreaAudio.SetActive(false);
                stoneAudio.volume = stoneVolume;
                break;
            case "forest":
                forestAreaAudio.SetActive(false);
                forestAudio.volume = forestVolume;
                break;
        }
    } //Call when all pandas of that area are found
    
    #region Panda Number Check
    private void CheckFourth(int num)
    {
        if (num * 4 == totalPandas)
        {
            quarterCompleteAudio.volume = quarterVolume;
        }
    }

    private void CheckHalf(int num)
    {
        if (num * 2 == totalPandas)
        {
            halfCompleteAudio.volume = halfVolume;
        }
    }

    private void CheckThreeFourths(int num)
    {
        if ((totalPandas - num) * 4 == totalPandas)
        {
            threeQuartersCompleteAudio.volume = thirdVolume;
        }
    }

    private void CheckAll(int num)
    {
        if (num == totalPandas)
        {
            startingAudio.SetActive(false);
            fireAudio.volume = -80f;
            waterAudio.volume = -80f;
            stoneAudio.volume = -80f;
            forestAudio.volume = -80f;
            quarterCompleteAudio.volume = -80f;
            halfCompleteAudio.volume = -80f;
            threeQuartersCompleteAudio.volume = -80f;
            allCompleteAudio.volume = allVolume;
        }
    }
    #endregion 
}
