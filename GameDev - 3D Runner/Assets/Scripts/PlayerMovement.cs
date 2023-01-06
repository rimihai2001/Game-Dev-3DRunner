using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody variable to be assigned to an object
    public Rigidbody rb;

    //Varibles declared public so they can be changed from the Unity frontend instead of changing directly into the code
    public float forwardForce = 1f;
    public float sidewayForce = 500f;
    public float jumpForce = 100f;
    public float jumpHeight = 2f;
    public float gravity = 3f;

    //The current lane of the player
    private int currentLane = 2;

    private float gainSpeed = 0;
    //Bool variable that says if the player is or is not touching the ground surface
    private bool playerOnGround = false;

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravity - 1) * rb.mass);
        //If the game has started
        if (PlayerManager.gameStart == true)
        {
            //Calculate the current speed
            Vector3 forwardSpeed = transform.forward * (forwardForce + gainSpeed / 1000) * Time.fixedDeltaTime / 10 ;
            //Constant forward force activated per frame
            rb.MovePosition(rb.position + forwardSpeed);
            //The speed increases by 0.001
            gainSpeed += 1;
            //Debug.Log(forwardForce + gainSpeed);
        }
    }

    //Update function that is called once per frame
    void Update()
    {
        //Right force that is activated per frame when the user is pressing the "D" key
        if (Input.GetKeyDown(KeyCode.D) && currentLane < 3)
        {
            if (PlayerManager.gameStart == true)
            {
                rb.MovePosition(rb.position + Vector3.right * 11);
                currentLane++;
            }
            checkGameStart();

        }

        //Left force that is activated per frame when the user is pressing the "A" key
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 1)
        {
            if (PlayerManager.gameStart == true)
            {
                rb.MovePosition(rb.position + Vector3.left * 11);
                currentLane--;
            }
            checkGameStart();

        }


        //Jump activated by "W" or"SPACE" key if the player is on the ground
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && playerOnGround)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            playerOnGround = false;
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