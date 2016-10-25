using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 10f;
    public float closeEnough = 1f;
    public Transform[] waypoints;
    public Transform eyes;
    public Vector3 offset = new Vector3(0, .5f, 0);
    public Light visionDisplay;
    public float searchDistance = 5;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public GameObject player;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertSate;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public LastKnownPositionState lastKnownPositionState;
    [HideInInspector] public SearchingState searchingState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Vector3 lastKnownPos;

    private void Awake() {  
        patrolState = new PatrolState(this);
        alertSate = new AlertState(this);
        chaseState = new ChaseState(this);
        lastKnownPositionState = new LastKnownPositionState(this);
        searchingState = new SearchingState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
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

    public void LastKnownPosition() {
        lastKnownPos = player.transform.position;
        currentState = lastKnownPositionState;
    }
}
