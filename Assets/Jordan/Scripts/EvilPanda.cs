using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPanda : MonoBehaviour
{
    private bool isAwake = true;
    
    public void Sleep()
    {
        //Sleep animation
        isAwake = false;
    }

    public void WakeUp()
    {
        //Waking animation
        isAwake = true;
    }
    private void Update()
    {
        if (isAwake)
        {
            //If thing is moved, replace the thing
        }
    }
}
