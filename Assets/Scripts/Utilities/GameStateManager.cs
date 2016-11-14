using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public Transform canvas;
    public Transform playerCamera;

    bool paused;

    void Start() {
        paused = false;        
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
        Time.timeScale = 1;
        playerCamera.gameObject.GetComponent<CameraController>().enabled = true;
        canvas.gameObject.SetActive(false);
        
    }

    public void Pause() {
        Time.timeScale = 0;
        playerCamera.gameObject.GetComponent<CameraController>().enabled = false;
        canvas.gameObject.SetActive(true);
        
    }


}
