using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // Variable used to determine if the game is over or not.
    public static bool gameOver;
    public static bool gameStart;
    // Variable used for the GameOverPanel
    public GameObject gameOverPanel;
    public GameObject gameStartPanel;

    void Start()
    {
        // Initializing the bool variable with false
        gameOver = false;
        gameStart = true;
    }

//     // Update is called once per frame
    void Update()
    {
        // If the game is over (true) the time stops and the GameOverPanel becomes active
        if(gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if(!gameStart)
        {
            gameStartPanel.SetActive(false);
        }
    }
}