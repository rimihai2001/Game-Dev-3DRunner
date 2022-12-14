using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // Variable used to determine if the game is over or not.
    public static bool gameOver;
    // Variable used to determine if the game started or not.
    public static bool gameStart;
    // Variable used for the GameOverPanel
    public GameObject gameOverPanel;
    // Variable used for the GameStartPanel
    public GameObject gameStartPanel;

    public bool showGameOverScreen = true;

    void Start()
    {
        // Initializing the bool variable with false
        gameOver = false;
        // Initializing the bool variable with false
        gameStart = false;
        // Setting the time equal to 1 for when scenes are loaded (at Start or Replay)
        Time.timeScale = 1;
    }
 
//     // Update is called once per frame
    void Update()
    {
        // If the game is over (true) the time stops and the GameOverPanel becomes active
        if (gameOver && showGameOverScreen)
        {
            showGameOverScreen = false;
            whenGameIsOver();


        }
        // If the game started (true) the time stops and the GameOverPanel becomes active
        if(gameStart)
        {
            gameStartPanel.SetActive(false);
        }
    }

    public void whenGameIsOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        int finalScore = ScoreScript.scoreValueStatic;
        LeaderboardPlayerNameInputWindow.Show(finalScore, (string name) => {
            LeaderboardTable.AddLeaderboardEntry(finalScore, name);
        });
    }
}