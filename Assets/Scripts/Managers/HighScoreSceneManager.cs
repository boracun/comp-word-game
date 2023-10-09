using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class HighScoreSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject highScoreBackgroundPrefab;
    [SerializeField] private GameObject fileNotFoundPrefab;
    private string _highScoresPath;

    private void Awake()
    {
        _highScoresPath = Application.persistentDataPath + "/highScores.txt";
        if (!File.Exists(_highScoresPath))
        {
            Instantiate(fileNotFoundPrefab, transform.position, Quaternion.identity, transform);
            return;
        }

        string containerJson = SpecialItemManager.EncryptDecrypt(File.ReadAllText(_highScoresPath));
        HighScoreContainer container = JsonUtility.FromJson<HighScoreContainer>(containerJson);

        foreach (HighScore highScore in container.HighScores)
        {
            GameObject scoreGO = Instantiate(highScoreBackgroundPrefab, Vector3.zero, Quaternion.identity, transform);
            int score = highScore.score;
            string date = highScore.timeString.Substring(0, 16);

            scoreGO.GetComponentInChildren<TextMeshProUGUI>().text = score + " at " + date;
        }
    }
}
