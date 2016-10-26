using UnityEngine;
using System.Collections;
using System;

public class ChaseState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private bool callForBlockHelp = true;

    public ChaseState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {

    }

    public void ToPatrolState() {

    }

    public void ToLasKnownPositionState() {
        EventManager.TriggerEvent("LastKnownPosition");
        callForBlockHelp = true;
    }

    public void ToAlertState() {
        enemy.currentState = enemy.alertSate;
        callForBlockHelp = true;
    }

    public void ToChaseState() {
        Debug.Log("Can't transition to same state");
    }

    public void ToSearchingState() {
        enemy.currentState = enemy.searchingState;
        callForBlockHelp = true;
    }

    public void UpdateState() {
        Look();
        Chase();
    }

    private void Look() {
        RaycastHit hit;
        Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
            enemy.chaseTarget = hit.transform;
        else
            ToLasKnownPositionState();
    }

    private void Chase() {
        if (callForBlockHelp) {
            EventManager.TriggerEvent("Block");
            callForBlockHelp = false;
        }

        enemy.visionDisplay.color = new Color(255, 0, 0);
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();
    }

    public void ToBlockingState()
    {
    }
}
