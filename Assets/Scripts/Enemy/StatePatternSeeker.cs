using UnityEngine;

public class StatePatternSeeker : StatePatternEnemy {

    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public SeekerAlertState seekerAlertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public SeekerSearchingState seekerSearchingState;

    public Vector3[] waypoints;

    protected override void Start() {     
        patrolState = new PatrolState(this);
        seekerAlertState = new SeekerAlertState(this);
        chaseState = new ChaseState(this);
        seekerSearchingState = new SeekerSearchingState(this);

        defaultState = patrolState;
        currentState = patrolState;

        base.Start();
    }

    void Update() {
        currentState.UpdateState();
    }

    public void SetWaypoints (Vector3[] waypoints) {
        this.waypoints = waypoints;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player"))
            currentState = defaultState;
        else
            currentState.OnTriggerEnter(collider);
    }
}
