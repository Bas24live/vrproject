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
        
    }

    public void ToAlertState() {
        //enemy.currentState = enemy.alertSate;
    }

    public void ToChaseState() {
        //enemy.currentState = enemy.chaseState;
        ToBlockingState();
    }

    public void ToLasKnownPositionState() {
        Debug.Log("Can't transition to same state");
    }

    public void ToSearchingState() {
        //enemy.currentState = enemy.searchingState;
    }

    public void ToDeathState() {
        enemy.currentState = enemy.deathState;
    }

    public void UpdateState() {
        Look();
        MoveToLastKnownPos();
    }

    private void Look() {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    private void MoveToLastKnownPos() {
        if (Vector3.Distance(enemy.transform.position, enemy.lastKnownPos) <= enemy.closeEnough)
            ToSearchingState();
        else {
            enemy.visionDisplay.color = new Color(255, 0, 0);
            enemy.navMeshAgent.SetPath(enemy.navMeshAgent.path);// = enemy.lastKnownPos;
            enemy.navMeshAgent.Resume();
        }
    }

    public void ToBlockingState()
    {
        EventManager.TriggerEvent("Block");
    }
}
