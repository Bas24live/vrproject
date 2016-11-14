using UnityEngine;
using System.Collections;

public class MoveObjectController : MonoBehaviour {

    public float distance = 2f;
    public float smooth = 15f;
    public float throwForce = 20f;

    GameObject carriedObject;
    Rigidbody rb;
    bool carrying = false;

    void Update() {
        if (carrying) {
            Carry(carriedObject);
            if (Input.GetKeyDown(KeyCode.Q)) 
                ThrowObject();
            
            if (Input.GetKeyDown(KeyCode.E))
                DropObject();
        }
        else {
            Pickup();
        }
    }

    void Carry(GameObject o) {
        o.transform.position =  transform.position + transform.forward * distance;//Vector3.Lerp(o.transform.position, transform.position + transform.forward * distance, Time.deltaTime * smooth);
        o.transform.rotation = transform.rotation;
    }

    void Pickup() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance) && hit.collider.CompareTag("Pickupable")) {
                carriedObject = hit.collider.gameObject;
                rb = carriedObject.GetComponent<Rigidbody>();
                rb.mass = 0.00001f;
                rb.useGravity = false;
                carrying = true;
            }
        }
    }

    void DropObject() {
        carrying = false;
        rb.useGravity = true;
        rb.mass = 1;
        carriedObject = null;
    }

    void ThrowObject() {        
        rb.velocity = transform.forward * throwForce;
        DropObject();
    }

}
