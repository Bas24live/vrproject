using UnityEngine;

public class SwitchSystem : MonoBehaviour {

    public GameObject switchPrefab;
    public GameObject exitPrefab;
    public GameObject worldContainer;
    public int minRoomSize = 4, maxRoomSize = 8;

    Spawner spawner;
    GameObject switchContainer;

    string switchContainerName = "Switches";
    int numSwitches, activeSwitches;
    bool openExit;

    void Awake() {
        worldContainer = GameObject.FindGameObjectWithTag("World");
        spawner = GetComponent<Spawner>();
    }

    void Start() {
        activeSwitches = 0;
        numSwitches = 0;
        openExit = false;
    }

    public void PlaceSwitches() {
        if (worldContainer.transform.FindChild(switchContainerName)) {
            Destroy(switchContainer);
        }

        switchContainer = new GameObject("Switches");
        switchContainer.transform.SetParent(worldContainer.transform);

        while (spawner.SpawnCenter(switchPrefab, switchContainer.transform, minRoomSize, maxRoomSize))
            ++numSwitches;
    }

    public void ActivateSwitch() {    
        ++activeSwitches;
        if (activeSwitches == numSwitches)
            OpenExit();
    }

    void OpenExit() {
        spawner.SpawnGameObject(, exitPrefab, switchContainer.transform);
    }
 
}
