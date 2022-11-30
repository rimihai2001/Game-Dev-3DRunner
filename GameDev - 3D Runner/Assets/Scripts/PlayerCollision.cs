using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Variable that makes the connection with the Player Movement script
    public PlayerMovement pm;


    //Function that detects when the player enter in a collision
    void OnCollisionEnter(Collision collisionInfo)
    {
        //If statement that disables the movement and stops the game if the player collides into a "GameOverObstacle" tag object
        if (collisionInfo.gameObject.tag == "GameOverObstacle")
        {
            pm.enabled = false;
            // change value of gameOver bool to True in order for the GameOverPanel to be visible
            PlayerManager.gameOver = true;
            // Debug.Log("GAME OVER!");
        }
    }
}
