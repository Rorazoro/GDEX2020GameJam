using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {
    public Transform player;

    private void LateUpdate () {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler (90f, player.eulerAngles.y, 0f);
    }

    public void ZoomIn () {
        float size = this.GetComponent<Camera> ().orthographicSize;

        if (size > 10) {
            size = size - 5;
            this.GetComponent<Camera> ().orthographicSize = size;
        }
    }

    public void ZoomOut () {
        float size = this.GetComponent<Camera> ().orthographicSize;

        if (size < 150) {
            size = size + 5;
            this.GetComponent<Camera> ().orthographicSize = size;
        }
    }
}