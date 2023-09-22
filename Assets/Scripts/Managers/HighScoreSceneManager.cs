using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class HighScoreSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject highScoreBackgroundPrefab;
    [SerializeField] private GameObject fileNotFoundPrefab;

    private void Awake()
    {
        if (!File.Exists(Application.persistentDataPath + "/highScores.txt"))
        {
            Instantiate(fileNotFoundPrefab, transform.position, Quaternion.identity, transform);
            return;
        }
        
        string[] highScoreStrings = File.ReadAllLines(Application.persistentDataPath + "/highScores.txt");

        foreach (string scoreString in highScoreStrings)
        {
            GameObject scoreGO = Instantiate(highScoreBackgroundPrefab, Vector3.zero, Quaternion.identity, transform);
            int score = int.Parse(scoreString.Substring(scoreString.IndexOf('*') + 1));
            string date = scoreString.Substring(0, 16);

            scoreGO.GetComponentInChildren<TextMeshProUGUI>().text = score + " at " + date;
        }
    }
}
