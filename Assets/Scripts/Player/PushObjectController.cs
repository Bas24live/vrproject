using UnityEngine;
using System.Collections;

public class PushObjectController : MonoBehaviour {
    
    public float distance = 2f;
    public float throwForce = 20f;

    GameObject pushedObject;
    Rigidbody rb;
    bool pushing = false;
    bool xDirection = true;
    Vector3 pDirection;
    Vector3 oDirection;


    void Update() {
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
                rb = pushedObject.GetComponent<Rigidbody>();
                xDirection = getDirection();              
                pushing = true;
                rb.mass = 2;
            }
        }
    }

    void Pushing(GameObject o) {
        Vector3 pPos = transform.position;
        Vector3 oPos = o.transform.position;

        if (xDirection)
            transform.position = new Vector3(pPos.x, pPos.y, oPos.z);
        else
            transform.position = new Vector3(oPos.x, pPos.y, pPos.z);

        o.transform.position = pPos + transform.forward * distance;

        if (xDirection)
            o.transform.position = new Vector3(o.transform.position.x, oPos.y, oPos.z);
        else
            o.transform.position = new Vector3(oPos.x, oPos.y, o.transform.position.z);
    }

    void Leave() {
        pushing = false;
        pushedObject = null;
        rb.mass = 1;
    }

    bool getDirection() {
        float yRotation = transform.rotation.y;
         
        if (yRotation >= .38 && yRotation <= .93 || yRotation >= -.93 && yRotation <= -.38) 
            return true;
        else
            return false;
        
    }
}
