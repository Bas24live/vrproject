using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 startingPosition;
    bool alive = false;

    
    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            alive = false;
            Spawn();
        }
    }

    public void Spawn() {
        transform.position = startingPosition;
        alive = true;
    }

    public void Spawn(Spawner spawner) {
        Vector2 temp = new Vector2();
        if (spawner.GetRoom(ref temp)) {
            startingPosition = new Vector3(temp.x, 10, temp.y);
            transform.position = startingPosition;
        }
    }

    public Vector3 GetStartingPosition() {
        return startingPosition;
    }

    public void SetStartingPosition(Vector3 position) {
        startingPosition = position;
        alive = true;
    }
}
