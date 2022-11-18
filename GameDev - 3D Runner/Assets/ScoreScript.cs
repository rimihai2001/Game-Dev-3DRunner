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

    //Function that updates the score regarding to the Z position of the player
    void Update()
    {
        scoreValue = 100 + (int)player.position.z / 100;
        scoreText.text = "Current Year: \n" + scoreValue.ToString("0");
    }
}
