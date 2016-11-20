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
    [HideInInspector] public IEnemyState defaultState;
    [HideInInspector] public LastKnownPositionState lastKnownPositionState;
    [HideInInspector] public DeathState deathState;


    private void Awake() {  
        lastKnownPositionState = new LastKnownPositionState(this);
        deathState = new DeathState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

	void Start () {
        currentState = defaultState;
	}
	
	void Update () {
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player"))
            currentState = defaultState;
        else
            currentState.OnTriggerEnter(collider);
    }

    public void LastKnownPosition() {
        lastKnownPos = player.transform.position;
        if (currentState != deathState)
            currentState = lastKnownPositionState;
    }
}
