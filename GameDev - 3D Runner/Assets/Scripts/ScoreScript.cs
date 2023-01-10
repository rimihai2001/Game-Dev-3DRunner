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

    //Function that updates the score regarding to the Z position of the player
    void Update()
    {
        //Every 100 distance on the Z axes increases the current year from the score
        scoreValue = 100 + (int)player.position.z / 10;
        scoreValueStatic = scoreValue;
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
