using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;
	
	void Update () {
	    if(carrying) {
            carry(carriedObject);
            CheckDrop();
            CheckThrow();
        } else {
            Pickup();
        }
	}

    void rotateObject()
    {
        carriedObject.transform.Rotate(5, 10, 15);
    }

    void carry(GameObject o) {
        o.transform.position = Vector3.Lerp(o.transform.position, transform.position + transform.forward * distance, Time.deltaTime * smooth);
        o.transform.rotation = Quaternion.identity;
    }

    void Pickup()
    {
        if (Input.GetKeyDown (KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance) && hit.collider.CompareTag("Pickupable")) {
                carriedObject = hit.collider.gameObject;
                carrying = true;
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    void CheckDrop()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        carriedObject = null;
    }


    void CheckThrow() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ThrowObject();
        }
    }

    void ThrowObject() {

    }

}
