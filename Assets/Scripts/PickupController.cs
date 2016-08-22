using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

	public GameObject player;
	public GameObject carriedObject;
	public float rayDistance, floatingDistance;

	public bool carrying;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying)
			carry (carriedObject);
		else
			pickup ();
	}

	void carry(GameObject o) {
		o.transform.position = player.transform.position + player.transform.forward * floatingDistance;
	}

	void pickup() {

		if (Input.GetKeyDown == (KeyCode.E)) {

			Ray pickupRay = new Ray (player.transform.position, Vector3.forward);
			RaycastHit hit;

			if (Physics.Raycast (pickupRay, out hit, rayDistance)) {
				Pickupable p = WheelHit.collider.GetComponent<Pickupable>();
				if (p != null) {
					carrying = true;				
					carriedObject = p.gameObject;
				}
			}
		}
	}
}
