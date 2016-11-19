using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public Transform canvas;
    public Transform playerCamera;
    private Transform mapParent;
    private Transform dynBlockPrefab;

    bool paused;

    void Awake()
    {
        dynBlockPrefab = GameObject.FindWithTag("Wall_Inner").transform;
        DungeonGenerator.instance.GenerateHauberkDungeon(23, 23);
        GenerateByGrid(DungeonGenerator._dungeon);
    }

    void Start() {
        paused = false;
    }

    public void GenerateByGrid(Tile[,] grid)
    {
        int xSize = grid.GetLength(0);
        int ySize = grid.GetLength(1);
        int xOff = 1;
        int yOff = 0;
        int zOff = 1;

        Vector3 midOff = new Vector3(0F, -yOff / 2F, 0F); 
        mapParent = new GameObject().transform;
        mapParent.name = "DynamicBlocks";

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                switch (grid[x, y])
                {
                    default:
                        break;
                    case Tile.Floor:
                        //CreateBlock(new Vector3(xOff * x, yOff * 0F, zOff * y) + midOff);
                        break;
                    case Tile.RoomFloor:
                        //CreateBlock(new Vector3(xOff * x, yOff * 0F, zOff * y) + midOff);
                        break;
                    case Tile.Wall:
                        CreateBlock(new Vector3(xOff * x, yOff * 0F, zOff * y) + midOff);
                        CreateBlock(new Vector3(xOff * x, yOff * 1F, zOff * y) + midOff);
                        break;
                }
            }
        }
    }

    private void CreateBlock(Vector3 pos)
    {
        GameObject block = Instantiate(dynBlockPrefab, pos, Quaternion.identity) as GameObject;
        block.transform.parent = mapParent;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            paused = !paused;

        if (paused)
            Pause();
        else
            Resume();
	}


    public void Resume() {
        paused = false;
        Time.timeScale = 1;        
        canvas.gameObject.SetActive(false);
        
    }

    public void Pause() {
        Time.timeScale = 0;
        canvas.gameObject.SetActive(true);
        
    }


}
