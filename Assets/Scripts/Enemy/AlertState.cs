﻿using UnityEngine;
using System.Collections;
using System;

public class AlertState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private float searchTimer;

    public AlertState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {
        
    }

    public void ToPatrolState() {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
    }

    public void ToAlertState() {
        Debug.Log("Can't transition to same state");
    }

    public void ToChaseState() {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    public void ToSearchingState() {
        enemy.currentState = enemy.searchingState;
    }

    public void ToLasKnownPositionState()
    {
    }

    public void UpdateState() {
        Look();
        Alert();
    }

    private void Look() {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Alert() {
        enemy.visionDisplay.color = new Color(253, 246, 0);
        enemy.navMeshAgent.Stop();

        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchingDuration)
            ToPatrolState();
    }

    public void ToBlockingState()
    {
        enemy.currentState = enemy.blockingState;
    }
}
