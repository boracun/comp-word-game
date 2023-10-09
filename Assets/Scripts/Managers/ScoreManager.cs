using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using SpecialItems;
using TMPro;
using UnityEngine;

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
        File.AppendAllLines(_scoresPath, new []{ DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("tr")) + "***" + _score });
        
        string[] highScoreStrings;
        
        if (File.Exists(_highScoresPath))
            highScoreStrings = File.ReadAllLines(_highScoresPath);
        else
            highScoreStrings = new string[] { };

        if (highScoreStrings.Length >= 10 &&
            int.Parse(highScoreStrings[9].Substring(highScoreStrings[9].IndexOf('*') + 1)) >= _score)
            return;

        Dictionary<string, int> highScoreDict = new Dictionary<string, int>();

        foreach (string scoreString in highScoreStrings)
        {
            highScoreDict.Add(scoreString.Substring(0, scoreString.IndexOf('*')),
                int.Parse(scoreString.Substring(scoreString.IndexOf('*') + 1)));
        }

        highScoreDict.Add(
            DateTime.Now.Day < 10
                ? "0" + DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("tr"))
                : DateTime.Now.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("tr")), _score);

        int index = 0;

        List<string> resultList = new List<string>();
        foreach (var pair in highScoreDict.OrderByDescending(key => key.Value))
        {
            resultList.Add(pair.Key + "*" + pair.Value);
            index++;
            if (index == 10)
                break;
        }
        
        File.WriteAllLines(_highScoresPath, resultList);
    }
}
