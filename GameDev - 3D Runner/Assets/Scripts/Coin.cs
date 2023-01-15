using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Rotation speed
    public float turnSpeed = 90f;
  

    // Update is called once per frame
    void Update()
    {
        // rotates the coin by 90 degrees along the z-axis every second
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the coin spawns inside an obstacle, the obstacle is destroyed
        if (other.gameObject.tag == "GameOverObstacle")
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with is the player
        // if the object we collided with is not the player, we exit the function
        if (other.gameObject.name != "Player")
        {
            return;
        }
       
        //If the player hits the coin
        //Destroy coin
        Destroy(gameObject);

        // Add to the player's score
        ScoreScript.inst.collectiblesBonus += 25;
        
    }
}
