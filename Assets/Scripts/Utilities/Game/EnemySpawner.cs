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
            GameObject clone = new GameObject();
            spawner.SpawnRandom(seeker, enemiesContainer.transform, ref clone);
            Destroy(clone);
        }
    }

    public void SpawnEnemies(int count) {

    }
}
