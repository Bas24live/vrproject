using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public void ChangeScene (string scene) {
        Application.LoadLevel(scene);
	}

    public void QuitGame() {
        Application.Quit();
    }
}
