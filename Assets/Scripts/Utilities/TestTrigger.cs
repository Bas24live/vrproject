using UnityEngine;
using System.Collections;

public class TestTrigger : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown("q")) {
            EventManager.TriggerEvent("LastKnownPosition");
        }
    }
}
