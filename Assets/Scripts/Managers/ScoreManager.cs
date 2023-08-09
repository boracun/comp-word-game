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

    public void IncreaseScore(int scoreToAdd, Vector3 pointIncreasePosition)
    {
        _score += scoreToAdd;
        scoreText.text = _score.ToString();
        DisplayScoreIncrease(scoreToAdd, pointIncreasePosition);
        animator.SetTrigger("Score Up");
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.text = _score.ToString();
    }

    private void DisplayScoreIncrease(int scoreToAdd, Vector3 position)
    {
        GameObject pointGO = Instantiate(pointPrefab, position, Quaternion.identity);
        pointGO.GetComponent<TextMeshProUGUI>().text = "+" + scoreToAdd;
        pointGO.transform.SetParent(transform.root);
        pointGO.transform.SetAsLastSibling();
        pointGO.transform.localScale = Vector3.one;
    }
}
