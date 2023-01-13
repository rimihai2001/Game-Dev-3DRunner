using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Variable that makes the connection with the Player Movement script
    public PlayerMovement pm;
    //Variable to link the script to the position of the player
    public Transform player;

    public AudioSource deathSound;
    public AudioSource BGMusic;

    //God Mode for testing
    public bool testMode = false;

    void FixedUpdate()
    {
        if (player.position.y < -10)
        {
            pm.enabled = false;
            // change value of gameOver bool to True in order for the GameOverPanel to be visible
            PlayerManager.gameOver = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && testMode == false)
        {
            testMode = true;
          
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) && testMode == true)
        {
            testMode = false;
            
        }
    }


    //Function that detects when the player enter in a collision with an obstacle
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(!testMode)
        {
            //If statement that disables the movement and stops the game if the player collides into a "GameOverObstacle" tag object
            if (collisionInfo.gameObject.tag == "GameOverObstacle" && testMode == false)
            {
                BGMusic.Stop();
                deathSound.Play();
                pm.enabled = false;
                // change value of gameOver bool to True in order for the GameOverPanel to be visible
                PlayerManager.gameOver = true;
            }
        }
        
    }
}
