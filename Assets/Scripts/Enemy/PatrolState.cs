﻿using UnityEngine;
using System.Collections;
using System;

public class PatrolState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private int nextWatpoint;

    public PatrolState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player"))
            ToAlertState();
        if (collider.CompareTag("Pickupable")) {
            enemy.thrownVelocity = collider.GetComponent<Rigidbody>().velocity;
            ToDeathState();
        }
    }

    public void ToPatrolState() {
        //Current state
    }

    public void ToAlertState() {
        enemy.currentState = enemy.alertSate;
    }

    public void ToChaseState() {        
        enemy.currentState = enemy.chaseState;
        ToBlockingState();
    }

    public void ToLasKnownPositionState() {
        enemy.currentState = enemy.lastKnownPositionState;
    }

    public void ToSearchingState() {
        enemy.currentState = enemy.searchingState;
    }

    public void ToBlockingState() {
        EventManager.TriggerEvent("Block");
    }

    public void ToDeathState() {
        Debug.Log("From patrol to death");
        enemy.currentState = enemy.deathState;
        
    }

    public void UpdateState() {
        Look();
        Patrol();
    }

    void Look() {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player") ) {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Patrol() {
        enemy.visionDisplay.color = new Color(0, 255, 86, 255);
        enemy.navMeshAgent.destination = enemy.waypoints[nextWatpoint].position;
        //enemy.navMeshAgent.Resume();

        //Move from waypoint to waypoint in a looping fashion
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
            nextWatpoint = (nextWatpoint + 1) % enemy.waypoints.Length;
    }

    
}
