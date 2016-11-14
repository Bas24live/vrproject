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
    public float searchDistance = 5;
    public GameObject wall;

    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public GameObject player;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Vector3 lastKnownPos;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector3 thrownVelocity;

    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertSate;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public BlockingState blockingState;
    [HideInInspector] public LastKnownPositionState lastKnownPositionState;
    [HideInInspector] public SearchingState searchingState;
    [HideInInspector] public DeathState deathState;


    private void Awake() {  
        patrolState = new PatrolState(this);
        alertSate = new AlertState(this);
        chaseState = new ChaseState(this);
        lastKnownPositionState = new LastKnownPositionState(this);
        searchingState = new SearchingState(this);
        blockingState = new BlockingState(this);
        deathState = new DeathState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
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
        if (currentState != deathState)
            currentState = lastKnownPositionState;
    }

    public void Block() {
        if (currentState != chaseState)
            currentState = blockingState;
    }
}
