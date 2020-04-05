using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public GameObject cameraObject;
    public float acceleration;
    public float walkAccelerationRatio;

    public float maxWalkSpeed;
    public float deaccelerate = 2;
    [HideInInspector]
    public Vector2 horizontalMovement;
    [HideInInspector]
    public float walkDeaccelerateX;
    [HideInInspector]
    public float walkDeaccelerateZ;

    [HideInInspector]
    public bool isGrounded = true;
    Rigidbody playerRigidBody;
    public float jumpVelocity = 20;
    float maxSlope = 45;

    void Awake(){
        // Getting the rigidbody component from player
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Update(){
        Jump();
        Move();
    }

    void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            playerRigidBody.AddForce(0, jumpVelocity, 0);
        }
    }

    void Move(){
        // Controlling the limit of the player by measuring the
        // Vector3 magnitude and then measuring and normalizing vector

        horizontalMovement = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.z);

        if (horizontalMovement.magnitude > maxWalkSpeed){
            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement *= maxWalkSpeed;
        }
        // Controlling the only X and Z speed of the cube movement
        playerRigidBody.velocity = new Vector3(horizontalMovement.x, playerRigidBody.velocity.y, horizontalMovement.y);
        // Rotating the player capsule according to MouseLook current Y variable so that player looks
        // exactly where the camera is looking.
        transform.rotation = Quaternion.Euler(0, cameraObject.GetComponent<MouseLook>().currentY, 0);
        // Moving here
        if (isGrounded){ // Complete control while player is on ground
            playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration);
        }
        else{ // Complete control while player is in air
            playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration *walkAccelerationRatio,
            0, Input.GetAxis("Vertical") * walkAccelerationRatio * acceleration);
        } 

        if (isGrounded) // This section of code adds friction to player's movement so that it does not slip when no force is apllied
        {
            float xMove = Mathf.SmoothDamp(playerRigidBody.velocity.x, 0, ref walkDeaccelerateX, deaccelerate);
            float zMove = Mathf.SmoothDamp(playerRigidBody.velocity.z, 0, ref walkDeaccelerateZ, deaccelerate);
            playerRigidBody.velocity = new Vector3(xMove, playerRigidBody.velocity.y, zMove);        
        }

    }

    void OnCollisionEnter (Collision coll)
    {
        foreach (ContactPoint contact in coll.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope) // Detecting
            {
                isGrounded = true;
            }
        }
    }

    //Making player state in the air
    void OnCollisionExit(Collision coll){
        if (coll.gameObject.name.Equals("Plane"))
        {
            isGrounded = false;   
        }
    }
}