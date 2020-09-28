using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public MinimapPandaScript minimapPandaScript;

    private void Start () {
        controller = gameObject.AddComponent<CharacterController> ();
    }

    void Update () {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        controller.Move (move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown (KeyCode.Space)) {
            DetectClosestPanda (this.gameObject);

        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move (playerVelocity * Time.deltaTime);
    }

    public void DetectClosestPanda (GameObject player) {
        float distanceToClosestPanda = Mathf.Infinity;
        Panda closest = null;

        Panda[] allPandas = GameObject.FindObjectsOfType<Panda> ();

        foreach (Panda currentPanda in allPandas) {
            float distanceToPanda = (currentPanda.transform.position - player.transform.position).sqrMagnitude;
            if (distanceToPanda < distanceToClosestPanda) {
                distanceToClosestPanda = distanceToPanda;
                closest = currentPanda;
            }

        }
        minimapPandaScript.MarkPandaLocation (closest.transform.position);
        //Debug.DrawLine (player.transform.position, closest.transform.position);

    }

    public void CollectPanda (GameObject panda) {
        minimapPandaScript.AddPanda ();
        Destroy (panda);
    }

}