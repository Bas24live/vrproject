using UnityEngine;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 10f;
    public float closeEnough = 1f;
    public float distanceAhead = 1f;
    
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

    protected virtual void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");  
    }

    protected virtual void Start() {
        lastKnownPositionState = new LastKnownPositionState(this);
        deathState = new DeathState(this);
    }

    public void LastKnownPosition() {
        lastKnownPos = player.transform.position;
        if (currentState != deathState)
            currentState = lastKnownPositionState;
    }
}
