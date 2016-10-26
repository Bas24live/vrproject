using UnityEngine;
using System.Collections;
using System;

public class BlockingState : IEnemyState {
    private readonly StatePatternEnemy enemy;

    public BlockingState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        MoveTo();
        Look();
    }

    public void OnTriggerEnter(Collider collider)
    {
    }

    public void ToAlertState() {

    }

    public void ToBlockingState() {
    }

    public void ToChaseState() {
    }

    public void ToLasKnownPositionState() {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToSearchingState() {
    }

    private void Look() {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
            //ToChaseState();
        }
    }

    void MoveTo() {
        Vector3 position = enemy.player.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, enemy.distanceAhead, NavMesh.AllAreas)) {
            enemy.navMeshAgent.destination = hit.position;
        }
        else
            ToPatrolState();
        enemy.navMeshAgent.Resume();

    }
}
