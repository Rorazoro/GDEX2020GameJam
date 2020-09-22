using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLevitateMultiPuzzle : MonoBehaviour
{
    public LevitatableObject[] Levitators;

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
        var percentOpen = 0.0f;
        foreach(var levitator in Levitators)
        {
            percentOpen += InvertPercent ? (levitator.transform.position.y - levitator.MinY) / (levitator.MaxY - levitator.MinY) 
                                          : 1.0f - (levitator.transform.position.y - levitator.MinY) / (levitator.MaxY - levitator.MinY);
        }

        float percent = percentOpen/Levitators.Length;

        transform.position = StartPosition + DoorHeight * percent;
    }
}
