using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class WordSubmission : MonoBehaviour
{
    private List<string> _validWordList;
    [SerializeField] private TextAsset wordListAsset;
    
    private void Awake()
    {
        _validWordList = wordListAsset.text.Split('\n').ToList();
    }

    public void SubmitWord()
    {
        int wordPoints = 0;
        string wordString = "";

        foreach (Transform cellTransform in transform)
        {
            if (cellTransform.childCount == 0)
                continue;

            LetterData letterData = cellTransform.GetChild(0).GetComponent<Letter>().LetterData;

            wordPoints += letterData.points;
            wordString += letterData.letter;
        }

        if (wordString.Length < 3 || !IsValid(wordString.ToLower(new CultureInfo("en-US"))))
            return;

        ScoreManager.Instance.IncreaseScore(wordPoints, wordString.Length, transform.position);
        SpaceManager.Instance.ResetWordContainer();
        EmptyCellManager.Instance.FillAllEmptyCells();
    }

    /**
     * Binary Search
     */
    private bool IsValid(string submission)
    {
        if (submission.Contains('.'))
            return IsValidWithWildLetters(submission);
        
        int high = _validWordList.Count - 1;
        int low = 0;
        int mid = high / 2;

        while (low <= high)
        {
            string midWord = _validWordList[mid];

            switch (midWord.CompareTo(submission))
            {
                // Word found
                case 0:
                    return true;
                // Check right
                case -1:
                    low = mid + 1;
                    break;
                // Check left
                case 1:
                    high = mid - 1;
                    break;
            }
            
            mid = (high + low) / 2;
        }

        return false;
    }

    private bool IsValidWithWildLetters(string submission)
    {
        Regex regex = new Regex("^" + submission + "$");
        return _validWordList.Count(s => regex.IsMatch(s)) > 0;
    }
}
