using UnityEngine;
using System.Collections;

public class StatePatternSeeker : StatePatternEnemy {


    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public SeekerAlertState seekerAlertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public SeekerSearchingState seekerSearchingState;

    public Vector3[] waypoints;

    void Awake () {
        patrolState = new PatrolState(this);
        seekerAlertState = new SeekerAlertState(this);
        chaseState = new ChaseState(this);
        seekerSearchingState = new SeekerSearchingState(this);
    }

    void Start () {
        defaultState = patrolState;
        currentState = patrolState;
	}
	
	void Update () {
        currentState.UpdateState();
	}

    public void SetWaypoints (Vector3[] waypoints) {
        this.waypoints = waypoints;
    }

}
