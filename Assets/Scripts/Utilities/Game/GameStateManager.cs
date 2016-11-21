using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour { 
    
    GameObject playerCamera;

    GameObject worldContainer;
    GameObject wallsContainer;

    public GameObject pauseMenu;
    public GameObject blockPrefab;
    public GameObject floorPrefab;

    EnemySpawner enemySpawner;
    SwitchSystem switchSystem;
    bool paused;

    void Awake() {   
        worldContainer = GameObject.FindGameObjectWithTag("World");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        enemySpawner = GetComponent<EnemySpawner>();
        switchSystem = GetComponent<SwitchSystem>();
    }

    void Start() {
        paused = false;
        DungeonGenerator.instance.GenerateHauberkDungeon();
        GenerateByGrid(DungeonGenerator._dungeon);
        //enemySpawner.SpawnSeekers();
        switchSystem.PlaceSwitches();
    }

    public void GenerateByGrid(Tile[,] grid)
    {
        int xSize = grid.GetLength(0);
        int zSize = grid.GetLength(1);
        int xOff = 1;
        float yOff = .5f;
        int zOff = 1;

        wallsContainer = new GameObject("Walls");
        wallsContainer.transform.SetParent(worldContainer.transform);

        GenerateFloor(xSize, zSize);

        for (int x = 0; x < xSize; x++)
            for (int y = 0; y < zSize; y++)
                if (grid[x, y] == Tile.Wall)
                    CreateBlock(new Vector3(xOff * x - xSize/2, yOff, zOff * y - zSize/2));
    }

    void CreateBlock(Vector3 pos) {
        Instantiate(blockPrefab, pos, Quaternion.identity, wallsContainer.transform);
    }

    void GenerateFloor(float width, float height) {
        GameObject floor = Instantiate(floorPrefab, Vector3.zero, Quaternion.identity, wallsContainer.transform) as GameObject;
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
}
