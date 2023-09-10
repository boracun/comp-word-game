using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject pointPrefab;

    private int _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void IncreaseScore(int scoreToAdd, int wordLength, Vector3 pointIncreasePosition)
    {
        _score += scoreToAdd + wordLength;
        scoreText.text = _score.ToString();
        DisplayScoreIncrease(scoreToAdd, wordLength, pointIncreasePosition);
        animator.SetTrigger("Score Up");
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
}
