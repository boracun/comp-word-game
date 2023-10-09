using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using SpecialItems;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private TextMeshProUGUI finalScoreText;
 
    private int _score;
    private List<int> _highScoreList;
    private string _scoresPath;
    private string _highScoresPath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _scoresPath = Application.persistentDataPath + "/scores.txt";
        _highScoresPath = Application.persistentDataPath + "/highScores.txt";
    }

    public void IncreaseScore(int scoreToAdd, int wordLength, Vector3 pointIncreasePosition)
    {
        if (SpecialItemManager.Instance.IsInUse(SpecialItem.Multiplier2Item))
        {
            scoreToAdd *= 2;
            wordLength *= 2;
        }
        
        _score += scoreToAdd + wordLength;
        scoreText.text = _score.ToString();
        DisplayScoreIncrease(scoreToAdd, wordLength, pointIncreasePosition);
        animator.SetTrigger("Score Up");
        finalScoreText.text = "Score: " + _score;
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.text = _score.ToString();
    }

    private void DisplayScoreIncrease(int scoreToAdd, int wordLength, Vector3 position)
    {
        GameObject pointGO = Instantiate(pointPrefab, position, Quaternion.identity);
        pointGO.GetComponent<TextMeshProUGUI>().text = "+" + scoreToAdd + " letter score \n+" + wordLength + " length score";
        pointGO.transform.SetParent(transform.root);
        pointGO.transform.SetAsLastSibling();
        pointGO.transform.localScale = Vector3.one;
    }

    public void SaveScore()
    {
        HighScore scoreObject = new HighScore(DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("tr")), _score);
        
        string json = JsonUtility.ToJson(scoreObject);
        string encryptedJson = SpecialItemManager.EncryptDecrypt(json);
        
        File.AppendAllLines(_scoresPath, new []{ encryptedJson });      // Append encrypted line
        
        HighScoreContainer highScoreContainer;
        
        if (File.Exists(_highScoresPath))
        {
            string decryptedContainer = SpecialItemManager.EncryptDecrypt(File.ReadAllText(_highScoresPath));   // Decrypt the container object
            highScoreContainer = JsonUtility.FromJson<HighScoreContainer>(decryptedContainer);
        }
        else
            highScoreContainer = new HighScoreContainer(Array.Empty<HighScore>());

        if (highScoreContainer.HighScores.Length >= 10 && highScoreContainer.HighScores[9].score >= _score)
            return;

        Dictionary<string, int> highScoreDict = new Dictionary<string, int>();

        foreach (HighScore highScore in highScoreContainer.HighScores)
        {
            highScoreDict.Add(highScore.timeString, highScore.score);
        }

        highScoreDict.Add(
            DateTime.Now.Day < 10
                ? "0" + DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("tr"))
                : DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("tr")), _score);

        int index = 0;

        List<HighScore> updatedHighScoreList = new List<HighScore>();
        foreach (var pair in highScoreDict.OrderByDescending(key => key.Value))
        {
            HighScore highScore = new HighScore(pair.Key, pair.Value);
            updatedHighScoreList.Add(highScore);
            index++;
            if (index == 10)
                break;
        }

        HighScoreContainer updatedContainer = new HighScoreContainer(updatedHighScoreList.ToArray());
        string updatedJson = JsonUtility.ToJson(updatedContainer);
        File.WriteAllText(_highScoresPath, SpecialItemManager.EncryptDecrypt(updatedJson));     // Encrypt the container object
    }
}

public struct HighScoreContainer
{
    public HighScore[] HighScores;

    public HighScoreContainer(HighScore[] highScores)
    {
        HighScores = highScores;
    }
}

[Serializable]
public struct HighScore
{
    public string timeString;
    public int score;

    public HighScore(string time, int score)
    {
        timeString = time;
        this.score = score;
    }
}
