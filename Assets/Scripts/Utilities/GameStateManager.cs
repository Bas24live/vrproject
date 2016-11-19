using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    GameObject pauseMenu;
    GameObject playerCamera;

    GameObject worldContainer;
    GameObject wallsContainer;

    public GameObject blockPrefab;
    public GameObject floorPrefab;

    bool paused;

    void Awake()
    {   
        worldContainer = GameObject.FindGameObjectWithTag("World");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }

    void Start() {
        paused = false;
        DungeonGenerator.instance.GenerateHauberkDungeon();
        GenerateByGrid(DungeonGenerator._dungeon);
    }

    public void GenerateByGrid(Tile[,] grid)
    {
        int xSize = grid.GetLength(0);
        int ySize = grid.GetLength(1);
        int xOff = 1;
        int yOff = 0;
        int zOff = 1;

        Vector3 midOff = new Vector3(0F, -yOff / 2F, 0F); 
        wallsContainer = new GameObject("Walls");
        wallsContainer.transform.SetParent(worldContainer.transform);
       
        //GameObject floor = Instantiate(floorPrefab, new Vector3(0, 0, 0), Quaternion.identity, wallsContainer.transform) as GameObject;
        //floor.transform.localScale.Set(.1f * xSize, 1, .1f * ySize);

        for (int x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++)
                if (grid[x, y] == Tile.Wall)              
                    CreateBlock(new Vector3(xOff * x, .5f, zOff * y) + midOff);
    }

    private void CreateBlock(Vector3 pos)
    {
        Instantiate(blockPrefab, pos, Quaternion.identity, wallsContainer.transform);
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
