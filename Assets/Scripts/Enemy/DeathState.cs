using UnityEngine;
using System.Collections;

public class DeathState : IEnemyState {

    private readonly StatePatternEnemy enemy;
    private int nextWatpoint;

    public DeathState(StatePatternEnemy statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Wall")) {
            Debug.Log("Touched a wall");
            Object.Instantiate(enemy.wall, enemy.transform.position, new Quaternion(0,0,0,0));
            Object.Destroy(enemy.gameObject);
        }
    }

    public void ToPatrolState() {
    }

    public void ToAlertState() {
        Debug.Log("From death to alert");
        enemy.currentState = enemy.alertSate;
    }

    public void ToChaseState() {
        
    }

    public void ToLasKnownPositionState() {
        
    }

    public void ToSearchingState() {
        
    }

    public void ToBlockingState() {
        
    }

    public void ToDeathState() {
        //Current State
    }

    public void UpdateState() {
        //enemy.rb.velocity = enemy.thrownVelocity;
        //if (enemy.rb.velocity.magnitude <= 2)
        //     ToAlertState();
    }
}
