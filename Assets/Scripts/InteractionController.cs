using UnityEngine;

public class InteractionController : MonoBehaviour {
    [SerializeField]
    private IInteractable currentTarget;

    public float range = 100;
    public GameObject FollowTarget;

    private void Update () {
        RaycastForInteractable ();
        if (InputManager.Instance.InteractInput && (currentTarget as IInteractable) != null) {
            (currentTarget as IInteractable).OnInteract ();
        } else if (InputManager.Instance.CastInput) {
            if ((currentTarget as ICastable) != null && !MagicManager.Instance.IsSpellActive) {
                Debug.Log ("Starting Casting");
                (currentTarget as ICastable).OnInteract ();
            } else if (MagicManager.Instance.IsSpellActive) {
                Debug.Log ("Ending Spell");
                MagicManager.Instance.EndSpell ();
            }
        }
    }

    private void RaycastForInteractable () {
        Ray ray = new Ray (FollowTarget.transform.position, FollowTarget.transform.forward);
        if (Physics.Raycast (ray, out RaycastHit hit, range)) {
            IInteractable interactable = hit.collider.GetComponent<IInteractable> ();
            if (interactable != null) {
                if (hit.distance <= interactable.MaxRange) {
                    if (interactable == currentTarget) {
                        return;
                    } else if (currentTarget != null) {
                        currentTarget.OnEndHover ();
                        currentTarget = interactable;
                        currentTarget.OnStartHover ();
                        return;
                    } else {
                        currentTarget = interactable;
                        currentTarget.OnStartHover ();
                        return;
                    }
                } else {
                    if (currentTarget != null) {
                        currentTarget.OnEndHover ();
                        currentTarget = null;
                        return;
                    }
                }
            } else {
                if (currentTarget != null) {
                    currentTarget.OnEndHover ();
                    currentTarget = null;
                    return;
                }
            }
        } else {
            if (currentTarget != null) {
                currentTarget.OnEndHover ();
                currentTarget = null;
                return;
            }
        }
        Debug.DrawRay (ray.origin, ray.direction * 10, Color.red);
    }
}