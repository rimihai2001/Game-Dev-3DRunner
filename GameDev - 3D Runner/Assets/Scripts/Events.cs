using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    /*
     * PLAY GAME FUNCTION
     * function used for the PlayButton in the "Main Menu"
     */
    public void PlayGame()
    {
        // changes the value of the 'gameStart' variable which will trigger a set of actions in PlayerManager and PlayerMovement
        PlayerManager.gameStart = true;
    }

    /*
     * REPLAY GAME FUNCTION
     * function used for the ReplayButton in the "GameOver" scene
     */
    public void ReplayGame()
    {
        // Function that loads the game scene again
        SceneManager.LoadScene("3DRunner");
        // changes the value of the 'gameStart' variable which will trigger a set of actions in PlayerManager and PlayerMovement
        PlayerManager.gameStart = true;
        // triggers the refresh of the top player position in order to display it's name and score during the game
        PlayerManager.needTopPlayer = true;
    }

    /*
     * MAIN MENU FUNCTION
     * function used for the MainMenuButton in the "GameOver" scene
     */
    public void MainMenu()
    {
        // Function that loads the game scene again
        SceneManager.LoadScene("3DRunner");
        // changes the value of the 'gameStart' variabile in order for the game to not start instantly after the scene starts again
        PlayerManager.gameStart = false;
        // triggers the refresh of the top player position in order to display it's name and score during the game
        PlayerManager.needTopPlayer = true;
    }

    /*
     * BACK BUTTON FUNCTION
     * function used for the BackButton in the "Leaderboard" scene
     */
    public void BackButton()
    {
        // triggers the activation of 'whenMainMenu' function in PlayerManager which hides the leaderboard and shows the
        // GameStartPanel again
        PlayerManager.isMainMenu = true;
    }

    /*
     * SHOW LEADERBOARD TABLE FUNCTION
     * function used for the LeaderboardButton in the "Main Menu"
     */
    public void ShowLeaderboardTable()
    {
        // triggers the activation of the 'whenLeaderboard' function 
        // along with the activation of the darker background and the back button and it also hides the elements of
        // the GameStartPanel
        PlayerManager.isLeaderboard = true;
    }

    /*
     * QUIT GAME FUNCTION
     * function used to quit the game
     */
    public void QuitGame()
    {
        // Function used for quiting the game
        Application.Quit();
    }
}
