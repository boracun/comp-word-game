using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUIGO;
    
    public void OpenPauseMenu()
    {
        pauseMenuUIGO.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenuUIGO.SetActive(false);
    }

    public void Quit()
    {
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene("MainScene");
    }
}
