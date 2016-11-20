using UnityEngine;
using System.Collections;
using System;

public class SeekerAlertState : ISeekerState {

    private readonly StatePatternSeeker enemy;
    private float searchTimer;

    public SeekerAlertState(StatePatternSeeker statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {
        
    }

    public void ToPatrolState() {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
    }

    public void ToAlertState() {
        //Current State
    }

    public void ToChaseState() {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    public void ToSearchingState() {
        enemy.currentState = enemy.seekerSearchingState;
    }

    public void ToLasKnownPositionState() {
        EventManager.TriggerEvent("LastKnownPosition");
    }

    public void ToDeathState() {
        enemy.currentState = enemy.deathState;
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

        //enemy.rb.MoveRotation(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchingDuration)
            ToPatrolState();
    }
}
