using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{

    // Variable used to determine if the game is over or not.
    public static bool gameOver;
    // Variable used to determine if the game started or not.
    public static bool gameStart = false;
    // Variable used for the GameOverPanel
    public GameObject gameOverPanel;
    // Variable used for the GameStartPanel
    public GameObject gameStartPanel;

    public GameObject leaderboardTable;
    public GameObject backgroundMenu;

    public GameObject leaderboardPlayerNameInputWindow;

    public GameObject inGameText;

    public GameObject backButton;

    public Text topPlayer;

    public static bool isMainMenu = false;

    public static bool isLeaderboard = false;

    public static bool isInGameText = false;

    public bool showGameOverScreen = true;

    public static bool needTopPlayer = true;



    void Start()
    {
        // Initializing the bool variable with false
        gameOver = false;
        // Setting the time equal to 1 for when scenes are loaded (at Start or Replay)
        Time.timeScale = 1;
    }
 
//     // Update is called once per frame
    void Update()
    {
       
        // If the game started (true) the time stops and the GameStartPanel becomes inactive
        if (gameStart)
        { 
            gameStartPanel.SetActive(false);
            if (needTopPlayer)
            {
                needTopPlayer = false;
                CheckTopPlayer();
            }
        }

        if(gameStartPanel.activeSelf || leaderboardTable.activeSelf)
        {
            inGameText.SetActive(false);
        }
        else
        {
            inGameText.SetActive(true);
        }

        // If the game is over (true) the time stops and the GameOverPanel becomes active
        if (gameOver && showGameOverScreen)
        {
            whenGameIsOver();
            showGameOverScreen = false;
        }

        if (isMainMenu)
        {
            whenMainMenu();
            isMainMenu = false;
        }

        if (isLeaderboard)
        {
            whenLeaderboard();
            isLeaderboard = false;
            gameStartPanel.SetActive(false);
            backgroundMenu.SetActive(true);
            backButton.SetActive(true);
        }
    }

    public void whenGameIsOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        int finalScore = ScoreScript.scoreValueStatic;
        whenLeaderboard();
        leaderboardPlayerNameInputWindow.SetActive(true);
        LeaderboardPlayerNameInputWindow.Show(finalScore, (string name) => {
            LeaderboardTable.AddLeaderboardEntry(finalScore, name);
        });
    }

    public void whenMainMenu()
    {
        leaderboardTable.SetActive(false);
        backgroundMenu.SetActive(false);
        gameOverPanel.SetActive(false);
        leaderboardPlayerNameInputWindow.SetActive(false);
        gameStartPanel.SetActive(true);
        backButton.SetActive(false);
    }
   
    public void whenLeaderboard()
    {
        leaderboardTable.SetActive(true);
    }

    private void CheckTopPlayer()
    {
        if (inGameText.activeSelf)
        {
            Leaderboard leaderboard = Leaderboard.Load();
            if (leaderboard.leaderboardEntryList.Count > 0)
            {
                LeaderboardEntry topPlayerEntry = leaderboard.leaderboardEntryList[0];
                topPlayer.text = "Top Player: \n" + topPlayerEntry.name + "\n" + topPlayerEntry.yearReached;
            }
            else
            {
                topPlayer.text = "";
            }
        }
        else 
        { 
            needTopPlayer = true; 
        }
    }



}