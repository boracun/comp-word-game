using SpecialItems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void DisplayHighScores()
    {
        SceneManager.LoadScene("HighScoresScene");
    }

    public void OpenTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GiveItem(int itemIndex)
    {
        SpecialItemManager.Instance.GiveItem((SpecialItem) itemIndex);
        rewardPanel.SetActive(false);
    }

    public void ActivateRewardPanel()
    {
        rewardPanel.SetActive(true);
    }
}
