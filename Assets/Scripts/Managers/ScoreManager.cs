using System;
using System.Collections.Generic;
using System.IO;
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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
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
        File.AppendAllLines(Application.persistentDataPath + "/scores.txt", new []{ DateTime.Now + "***" + _score });
    }
}
