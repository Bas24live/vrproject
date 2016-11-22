﻿using UnityEngine;

public class ChaseState : IEnemyState {

    StatePatternSeeker enemy;
    bool callForBlockHelp = true;


    public ChaseState(StatePatternSeeker statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {

    }

    public void ToPatrolState() {

    }

    public void ToLasKnownPositionState() {
        EventManager.TriggerEvent("LastKnownPosition");
    }

    public void ToAlertState() {
        enemy.currentState = enemy.seekerAlertState;
    }

    public void ToChaseState() {
        Debug.Log("Can't transition to same state");
    }

    public void ToSearchingState() {
        enemy.currentState = enemy.seekerSearchingState;
    }

    public void ToDeathState() {
        enemy.currentState = enemy.deathState;
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
        enemy.visionDisplay.color = new Color(255, 0, 0);
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
    }

    public void ToBlockingState() {
        EventManager.TriggerEvent("Block");
        callForBlockHelp = false;
    }
}
