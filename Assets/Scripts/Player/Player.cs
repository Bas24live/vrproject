using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Vector3 startingPosition = new Vector3(0, 10, 0);


	void Start () {
        transform.position = startingPosition;
    }

    public void Spawn() {
        transform.position = startingPosition;
    }
}
