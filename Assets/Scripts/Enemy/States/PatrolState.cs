using UnityEngine;

public class PatrolState : ISeekerState {

    StatePatternSeeker enemy;    
    Color lightColor = new Color(0, 255, 86, 255);
    int nextWatpoint;

    public PatrolState (StatePatternSeeker statePatternEnemy) {
        enemy = statePatternEnemy;
    }

    public void OnTriggerEnter (Collider collider) {        
    }

    public void ToPatrolState () {
        //Current state
    }

    public void ToAlertState () {
        enemy.currentState = enemy.seekerAlertState;
    }

    public void ToChaseState () {        
        enemy.currentState = enemy.chaseState;
    }

    public void ToLasKnownPositionState () {
        enemy.currentState = enemy.lastKnownPositionState;
    }

    public void ToSearchingState () {
        enemy.currentState = enemy.seekerSearchingState;
    }

    public void ToDeathState () {
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
        if (enemy.visionDisplay.color != lightColor)
            enemy.visionDisplay.color = lightColor;

        enemy.navMeshAgent.destination = enemy.waypoints[nextWatpoint];

        //Move from waypoint to waypoint in a looping fashion
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
            nextWatpoint = (nextWatpoint + 1) % enemy.waypoints.Length;
    }

    
}
