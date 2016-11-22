using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject seeker, blocker, zombie;

    GameObject enemiesContainer;

    public int minRoomSize = 4, maxRoomSize = 8;

    void Start () {
        DungeonGenerator.instance.GenerateHauberkDungeon();
        enemiesContainer = new GameObject("Enemies");
    }

    public void SpawnSeekers() {
        foreach (Rect r in DungeonGenerator._rooms) {

            int mapXSize = DungeonGenerator._dungeon.GetLength(0) / 2;
            int mapZSize = DungeonGenerator._dungeon.GetLength(1) / 2;

            int xmin = (int)r.xMin;
            int xmax = (int)r.xMax;
            int zmin = (int)r.yMin;
            int zmax = (int)r.yMax;


            int xLength = (xmax - xmin) - 1;
            int zLength = (zmax - zmin) - 1;

            if ((xLength >= minRoomSize && xLength <= maxRoomSize) && (zLength >= minRoomSize && zLength <= maxRoomSize)) {
                GameObject seeker1 = SpawnEnemy(new Vector3(xmin - mapXSize, 0, zmin - mapZSize), seeker);

                StatePatternSeeker seekerPattern1 = seeker1.GetComponent<StatePatternSeeker>();
               

                Vector3[] waypoints = GenerateWaypoints(xmin - mapXSize, xmax - 1 - mapXSize, zmin - mapZSize, zmax - 1 -mapZSize);

                seekerPattern1.SetWaypoints(waypoints);
            }    
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
