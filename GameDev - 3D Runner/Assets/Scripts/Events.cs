using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        // Function that loads the game scene again
        SceneManager.LoadScene("3DRunner");
    }

    public void QuitGame()
    {
        // Function used for quiting the game
        Application.Quit();
    }
}
