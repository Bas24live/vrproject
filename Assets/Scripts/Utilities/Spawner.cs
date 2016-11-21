using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {






    void SpawnGameObject(GameObject gameObject, Transform parent) {
        //Spawn gameobject at an open spwan location, use that as the position for this gameobject
    }

    void SpawnGameObject(Vector3 position, GameObject gameObject, Transform parent) {
        Instantiate(gameObject, position, gameObject.transform.rotation, parent);
    }
}
