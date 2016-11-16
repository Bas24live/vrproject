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
        paused = false;
        Time.timeScale = 1;        
        canvas.gameObject.SetActive(false);
        
    }

    public void Pause() {
        Time.timeScale = 0;
        canvas.gameObject.SetActive(true);
        
    }


}
