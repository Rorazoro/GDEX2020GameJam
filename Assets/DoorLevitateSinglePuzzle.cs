using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevitateSinglePuzzle : MonoBehaviour
{
    public LevitatableObject Levitator;

    //Yes door height is represented with a vector3. so the door can open in any direction :)
    public Vector3 DoorHeight;
    public Vector3 StartPosition;
    public bool InvertPercent;

    private void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        float percent = (Levitator.transform.position.y - Levitator.MinY) / (Levitator.MaxY - Levitator.MinY);
        if (InvertPercent)
        {
            percent = 1.0f - percent;
        }
        Debug.Log(percent);
        transform.position = StartPosition + DoorHeight * percent;
    }
}
