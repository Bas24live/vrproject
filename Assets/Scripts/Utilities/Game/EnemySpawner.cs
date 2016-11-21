using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject seeker, blocker, zombie;

    GameObject enemiesContainer;

    void Start () {
        DungeonGenerator.instance.GenerateHauberkDungeon();
        enemiesContainer = new GameObject("Enemies");
    }

    public void SpawnSeekers() {
        foreach (Rect r in DungeonGenerator._rooms) {
            int xmin = (int)r.xMin;
            int xmax = (int)r.xMax;
            int ymin = (int)r.yMin;
            int ymax = (int)r.yMax;

            GameObject seeker1 = SpawnEnemy (new Vector3(xmin, 0, ymin), seeker);
            GameObject seeker2 = SpawnEnemy(new Vector3(xmax - 1, 0, ymax - 1), seeker);

            StatePatternSeeker seekerPattern1 = seeker1.GetComponent<StatePatternSeeker> ();
            StatePatternSeeker seekerPattern2 = seeker2.GetComponent<StatePatternSeeker>();

            Vector3[] waypoints = GenerateWaypoints(xmin, xmax - 1, ymin, ymax - 1);

            seekerPattern1.SetWaypoints(waypoints);
            seekerPattern2.SetWaypoints(waypoints);
        }
    }

    GameObject SpawnEnemy (Vector3 position, GameObject enemy) {
        return Instantiate(enemy, position, enemy.transform.rotation, enemiesContainer.transform) as GameObject;
    }

    Vector3[] GenerateWaypoints (int xMin, int xMax, int zMin, int zMax) {
        Vector3[] waypoints = new Vector3[4];

        waypoints[0] = new Vector3(xMin, 0, zMin);
        waypoints[1] = new Vector3(xMin, 0, zMax);
        waypoints[2] = new Vector3(xMax, 0, zMax);
        waypoints[3] = new Vector3(xMax, 0, zMin);

        return waypoints;
    }


}
