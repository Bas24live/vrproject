using UnityEngine;
using System;

public class SearchingState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private Vector3[] destinations;
    private int destIndex;
    private bool newDestinations = true;

    public SearchingState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
        destinations = new Vector3[4];
        destIndex = 0;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player"))
            ToAlertState();
    }

    public void ToPatrolState() {
        destIndex = 0;
        enemy.currentState = enemy.patrolState;
        newDestinations = true;
    }

    public void ToAlertState() {
        destIndex = 0;
        newDestinations = true;
        enemy.currentState = enemy.alertSate;
    }

    public void ToChaseState() {
        destIndex = 0;
        newDestinations = true;
        enemy.currentState = enemy.chaseState;
    }

    public void ToLasKnownPositionState() {
    }

    public void ToSearchingState() {
    }

    public void UpdateState() {
        Look();
        if (newDestinations)
            SetDestinationsRandom();
        Search();
    }

    private void Look() {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Search() {
        enemy.visionDisplay.color = new Color(0, 255, 86, 255);
        
        if (destIndex == (destinations.Length - 1) && Vector3.Distance(enemy.transform.position, destinations[destIndex]) <= enemy.closeEnough)
            ToPatrolState();
        if (Vector3.Distance(enemy.transform.position, destinations[destIndex]) <= enemy.closeEnough)
            ++destIndex;

        enemy.navMeshAgent.destination = destinations[destIndex];
        enemy.navMeshAgent.Resume();
    }

    public void SetDestinationsRandom() {        
        float distance = enemy.searchDistance;
        Vector3 pos = new Vector3(0,0,0);
        destIndex = 0;

        for (int i = 0; i < 4; i++) {
            bool validDestination = false;
            while (!validDestination) {
                Vector3 randomDirection = enemy.transform.position + UnityEngine.Random.insideUnitSphere * distance;
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas)) {
                    pos = navHit.position;
                    validDestination = true;
                }
            }
            destinations[i] = pos;
        }
        newDestinations = false;
    }
}