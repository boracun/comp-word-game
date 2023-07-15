using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;

    private int _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void IncreaseScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = _score.ToString();
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.text = _score.ToString();
    }
}
