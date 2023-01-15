using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody variable to be assigned to an object
    public Rigidbody rb;

    //Varibles declared public so they can be changed from the Unity frontend instead of changing directly into the code
    public float forwardForce = 1f;
    public float sidewayForce = 1f;
    public float jumpForce = 100f;
    public float jumpHeight = 2f;
    public float gravity = 3f;
    float xPosition;
    //The current lane of the player
    private int currentLane = 2;
    private float gainSpeed = 0;
    //Bool variable that says if the player is or is not touching the ground surface
    private bool playerOnGround = false;

    public AudioSource jumpSound;

    void Start()
    {
        xPosition = rb.position.x;
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravity - 1) * rb.mass);
        // check If the game has started
        if (PlayerManager.gameStart == true)
        {
            // Calculate the current speed
            Vector3 forwardSpeed = transform.forward * (forwardForce + gainSpeed / 1000) * Time.fixedDeltaTime / 10 ;
            // Constant forward force activated per frame
            rb.MovePosition(rb.position + forwardSpeed);
            // The speed increases by 0.001
            gainSpeed += 1;
        }
    }

    //Update function that is called once per frame
    void Update()
    {
        // Jump activated by "W" or "SPACE" key if the player is on the ground
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && playerOnGround)
        {
            // play the JUMP sound
            jumpSound.Play();
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            playerOnGround = false;
            // if the players press W or SPACE while they are in the main menu => the game will start
            checkGameStart();
        }

        Vector3 pos = rb.position;
        pos.x = Mathf.MoveTowards(pos.x, xPosition, sidewayForce * Time.deltaTime);
        rb.position = pos;
        //Right force that is activated per frame when the user is pressing the "D" key
        if (Input.GetKeyDown(KeyCode.D) && currentLane < 3)
        {
            if (PlayerManager.gameStart == true)
            {
                // move to the right lane
                xPosition += 11;
                currentLane++;
            }
            // if the players press D while they are in the main menu => the game will start
            checkGameStart();
        }
        //Left force that is activated per frame when the user is pressing the "A" key
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 1)
        {
            if (PlayerManager.gameStart == true)
            {
                // move to the left lane
                xPosition -= 11;
                currentLane--;
            }
            // if the players press A while they are in the main menu => the game will start
            checkGameStart();
        }
    }

    //Function that detects when the player enter in a collision
    void OnCollisionEnter(Collision collision)
    {
        //If statement that detects if the player is on the ground and changes the variable value to true
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerOnGround = true;
        }
    }

    // Function that checks if the game started or not
    void checkGameStart()
    {
        if(PlayerManager.gameStart == false)
        {
            PlayerManager.gameStart = true;
        }
    }
}