using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody variable to be assigned to an object
    public Rigidbody rb;

    //Varibles declared public so they can be changed from the Unity frontend instead of changing directly into the code
    public float forwardForce = 1000f;
    public float sidewayForce = 500f;
    public float jumpForce = 100f;
    private bool playerOnGround = true;
    


    //Update function that is called once per frame
    void FixedUpdate()
    {
        //Constant forward force activated per frame
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        
        //Right force that is activated per frame when the user is pressing the "D" key
        if(Input.GetKey("d"))
        {
            rb.AddForce(sidewayForce * Time.deltaTime, 0, 0);
        }

        //Left force that is activated per frame when the user is pressing the "A" key
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewayForce * Time.deltaTime, 0, 0);
        }

        //Jump activated by "W" key if the player is on the ground
        if (Input.GetKey("w") && playerOnGround)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    //Function that detects if the player is on the ground and changes the variable value to true
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerOnGround = true;
        }
    }

    //Function that detects if the player is not on the ground anymore and changes the variable accordingly
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerOnGround = false;
        }
    }

}
