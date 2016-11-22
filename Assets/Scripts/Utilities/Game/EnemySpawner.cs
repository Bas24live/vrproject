using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject seeker, blocker, zombie;

    GameObject enemiesContainer;
    Spawner spawner;

    void Start () {
        enemiesContainer = new GameObject("Enemies");
        spawner = GetComponent<Spawner>();
    }

    public void SpawnSeekers(int count) {
        for (int i = 0; i < count; ++i)
        {
            spawner.SpawnRandom(seeker, enemiesContainer.transform);
        }
    }

    public void SpawnEnemies(int count) {

    }
}
