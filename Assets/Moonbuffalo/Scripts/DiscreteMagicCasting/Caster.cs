using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Caster : MonoBehaviour
{
    public bool IsCasting = false;
    public UnityEvent OnStartCast, OnEndCast;

    public void StartCast()
    {
        IsCasting = true;
        OnStartCast?.Invoke();
    }

    public void FinishCast()
    {
        IsCasting = false;
        OnEndCast?.Invoke();
    }
}
