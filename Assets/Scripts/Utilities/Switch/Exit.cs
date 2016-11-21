using UnityEngine;

public class Exit : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            EventManager.TriggerEvent("NextLevel");
            Destroy();
        }
    }

    void Destroy() {
        Destroy(gameObject);
    }
}
