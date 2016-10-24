using UnityEngine;
using System.Collections;
using System;

public class LastKnownPositionState : IEnemyState {

    private readonly StatePatternEnemy enemy;

    public LastKnownPositionState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {
    }

    public void ToPatrolState() {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAlertState() {
        enemy.currentState = enemy.alertSate;
    }

    public void ToChaseState() {
        enemy.currentState = enemy.chaseState;
    }

    public void ToLasKnownPositionState() {
        Debug.Log("Can't transition to same state");
    }

    public void UpdateState() {
        Look();
        MoveToLastKnownPos();
    }

    private void Look() {
        RaycastHit hit;
        //Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    private void MoveToLastKnownPos() {
        if (Vector3.Distance(enemy.transform.position, enemy.lastKnownPos) <= enemy.closeEnough)
            ToAlertState();
        else {
            enemy.visionDisplay.color = new Color(255, 0, 0);
            enemy.navMeshAgent.destination = enemy.lastKnownPos;
            enemy.navMeshAgent.Resume();
        }
    }


}
