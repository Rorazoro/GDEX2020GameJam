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
        StartPosition = transform.localPosition;
    }

    void Update()
    {
        float percent = InvertPercent ? (Levitator.transform.localPosition.y - Levitator.YPosMin) / (Levitator.YPosMax - Levitator.YPosMin)
                                             : 1.0f - (Levitator.transform.localPosition.y - Levitator.YPosMin) / (Levitator.YPosMax - Levitator.YPosMin);
        transform.localPosition = StartPosition + DoorHeight * percent;
    }
}
