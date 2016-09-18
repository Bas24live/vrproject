using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public float speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // Rotate 90 degrees clockwise
        if (Input.GetKeyDown("d"))
            transform.Rotate(0, 90, 0, Space.Self);

        // Rotate 90 degrees anticlockwise
        if (Input.GetKeyDown("a"))
            transform.Rotate(0, -90, 0, Space.Self);
       
    }

    // Physics calculations
    void FixedUpdate ()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);        

        transform.Translate(movement * speed);        
    }
}
