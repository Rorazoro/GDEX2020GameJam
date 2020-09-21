using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHalfWayLevitateMulti : MonoBehaviour
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
        foreach (var levitator in Levitators)
        {
            percentOpen += Mathf.Abs((levitator.transform.position.y - levitator.MinY) / (levitator.MaxY - levitator.MinY)-.5f);
        }

        float percent = InvertPercent ? percentOpen / Levitators.Length * 2.0f
                                      : 1.0f - percentOpen / Levitators.Length * 2.0f;

        transform.position = StartPosition + DoorHeight * percent;
    }
}
