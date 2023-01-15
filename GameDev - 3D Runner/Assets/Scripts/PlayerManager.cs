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
    // Variable used to link the script to GameOverPanel
    public GameObject gameOverPanel;
    // Variable used to link the script to GameStartPanel
    public GameObject gameStartPanel;
    // Variable used to link the script to LeaderboardTable
    public GameObject leaderboardTable;
    // Variable used to link the script to the Background used when the LeaderboardButton in the Main Menu is pressed
    public GameObject backgroundMenu;
    // Variable used to link the script to LeaderboardPlayerInputWindow
    public GameObject leaderboardPlayerNameInputWindow;
    // Variable used to link the script to InGameText object
    public GameObject inGameText;
    // Variable used to link the script to the BackButton used when the LeaderboardButton in the MainMenu is pressed
    public GameObject backButton;
    // Variable used to link the script to TopPlayer text object
    public Text topPlayer;
    // Variable used as a trigger to hide the Leaderboard screen and show the main menu screen
    public static bool isMainMenu = false;
    // Variable used as a trigger to show the Leaderboard screen
    public static bool isLeaderboard = false;
    // Variable used as a trigger to show the InGameText object
    public static bool isInGameText = false;
    // variable used to determine if the gameOverScreen is being displayed
    public bool showGameOverScreen = true;
    // variable used as a trigger to refresh the Top Player UI element
    public static bool needTopPlayer = true;


    /*
     * START FUNCTION
     */
    void Start()
    {
        // Initializing the bool variable with false because the game is not over
        gameOver = false;
        // Setting the time equal to 1 for when scenes are loaded (at Start or Replay)
        Time.timeScale = 1;
    }
 
    /*
     * UPDATE FUNCTION
     */
    void Update()
    {
       
        // If the game started (gameStart = true) => the time starts and the GameStartPanel becomes inactive
        if (gameStart)
        { 
            // disables the panel
            gameStartPanel.SetActive(false);

            // check if there is a need to refresh the TopPlayer UI (it only happens at the beginning of the game)
            if (needTopPlayer)
            {
                // changes the value to false so the check only happens at the beginning of the game
                needTopPlayer = false;
                // triggers the function which loads the leaderboard entries and selects the top player in the leaderboard
                // which is then displayed in the TopPlayer text object
                CheckTopPlayer();
            }
        }

        // If the gameStartPanel is activated or the leaderboardTable screen is activated =>
        // the InGameText object, which represents the UI elements displayed during while the game is playing,
        // is not displayed
        // otherwise, it is displayed
        if(gameStartPanel.activeSelf || leaderboardTable.activeSelf)
        {
            inGameText.SetActive(false);
        }
        else
        {
            inGameText.SetActive(true);
        }

        // If the game is over (gameOver = true) AND the GameOverScreen is should be displyed,
        // the time stops and the GameOverPanel becomes active along with the leaderboard and new score entry
        if (gameOver && showGameOverScreen)
        {
            whenGameIsOver();
            showGameOverScreen = false;
        }

        // If isMainMenu = true => hides leaderboard screen and shows GameStartPanel
        if (isMainMenu)
        {
            whenMainMenu();
            isMainMenu = false;
        }

        // if isLeaderboard = true => shows the leaderboard screen and Back button and hides the GameStartPanel 
        if (isLeaderboard)
        {
            leaderboardTable.SetActive(true);
            isLeaderboard = false;
            gameStartPanel.SetActive(false);
            backgroundMenu.SetActive(true);
            backButton.SetActive(true);
        }
    }

    /*
     * WHEN GAME IS OVER FUNCTION
     * triggers when the game is over and the GameOverPanel should become active
     */
    public void whenGameIsOver()
    {
        // the GameOverPanel becomes active
        gameOverPanel.SetActive(true);
        // Setting the time as 0
        Time.timeScale = 0;
        // Loads the reached score from the Score Script
        int finalScore = ScoreScript.scoreValueStatic;
        // Shows the leaderboard
        leaderboardTable.SetActive(true);
        // Shows the new entry window
        leaderboardPlayerNameInputWindow.SetActive(true);
        // Saves the new entry window
        LeaderboardPlayerNameInputWindow.GetInput(finalScore, (string name) => {
            LeaderboardTable.AddLeaderboardEntry(finalScore, name);
        });
    }

    /*
     * WHEN MAIN MENU FUNCTION
     * triggers when the game is in the leaderboard screen and the back button is pressed to go back to the MainMenu screen
     */
    public void whenMainMenu()
    {
        // hides the leaderboard screen
        leaderboardTable.SetActive(false);
        // hides the backgroundd
        backgroundMenu.SetActive(false);
        // hides the Back Button 
        backButton.SetActive(false);
        // shows the GameStartPanel
        gameStartPanel.SetActive(true);
    }
   
    /*
     * CHECK TOP PLAYER
     * triggers at the beginning of the game after you click on PlayGameButton or PlayAgainButton
     */
    private void CheckTopPlayer()
    {
        // checks if the inGameText is activated
        if (inGameText.activeSelf)
        {
            // initializes a new leaderboard and loads the information from the JSON file
            Leaderboard leaderboard = Leaderboard.Load();
            // check if the list of entries is empty
            if (leaderboard.leaderboardEntryList.Count != 0)
            {
                // saved the current top player
                LeaderboardEntry topPlayerEntry = leaderboard.leaderboardEntryList[0];
                // changes the TopPLayer text into the top player of the leaderboard
                topPlayer.text = "Top Player: \n" + topPlayerEntry.name + "\n" + topPlayerEntry.yearReached;
            }
            else
            {
                // if the leaderboard is empty => displays nothing
                topPlayer.text = "";
            }
        }
        else 
        { 
            // if the inGameText is not activated yet, change the value of needTopPlayer in order for the check 
            // for the top player to happen again when it becomes active
            needTopPlayer = true; 
        }
    }



}