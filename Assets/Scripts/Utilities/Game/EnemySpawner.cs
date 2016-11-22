using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject seeker, blocker, zombie;

    string enemiesContainerName;
    GameObject enemiesContainer;
    GameObject worldContainer;
    Spawner spawner;

    void Start () {       
        spawner = GetComponent<Spawner>();
        worldContainer = GameObject.FindGameObjectWithTag("World");
    }

    public void SpawnSeekers(int count) {
        if (worldContainer.transform.FindChild(enemiesContainerName))
            Destroy(enemiesContainer);

        enemiesContainer = new GameObject("Enemies");
        enemiesContainer.transform.SetParent(worldContainer.transform);

        for (int i = 0; i < count; ++i)
            spawner.SpawnRandom(seeker, enemiesContainer.transform);
    }

    public void SpawnEnemies(int count) {

    }
}
