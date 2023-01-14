using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

   

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // rotates the coin by 90 degrees along the z-axis every second
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        // Check that the object we collided with is the player
        // if the object we collided with is not the player, we exit the function
        if (other.gameObject.name != "Player")
        {
            return;
        }
        else
        {
            Destroy(gameObject);

            // Add to the player's score
            ScoreScript.inst.collectiblesBonus += 10;
        }
        // Destroy the coin object
        Destroy(gameObject);
        
    }
}
