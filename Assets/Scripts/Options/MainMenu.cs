using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void DisplayHighScores()
    {
        SceneManager.LoadScene("HighScoresScene");
    }
}
