using UnityEngine;
using System.Collections;

public class PushObjectController : MonoBehaviour {
    
    public float distance = 2f;
    public float throwForce = 20f;

    GameObject pushedObject;
    Rigidbody rb;
    bool pushing = false;
    Vector3 direction;

    void Update() {
        if (pushing) {
            Pushing(pushedObject);
            if (Input.GetKeyDown(KeyCode.Q))
                //ThrowObject();

            if (Input.GetKeyDown(KeyCode.E))
                Leave();
        }
        else {
            Push();
        }
    }

    void Push() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance) && hit.collider.CompareTag("Wall_Inner")) {
                pushedObject = hit.collider.gameObject;
                rb = pushedObject.GetComponent<Rigidbody>();
                pushing = true;
                direction = getDirection();
            }
        }
    }

    void Pushing(GameObject o) {
        direction = new Vector3(direction.x * transform.position.x, direction.y * transform.position.y, direction.z * transform.position.z);
        o.transform.position = direction + o.transform.position * distance;
    }

    void Leave() {
        pushing = false;
        pushedObject = null;
    }

    Vector3 getDirection() {
        float yRotation = transform.rotation.y;

        if (yRotation >= -45 && yRotation <= 45)
            return new Vector3(0, 0, 1);
        else if (yRotation >= 45 && yRotation <= 135)
            return new Vector3(1, 0, 0);
        else if (yRotation >= -135 && yRotation <= -45)
            return new Vector3(-1, 0, 0);
        else
            return new Vector3(0, 0, -1);
    }
}
