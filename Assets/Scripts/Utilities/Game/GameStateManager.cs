﻿using UnityEngine;

public class GameStateManager : MonoBehaviour { 

    public GameObject pauseMenu;
    public GameObject blockPrefab;
    public GameObject floorPrefab;

    GameObject player;
    GameObject playerCamera;
    GameObject worldContainer;

    Spawner spawner;
    EnemySpawner enemySpawner;
    SwitchSystem switchSystem;

    string mapContainerName = "Map";
    int currentLevel = 0;
    bool paused;

    void Awake() {   
        worldContainer = GameObject.FindGameObjectWithTag("World");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");

        player = GameObject.FindGameObjectWithTag("Player");
        spawner = GetComponent<Spawner>();
        enemySpawner = GetComponent<EnemySpawner>();
        switchSystem = GetComponent<SwitchSystem>();
    }

    void Start() {
        paused = false;
        GenerateWorld();        
    }

    public void GenerateByGrid(Tile[,] grid) {
        int xSize = grid.GetLength(0);
        int zSize = grid.GetLength(1);
        int xOff = 1;
        float yOff = .5f;
        int zOff = 1;

        GameObject mapContainer = new GameObject(mapContainerName);
        mapContainer.transform.SetParent(worldContainer.transform);

        GenerateFloor(xSize, zSize, mapContainer.transform);

        for (int x = 0; x < xSize; x++)
            for (int z = 0; z < zSize; z++)
                if (grid[x, z] == Tile.Wall)
                    spawner.SpawnGameObject(new Vector3(xOff * x, yOff, zOff * z), blockPrefab, mapContainer.transform);
    }

    void GenerateFloor(float width, float height, Transform parent) {
        GameObject floor = Instantiate(floorPrefab, Vector3.zero, Quaternion.identity, parent) as GameObject;
        floor.transform.localScale += new Vector3(.1f * (width / 2.5f), 0, .1f * (height / 2.5f));

        UnityEditor.NavMeshBuilder.BuildNavMesh();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            if (paused)
                Pause();
            else
                Resume();
        }
	}

    public void Resume() {
        paused = false;
        Time.timeScale = 1;        
        pauseMenu.gameObject.SetActive(false);  
    }

    public void Pause() {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void NextLevel() {
        ++currentLevel;
        (player.GetComponent<Player>()).Spawn();
        GenerateWorld();
    }

    void GenerateWorld() {
        DungeonGenerator.SetSeed(currentLevel);
        DungeonGenerator.instance.GenerateHauberkDungeon();        

        if (worldContainer.transform.FindChild(mapContainerName))
            Destroy(worldContainer.transform.FindChild(mapContainerName).gameObject);

        GenerateByGrid(DungeonGenerator._dungeon);
        switchSystem.PlaceSwitches();
        enemySpawner.SpawnSeekers();
    }
}
