using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 5, rotationSpeed = 2;

    Rigidbody rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Physics calculations
    void FixedUpdate () {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * movementSpeed * Time.deltaTime;
        float yRotation = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3(0, yRotation, 0) * rotationSpeed;
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement = transform.forward * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            movement = -transform.forward * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            movement = transform.right * movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            movement = -transform.right * movementSpeed * Time.deltaTime;        
        
        rb.MoveRotation(Quaternion.Euler(rb.rotation.eulerAngles + rotation));
        rb.MovePosition(transform.position + movement);           
    }
}
