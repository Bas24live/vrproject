using UnityEngine;
using System.Collections;

public class SwitchSystem : MonoBehaviour {

    public GameObject switchPrefab;
    public GameObject exitPrefab;
    public int minRoomSize = 4, maxRoomSize = 8;

    GameObject switchContainer;
    int numSwitches, activeSwitches;
    bool openExit;

    void Start() {
        switchContainer = new GameObject("Switches");
        activeSwitches = 0;
        numSwitches = 0;
        openExit = false;
    }

    public void PlaceSwitches() {      
        foreach (Rect r in DungeonGenerator._rooms) {

            int mapXSize = DungeonGenerator._dungeon.GetLength(0)/2;
            int mapZSize = DungeonGenerator._dungeon.GetLength(1)/2;

            int xmin = (int)r.xMin;
            int xmax = (int)r.xMax;
            int zmin = (int)r.yMin;
            int zmax = (int)r.yMax;

            int xLength = (xmax - xmin) - 1;
            int zLength = (zmax - zmin) - 1;

            if ((xLength >= minRoomSize && xLength <= maxRoomSize) && (zLength >= minRoomSize && zLength <= maxRoomSize)) {
                ++numSwitches;
                Vector3 position = new Vector3((xmin - mapXSize + xLength / 2), 0.05f, zmin - mapZSize + (zLength / 2));

                SpawnGameObject(position, switchPrefab, switchContainer.transform);
            }
        }
    }

    public void ActivateSwitch() {    
        ++activeSwitches;
        if (activeSwitches == numSwitches)
            OpenExit();
    }

    void OpenExit() {
        SpawnGameObject(new Vector3(0, .01f, 0), exitPrefab, switchContainer.transform);
    }

    void SpawnGameObject(Vector3 position, GameObject gameObject, Transform parent) {
        Instantiate(gameObject, position, gameObject.transform.rotation, parent);
    }
}
