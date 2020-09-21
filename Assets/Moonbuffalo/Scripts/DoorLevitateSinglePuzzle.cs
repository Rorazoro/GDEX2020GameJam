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
        float percent = InvertPercent ? (Levitator.transform.position.y - Levitator.MinY) / (Levitator.MaxY - Levitator.MinY)
                                             : 1.0f - (Levitator.transform.position.y - Levitator.MinY) / (Levitator.MaxY - Levitator.MinY);
        transform.position = StartPosition + DoorHeight * percent;
    }
}
