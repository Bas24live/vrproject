using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5, speedDampening = 2, rotationSpeed = 1.5f;
    public float grabDistance = 2, pushDistance = 2, throwForce = 20;

    enum Direction { XPOS, XNEG, ZPOS, ZNEG };
    Direction playerDirection;

    GameObject pushedObject;
    Rigidbody playerRb, pushedObjectRb;
    Vector3 movement, velocity, rotation;

    bool pushing;
    float rotMinClamp, rotMaxClamp;

    void Awake () {
        playerRb = GetComponent<Rigidbody>();
    }
    
    void Start() {
        
        pushing = false;
        rotation = Vector3.zero;
        velocity = Vector3.zero;
        movement = Vector3.zero;
    }

    void Update () {
        velocity = Vector3.zero;

        float yAxisRotation = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3(0, yAxisRotation, 0) * rotationSpeed;

        if (Input.GetKey(KeyCode.W))
            MoveTo(transform.forward.normalized);
        if (Input.GetKey(KeyCode.S))
            MoveTo(-transform.forward.normalized);
        if (Input.GetKey(KeyCode.D))
            MoveTo(transform.right.normalized);
        if (Input.GetKey(KeyCode.A))
            MoveTo(-transform.right.normalized);

        if (pushing) {
            Pushing();
            if (Input.GetKeyDown(KeyCode.E))
                Leave();
        }
        else
            TryPush();
    }

    void FixedUpdate () {       
        playerRb.MoveRotation(Quaternion.Euler(playerRb.rotation.eulerAngles + rotation));
        playerRb.MovePosition(playerRb.position + velocity * Time.fixedDeltaTime);
    }

    //.........................................Move Player Methods.........................................//

    public void MoveTo(Vector3 direction) {     
        if (pushing)
            if (playerDirection == Direction.XPOS || playerDirection == Direction.XNEG)
                movement = new Vector3(direction.x, direction.y, 0);
            else
                movement = new Vector3(0, direction.y, direction.z);
        else
            movement = direction;

        velocity = (velocity.normalized + movement) * movementSpeed;
    }

    void Reposition() {
        float pushedPosZ = pushedObject.transform.position.z;
        float pushedPosX = pushedObject.transform.position.x;

        Vector3 newPos;

        if (playerDirection == Direction.XPOS || playerDirection == Direction.XNEG)
            newPos = new Vector3(transform.position.x, transform.position.y, pushedPosZ);
        else
            newPos = new Vector3(pushedPosX, transform.position.y, transform.position.z);

        playerRb.MovePosition(newPos);
    }

    //.........................................Push Object Methods.........................................//

    void TryPush() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance) && hit.collider.CompareTag("Wall_Inner")) {
                pushedObject = hit.collider.gameObject;
                pushedObjectRb = pushedObject.GetComponent<Rigidbody>();

                if (CheckPosition()) {
                    movementSpeed /= speedDampening;
                    SetDirection();
                    PreparePushedObject();
                    Reposition();                    
                    pushing = true;
                }
                else {
                    pushedObject = null;
                    pushedObjectRb = null;
                }
            }
            else if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance) && hit.collider.CompareTag("Switch")) {

            }
        }
    }

    void SetDirection() {
        float yRotation = transform.rotation.eulerAngles.y;

        if (yRotation >= 45 && yRotation < 135) {
            playerDirection = Direction.XPOS;            
            rotMinClamp = 45;
            rotMaxClamp = 135;
        }
        else if (yRotation >= 135 && yRotation < 225) {
            playerDirection = Direction.ZNEG;
            rotMinClamp = 135;
            rotMaxClamp = 225;
        }
        else if (yRotation >= 225 && yRotation < 315) {
            playerDirection = Direction.XNEG;
            rotMinClamp = 225;
            rotMaxClamp = 315;
        }
        else {
            playerDirection = Direction.ZPOS;
            rotMinClamp = 315;
            rotMaxClamp = 45;
        }
    }

    void Pushing() {
        Vector3 newPos;

        switch (playerDirection) { 
            case Direction.ZPOS:
                newPos = new Vector3(pushedObjectRb.position.x, pushedObjectRb.position.y, transform.position.z + pushDistance);
                break;
            case Direction.XPOS:
                newPos = new Vector3(transform.position.x + pushDistance, pushedObjectRb.position.y, pushedObjectRb.position.z);
                break;
            case Direction.ZNEG:
                newPos = new Vector3(pushedObjectRb.position.x, pushedObjectRb.position.y, transform.position.z - pushDistance);
                break;
            default:
                newPos = new Vector3(transform.position.x - pushDistance, pushedObjectRb.position.y, pushedObjectRb.position.z);
                break;
        }

        pushedObjectRb.MovePosition(newPos);
    }

    void Leave() {
        pushing = false;
        pushedObjectRb.constraints = RigidbodyConstraints.FreezeAll;
        pushedObject = null;
        pushedObjectRb = null;
        movementSpeed *= 2;
    }

    void PreparePushedObject() {

        if (playerDirection == Direction.XPOS || playerDirection == Direction.XNEG)
            pushedObjectRb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        else
            pushedObjectRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    bool CheckPosition() {
        RaycastHit hit;

        if (Physics.Raycast(pushedObject.transform.position, pushedObject.transform.forward, out hit, grabDistance) && hit.collider.CompareTag("Player"))
            return true;
        if (Physics.Raycast(pushedObject.transform.position, pushedObject.transform.right, out hit, grabDistance) && hit.collider.CompareTag("Player"))
            return true;
        if (Physics.Raycast(pushedObject.transform.position, -pushedObject.transform.forward, out hit, grabDistance) && hit.collider.CompareTag("Player"))
            return true;
        if (Physics.Raycast(pushedObject.transform.position, -pushedObject.transform.right, out hit, grabDistance) && hit.collider.CompareTag("Player"))
            return true;

        return false;
    }

    
}
