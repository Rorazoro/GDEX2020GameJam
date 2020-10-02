using UnityEngine;

public class MagicController : MonoBehaviour {
    [SerializeField]
    private ICastable currentTarget;

    public float range = 100;
    public GameObject FollowTarget;

    private void Update () {
        RaycastForInteractable ();
        if (InputManager.Instance.CastInput) {
            if (currentTarget != null) {
                currentTarget.OnInteract ();
            } else if (MagicManager.Instance.IsSpellActive) {
                MagicManager.Instance.EndSpell ();
            }

        }
    }

    private void RaycastForInteractable () {
        Ray ray = new Ray (FollowTarget.transform.position, FollowTarget.transform.forward);
        if (Physics.Raycast (ray, out RaycastHit hit, range)) {
            ICastable interactable = hit.collider.GetComponent<ICastable> ();
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
        Debug.DrawRay (ray.origin, ray.direction * 10, Color.blue);
    }
}