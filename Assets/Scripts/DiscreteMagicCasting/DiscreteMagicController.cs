using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscreteMagicController : MonoBehaviour
{
    public bool IsCasting;

    public UnityEvent OnStartCast, OnEndCast, OnEndSpell;

    Transform[] nodes;
    void Start()
    {
        Transform[] allChildComponents = transform.GetComponentsInChildren<Transform>();
        List<Transform> childrenWeCareAbout = new List<Transform>();

        foreach(var child in allChildComponents)
        {
           if(child != transform)
            {
                childrenWeCareAbout.Add(child);
            }
        }

        nodes = childrenWeCareAbout.ToArray();
        SetNodesActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetIsCasting(!IsCasting);
        }
    }

    private void SetIsCasting(bool isCasting)
    {
        IsCasting = isCasting;
        SetNodesActive(IsCasting);

        if (IsCasting)
        {
            Cursor.lockState = CursorLockMode.Confined;
            OnStartCast?.Invoke();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            OnEndCast?.Invoke();
        }
    }

    private void SetNodesActive(bool active)
    {
        foreach (var node in nodes)
        {
            node.gameObject.SetActive(active);
        }
    }

}
