using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 10f;
    public float closeEnough = 1f;
    public float distanceAhead = 1f;
    public Transform[] waypoints;
    public Transform eyes;
    public Vector3 offset = new Vector3(0, .5f, 0);
    public Light visionDisplay;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertSate;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public BlockingState blockingState;
    [HideInInspector] public LastKnownPositionState lastKnownPositionState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Vector3 lastKnownPos;

    private void Awake() {
  
        patrolState = new PatrolState(this);
        alertSate = new AlertState(this);
        chaseState = new ChaseState(this);
        lastKnownPositionState = new LastKnownPositionState(this);
        blockingState = new BlockingState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

	void Start () {
        currentState = patrolState;
	}
	
	void Update () {
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider collider) {
        currentState.OnTriggerEnter(collider);
    }
}
