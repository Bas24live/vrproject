using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
    GameObject player;
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	    if(carrying)
        {
            carry(carriedObject);
            checkDrop();
        } else
        {
            pickup();
        }
	}

    void rotateObject()
    {
        carriedObject.transform.Rotate(5, 10, 15);
    }

    void carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, player.transform.position + player.transform.forward * distance, Time.deltaTime * smooth);
        o.transform.rotation = Quaternion.identity;
    }

    void pickup()
    {
        if(Input.GetKeyDown (KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, distance))
            {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null)
                {
                    carrying = true;
                    carriedObject = p.gameObject;
                    carriedObject.rigidbody.useGravity = false;
                }
            }
        }
    }

    void checkDrop()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
        }
    }

    void dropObject()
    {
        carrying = false;
        carriedObject.gameObject.rigidbody.useGravity = true;
        carriedObject = null;
    }

}
