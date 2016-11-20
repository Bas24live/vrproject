using UnityEngine;
using System.Collections;

public class StatePatternSeeker : StatePatternEnemy {


    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public SeekerAlertState seekerAlertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public SeekerSearchingState seekerSearchingState;

    void Awake () {
        patrolState = new PatrolState(this);
        seekerAlertState = new SeekerAlertState(this);
        chaseState = new ChaseState(this);
        seekerSearchingState = new SeekerSearchingState(this);
    }

    void Start () {
        base.defaultState = patrolState;
	}
	
	void Update () {
       
	}


}
