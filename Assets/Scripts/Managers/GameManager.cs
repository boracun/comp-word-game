using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private GameObject gridContainer;
    [SerializeField] private List<LetterData> letterDataList;

    public static List<LetterData> StaticLetterDataList;

    private void Awake()
    {
        StaticLetterDataList = letterDataList;
    }

    private void Start()
    {
        AssignLettersToCells();
        StartTimer();
    }

    private void AssignLettersToCells()
    {
        foreach (Transform cellTransform in gridContainer.transform)
        {
            AssignLetterToCell(cellTransform.gameObject);
        }
    }

    private void AssignLetterToCell(GameObject cellObject)
    {
        int randomIndex = Random.Range(0, letterDataList.Count);
        LetterData letterData = letterDataList[randomIndex];
        
        var letterObject = Instantiate(letterPrefab, cellObject.transform.position, cellObject.transform.rotation);
        letterObject.GetComponent<Letter>().LetterData = letterData;
        letterObject.transform.SetParent(cellObject.transform);
        letterObject.transform.localScale = Vector3.one;    // TODO: If this is not explicitly set, the letters become too big. This is most probably caused by the layout group in cells.
    }

    private void StartTimer()
    {
        TimeManager.Instance.StartTimer();
    }

    public static LetterData GetRandomLetterData(bool isVowel)
    {
        var randomIndex = Random.Range(0, isVowel ? 5 : StaticLetterDataList.Count);
        return StaticLetterDataList[randomIndex];
    }
}
