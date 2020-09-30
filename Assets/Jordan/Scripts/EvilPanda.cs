using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvilPanda : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPositions;
    [SerializeField] private float patrolPause = 10f; //Amount of time moving from one position to another
    private NavMeshAgent agent;
    private bool isAwake = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }
    private IEnumerator Patrol() {
        if (isAwake) {
            agent.SetDestination(patrolPositions[UnityEngine.Random.Range(0, patrolPositions.Length)].transform.position);
            //else { SetDestination(basementPatrolPos[Random.Range(0, basementPatrolPos.Length)].transform.position); }
            yield return new WaitForSeconds(patrolPause);
            StartCoroutine(Patrol());
        }
    }
    public void Sleep()
    {
        //Snoring sounds
        //Sleep animation
        isAwake = false;
    }
}
