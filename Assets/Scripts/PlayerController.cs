using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
	public bool oldSchoolControls; //Apparently there are clunky and not fun :(
    private int faceF = 1, faceR = 2, faceB = 3, faceL = 4, curDirection, newDir;

    // Use this for initialization
    void Start () {
		curDirection = faceF;
        newDir = faceF;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (oldSchoolControls) {
			// Rotate 90 degrees clockwise
			if (Input.GetKeyDown ("d"))
				transform.Rotate (0, 90, 0, Space.Self);

			// Rotate 90 degrees anticlockwise
			if (Input.GetKeyDown ("a"))
				transform.Rotate (0, -90, 0, Space.Self);
		} else {
            if (Input.GetKeyDown(KeyCode.W))                
                newDir = faceF;

            if (Input.GetKeyDown(KeyCode.D))
                newDir = faceR;

            if (Input.GetKeyDown(KeyCode.S))
                newDir = faceB;

            if (Input.GetKeyDown(KeyCode.A)) 
                newDir = faceL;

            transform.Rotate(0, (newDir - curDirection) * 90, 0, Space.Self);
            curDirection = newDir;
        }
    }

    // Physics calculations
    void FixedUpdate ()
    {
		float moveForward;
		Vector3 movement;

		if (oldSchoolControls) {
			moveForward = Input.GetAxis ("Vertical");
			movement = new Vector3 (0.0f, 0.0f, moveForward);         
		} else {
			moveForward = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))? 1 : 0;
			movement = new Vector3 (0.0f, 0.0f, moveForward);
		}

		transform.Translate (movement * speed);               
    }
}
