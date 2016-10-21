﻿using UnityEngine;
using System.Collections;
using System;

public class ChaseState : IEnemyState {

    private readonly StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {

    }

    public void ToPatrolState() {

    }

    public void ToAlertState() {
        enemy.currentState = enemy.alertSate;
    }

    public void ToChaseState() {
        Debug.Log("Can't transition to same state");
    }

    public void UpdateState() {
        Look();
        Chase();
    }

    private void Look() {
        RaycastHit hit;
        Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
        if (Physics.SphereCast(enemy.eyes.transform.position, enemy.sightRadius, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
            enemy.chaseTarget = hit.transform;
        else
            ToAlertState();
    }

    private void Chase() {
        enemy.visionDisplay.color = new Color(255, 0, 0);
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.Resume();
    }
}
