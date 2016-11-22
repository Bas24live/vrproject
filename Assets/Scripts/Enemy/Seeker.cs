using UnityEngine;

public class Seeker : MonoBehaviour {

    int numWaypoints = 4;
    StatePatternSeeker statePatternSeeker;
    Spawner spawner;
    Vector3[] waypoints;

    void Awake() {
        statePatternSeeker = GetComponent<StatePatternSeeker>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Start() {
        GenerateWaypoints();
        statePatternSeeker.SetWaypoints(waypoints);
    }

    void GenerateWaypoints() {
        waypoints = new Vector3[numWaypoints];

        for (int i = 0; i < numWaypoints; ++i) {
            Vector2 temp = new Vector2();

            if (spawner.GetRoom(ref temp)) {
                waypoints[i] = new Vector3(temp.x, 0, temp.y);
            }
            else
                break;
        }
    }
}
