using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    //Variable to link the script to the position of the player
    public Transform player;
    //Variable to link the script to the text of the UI
    public Text scoreText;
    //Variable to store the current score value
    private int scoreValue;
    //Variable to store the static value of the score
    public static int scoreValueStatic;
    // Variable to store the number of collected coins
    public int collectiblesBonus = 0;
    //An instance to the Score Script
    public static ScoreScript inst;

    //Create an instance of the script
    private void Awake()
    {
        inst = this;
    }

    //Function that updates the score regarding to the Z position of the player
    void Update()
    {
        //Every 100 distance on the Z axes increases the current year from the score, on which we add the collectibles score
        scoreValue = 100 + (int)player.position.z / 5 + collectiblesBonus;
        scoreValueStatic = scoreValue;

        //Different text if it's game over or not
        //ToString("0") method is used to only take the integer out from the number
        if(PlayerManager.gameOver == false)
        { 
            scoreText.text = "Current Year: \n" + scoreValue.ToString("0");
        }
        else {
            scoreText.text = "Year Reached: \n" + scoreValue.ToString("0");
        }
    }
}
