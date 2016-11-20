using UnityEngine;
using System.Collections;
using System;

public class BlockingState : IBlockerState {
    private readonly StatePatternBlocker enemy;

    public BlockingState(StatePatternBlocker statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        MoveTo();
        //Look();
    }

    public void OnTriggerEnter(Collider collider)
    {
    }

    public void ToAlertState() {

    }

    public void ToBlockingState() {

    }

    public void ToLasKnownPositionState() {
        EventManager.TriggerEvent("LastKnownPosition");
    }

    public void ToSearchingState() {
    }

    public void ToDeathState() {
        enemy.currentState = enemy.deathState;
    }

    private void Look() {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) {
            enemy.chaseTarget = hit.transform;
        }
    }

    void MoveTo() {
        enemy.visionDisplay.color = new Color(253, 246, 0);
        Vector3 position = enemy.player.transform.position;
       
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, enemy.distanceAhead, NavMesh.AllAreas)) {
            enemy.navMeshAgent.destination = hit.position;
        }
    }
}
