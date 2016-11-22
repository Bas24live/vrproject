using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 startingPosition;

    public void Spawn(Spawner spawner) {   
        Vector2 temp = new Vector2();
        if (spawner.GetRoom(ref temp))
            transform.position = new Vector3(temp.x, 10, temp.y);
    }

    public void Spawn() {
        transform.position = startingPosition;
    }

    public Vector3 GetStartingPosition() {
        return startingPosition;
    }

    public void SetStartingPosition(Vector3 position) {
        startingPosition = position;
    }
}
