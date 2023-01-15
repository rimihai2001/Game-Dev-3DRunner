using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Variable that makes the connection with the Player Movement script
    public PlayerMovement pm;
    //Variable to link the script to the position of the player
    public Transform player;

    //Audio sources for Background Music and 
    public AudioSource deathSound;
    public AudioSource BGMusic;
    public AudioSource coinSound;

    //A game object refferenced to a message that appears on test mode
    public GameObject testModePanel;

    //Variable for test mode
    private bool testMode = false;



    void FixedUpdate()
    {
        //If the player if under the platform it's game over
        if (player.position.y < -10)
        {
            //The movement is disabled
            pm.enabled = false;
            // change value of gameOver bool to True in order for the GameOverPanel to be visible
            PlayerManager.gameOver = true;
        }
    }

    void Update()
    {
        //If = and - are pressed at the same time durint the game, the test mode is activated/deactivated
        if (Input.GetKeyDown(KeyCode.Equals) && Input.GetKeyDown(KeyCode.Minus) && PlayerManager.gameOver == false)
        {
            //If the game is not in test mode
            if (!testMode)
            {
                //Test mode is activated
                testMode = true;
                //A special message is now visible
                testModePanel.SetActive(true);
            }
            else
            {
                //Test mode is deactivated
                testMode = false;
                //The message is now hidden 
                testModePanel.SetActive(false);
            }

        }

    }


    //Function that detects when the player enter in a collision with an obstacle
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (!testMode)
        {
            //If statement that disables the movement and stops the game if the player collides into a "GameOverObstacle" tag object
            if (collisionInfo.gameObject.tag == "GameOverObstacle")
            {
                //Background music stops
                BGMusic.Stop();
                //The death sound is played
                deathSound.Play();
                //The player movement is disabled
                pm.enabled = false;
                //Change value of gameOver bool to True in order for the GameOverPanel to be visible
                PlayerManager.gameOver = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player collides with a coin
        if (other.gameObject.tag == "GoldCoin")
        {
            //The cond sound plays
            coinSound.Play();
         }

    }
}
