using UnityEngine;
using System.Collections;

public class PushObjectController : MonoBehaviour {
    
    public float distance = 2f;
    public float throwForce = 20f;

    GameObject pushedObject;
    PlayerController playerController;
    Rigidbody playerRb, pushedObjectRb;
    bool pushing = false;
    bool xDirection = true;
    Vector3 pDirection;
    Vector3 oDirection;

    void Start() {
        playerController = GetComponent<PlayerController>();
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (pushing) {
            Pushing(pushedObject);

            if (Input.GetKeyDown(KeyCode.E))
                Leave();
        }
        else
            Push();
    }

    void Push() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance) && hit.collider.CompareTag("Wall_Inner")) {
                pushedObject = hit.collider.gameObject;
                pushedObjectRb = pushedObject.GetComponent<Rigidbody>();        
                
                //pushedObjectRb.mass = 2;
                playerController.movementSpeed /= 2;
                xDirection = getDirection();
                pushing = true;
            }
        }
    }

    void Pushing(GameObject o) {
        Vector3 pPos = transform.position;
        Vector3 oPos = o.transform.position;

        if (xDirection)
            playerRb.MovePosition(new Vector3(pPos.x, pPos.y, oPos.z));
        else
            playerRb.MovePosition(new Vector3(oPos.x, pPos.y, pPos.z));

        o.transform.position = pPos + transform.forward * distance;

        if (xDirection)
            pushedObjectRb.MovePosition(new Vector3(o.transform.position.x, oPos.y, oPos.z));
        else
            pushedObjectRb.MovePosition(new Vector3(oPos.x, oPos.y, o.transform.position.z));
    }

    void Leave() {
        pushing = false;
        pushedObject = null;
        playerController.movementSpeed *= 2;
    }

    bool getDirection() {
        float yRotation = transform.rotation.eulerAngles.y;
         
        if (yRotation >= 45 && yRotation <= 135 || yRotation >= -135 && yRotation <= -45) 
            return true;
        else
            return false;
    }
}
