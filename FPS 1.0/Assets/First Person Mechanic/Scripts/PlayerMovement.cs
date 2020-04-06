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
    // [HideInInspector]
    public float walkDeaccelerateX;
    // [HideInInspector]
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
        // Makes a separate vector for horizontal movement
        horizontalMovement = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.z);
        // If movement speed exceeds the maximum set, the following condition will make the speed equal to the maximum
        // by making the magnitude to 1 then multipying it by the maximum
        if (horizontalMovement.magnitude > maxWalkSpeed){
            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement *= maxWalkSpeed;
        }
        // Sets the velocity (RigidBody) using horizontal movement for x and z coordinates and the player's RigidBody component
        // to get the y axis
        playerRigidBody.velocity = new Vector3(horizontalMovement.x, playerRigidBody.velocity.y, horizontalMovement.y);
        // Gets the rotation from the MouseLook script attached to the camera
        transform.rotation = Quaternion.Euler(0, cameraObject.GetComponent<MouseLook>().currentY, 0);
        // If player is grounded, accelerates the player through keys by adding force but since the player is grounded,
        // the y component is zero
        if (isGrounded){
            playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration);
        }
        // Player can still move but velocity can be slowed/sped depending on the walkAccelerationRatio
        else{
            playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration *walkAccelerationRatio,
            0, Input.GetAxis("Vertical") * walkAccelerationRatio * acceleration);
        }
        // Code for stopping the player. This is the use for the separate float and Vector2.
        // It damps the velocity through other variables before assigning it.
        if (isGrounded)
        {
            float xMove = Mathf.SmoothDamp(playerRigidBody.velocity.x, 0, ref walkDeaccelerateX, deaccelerate);
            float zMove = Mathf.SmoothDamp(playerRigidBody.velocity.z, 0, ref walkDeaccelerateZ, deaccelerate);
            playerRigidBody.velocity = new Vector3(xMove, playerRigidBody.velocity.y, zMove);        
        }

    }
    // Turns isGrounded to true when collision is made
    void OnCollisionEnter (Collision coll)
    {
        foreach (ContactPoint contact in coll.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
            {
                isGrounded = true;
            }
        }
    }
    // Turns isGrounded to false after collision
    void OnCollisionExit(Collision coll){
        if (coll.gameObject.name.Equals("Plane"))
        {
            isGrounded = false;   
        }
    }
}