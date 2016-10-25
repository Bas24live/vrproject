using UnityEngine;
using System.Collections;
using System;

public class BlockingState : IEnemyState {
    private readonly StatePatternEnemy enemy;
    private GameObject player;

    public BlockingState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        NavMeshHit hit;
        if(NavMesh.SamplePosition(player.transform.forward, out hit, enemy.distanceAhead, NavMesh.AllAreas)){
            enemy.navMeshAgent.destination = hit.position;
        }
        enemy.navMeshAgent.Resume();
    }

    public void OnTriggerEnter(Collider collider)
    {
    }

    public void ToAlertState()
    {
    }

    public void ToBlockingState()
    {
    }

    public void ToChaseState()
    {
    }

    public void ToLasKnownPositionState()
    {
    }

    public void ToPatrolState()
    {
    }
}
