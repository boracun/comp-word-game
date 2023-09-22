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
        if (!TimeManager.Instance.IsPaused)
            ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        if (!TimeManager.Instance.IsPaused)
            ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene("MainScene");
    }
}
