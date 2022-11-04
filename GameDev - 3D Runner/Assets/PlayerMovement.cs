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
    }
}
