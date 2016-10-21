using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 20f;
    public float sightRadius = 45f;
    public Transform[] waypoints;
    public Transform eyes;
    public Vector3 offset = new Vector3(0, .5f, 0);
    public Light visionDisplay;


    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertSate;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake() {
  
        patrolState = new PatrolState(this);
        alertSate = new AlertState(this);
        chaseState = new ChaseState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

	// Use this for initialization
	void Start () {
        currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider collider) {
        currentState.OnTriggerEnter(collider);
    }
}
