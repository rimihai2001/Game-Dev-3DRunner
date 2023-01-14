using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerManager.gameStart = true;
    }

    public void ReplayGame()
    {
        // Function that loads the game scene again
        SceneManager.LoadScene("3DRunner");
        PlayerManager.gameStart = true;
        PlayerManager.needTopPlayer = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("3DRunner");
        PlayerManager.gameStart = false;
    }

    public void BackButton()
    {
        PlayerManager.isMainMenu = true;
    }

    public void ShowLeaderboardTable()
    {
        // Function that loads the game scene again
        PlayerManager.isLeaderboard = true;
    }

    public void QuitGame()
    {
        // Function used for quiting the game
        Application.Quit();
    }
}
