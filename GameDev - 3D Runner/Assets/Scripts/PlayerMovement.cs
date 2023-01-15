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

    //The variable that stores the position of the player
    float xPosition;
    //The current lane of the player
    private int currentLane = 2;
    //Varible that increases speed
    private float gainSpeed = 0;
    //Bool variable that says if the player is or is not touching the ground surface
    private bool playerOnGround = false;

    public AudioSource jumpSound;

    void Start()
    {
        //The variable takes player position
        xPosition = rb.position.x;
    }

    void FixedUpdate()
    {
        //The gravity is added
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
        // Jump activated by "W", left arrow or "SPACE" key if the player is on the ground
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && playerOnGround)
        {
            // play the JUMP sound
            jumpSound.Play();
            //
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            //The player is not on the ground anymore
            playerOnGround = false;
            // if the players press W or SPACE while they are in the main menu => the game will start
            checkGameStart();
        }

        //The player moves to the selected lane
        Vector3 pos = rb.position;
        pos.x = Mathf.MoveTowards(pos.x, xPosition, sidewayForce * Time.deltaTime);
        rb.position = pos;

        // When pressing D or right arrow key the player moves to the next lane on the right if possible
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < 3)
        {
            //If the game has started
            if (PlayerManager.gameStart == true)
            {
                // move to the right lane
                xPosition += 11;
                currentLane++;
            }
            // if the players press D while they are in the main menu => the game will start
            checkGameStart();
        }
        //When pressing A or left arrow key the player moves to the next lane on the left if possible
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 1)
        {
            //If the game has started
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