using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscreteMagicController : MonoBehaviour
{
    public bool IsCasting;
    public Texture2D CursorTexture;

    public UnityEvent OnStartCast, OnEndCast, OnEndSpell;

    Transform[] nodes;
    void Start()
    {
        Cursor.SetCursor(CursorTexture, new Vector2(16, 16), CursorMode.Auto);

        Transform[] allChildTransforms = transform.GetComponentsInChildren<Transform>();
        List<Transform> allchildMagicNodes = new List<Transform>();

        foreach(var child in allChildTransforms)
        {
           if(child.CompareTag("MagicNode"))
            {
                allchildMagicNodes.Add(child);
            }
        }

        nodes = allchildMagicNodes.ToArray();
        SetNodesActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetIsCasting(!IsCasting);
        }

        if(Input.GetMouseButtonUp(0) && IsCasting)
        {
            CastSpell();
            SetIsCasting(false);
        }
    }

    private void CastSpell()
    {
        Debug.Log("CAST");
    }

    private void SetIsCasting(bool isCasting)
    {
        IsCasting = isCasting;

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
