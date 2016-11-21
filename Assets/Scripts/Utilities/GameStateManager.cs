using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour { 
    
    GameObject playerCamera;

    GameObject worldContainer;
    GameObject wallsContainer;

    public GameObject pauseMenu;
    public GameObject blockPrefab;
    public GameObject floorPrefab;
    public GameObject switchPrefab;

    EnemySpawner enemySpawner;

    int ActiveSwitches, inactiveSwitches;
    public int minRoomSize = 4, maxRoomSize = 8;
    

    bool paused;

    void Awake() {   
        worldContainer = GameObject.FindGameObjectWithTag("World");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        enemySpawner = GetComponent<EnemySpawner>();
    }

    void Start() {
        paused = false;
        DungeonGenerator.instance.GenerateHauberkDungeon();
        GenerateByGrid(DungeonGenerator._dungeon);
        //enemySpawner.SpawnSeekers();
        PlaceSwitches();
    }

    public void GenerateByGrid(Tile[,] grid)
    {
        int xSize = grid.GetLength(0);
        int ySize = grid.GetLength(1);
        int xOff = 1;
        float yOff = .5f;
        int zOff = 1;

        wallsContainer = new GameObject("Walls");
        wallsContainer.transform.SetParent(worldContainer.transform);

        GenerateFloor(xSize, ySize);

        for (int x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++)
                if (grid[x, y] == Tile.Wall)
                    CreateBlock(new Vector3(xOff * x, yOff, zOff * y));
    }

    void CreateBlock(Vector3 pos)
    {
        Instantiate(blockPrefab, pos, Quaternion.identity, wallsContainer.transform);
    }

    void GenerateFloor(float width, float height) {
        GameObject floor = Instantiate(floorPrefab, new Vector3((width - 1) / 2, 0, (height - 1) / 2), Quaternion.identity, wallsContainer.transform) as GameObject;
        floor.transform.localScale += new Vector3(.1f * (width / 2.5f), 0, .1f * (height / 2.5f));

        UnityEditor.NavMeshBuilder.BuildNavMesh();
    }

    void PlaceSwitches() {
        GameObject switchContainer = new GameObject("Switches");

        foreach (Rect r in DungeonGenerator._rooms) {
            int xmin = (int)r.xMin;
            int xmax = (int)r.xMax;
            int zmin = (int)r.yMin;
            int zmax = (int)r.yMax;

            int xLength = (xmax - xmin) - 1;
            int zLength = (zmax - zmin) - 1;

            if ((xLength >= minRoomSize && xLength <= maxRoomSize) && (zLength >= minRoomSize && zLength <= maxRoomSize)) {

                Vector3 position = new Vector3(xmin + xLength / 2, 0.05f, zmin + zLength / 2);

                SpawnGameObject(position, switchPrefab, switchContainer.transform);
            }
        }
    }

    void SpawnGameObject (Vector3 position, GameObject gameObject, Transform parent) {
        Instantiate(gameObject, position, gameObject.transform.rotation, parent);
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
