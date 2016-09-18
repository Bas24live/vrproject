using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private float rotateRight, rotateLeft;
    public float rotationSpeed;
    public float speed;

    private int faceForward = 1, faceRight = 2, faceBackward = 3, faceLeft = 4;
    private int curDirection;
    private Vector3 curRotation = new Vector3(0, 0), movement = new Vector3(0, 0, 0);


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        curDirection = 0;

    }
	
	// Update is called once per frame
	void Update () {
    }

    // Physics calculations
    void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.W) && curDirection != faceForward)
        {
            curDirection = faceForward;
            curRotation = -curRotation;
            transform.Rotate(curRotation);
        }


        if (Input.GetKeyDown(KeyCode.D) && curDirection != faceRight)
        {
            curDirection = faceRight;
            curRotation = -curRotation;
            transform.Rotate(curRotation);
            curRotation += new Vector3(0, 90, 0);
            transform.Rotate(curRotation);
        }

        if (Input.GetKeyDown(KeyCode.S) && curDirection != faceBackward)
        {
            curDirection = faceBackward;
            curRotation = -curRotation;
            transform.Rotate(curRotation);
            curRotation += new Vector3(0, 180, 0);
            transform.Rotate(curRotation);
        }


        if (Input.GetKeyDown(KeyCode.A) && curDirection != faceLeft)
        {
            curDirection = faceLeft;
            
            curRotation = -curRotation;
            transform.Rotate(curRotation);
            curRotation += new Vector3(0, 270, 0);
            transform.Rotate(curRotation);
        }


        //transform.Translate(movement * speed);


        // float moveVertical = Input.GetAxis("Vertical");
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float rotation;

        //rb.Get

        //if (Input.GetKeyDown("D") && ) {

        //}

        //if (mouse)
        //    rotation = Input.GetAxis("Mouse X") * rotationSpeed;

        //else
        //    rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);        
        ////Rotate with keys
        //transform.Translate(movement * speed);
        //transform.Rotate(0, rotation * Time.deltaTime, 0, Space.Self);
    }
}
