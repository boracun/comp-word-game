using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EmptyCellManager : MonoBehaviour
{
    private const float TOP_Y = 7.6f;   // Used for moving the letter out after submission
    private const float MAX_X = 3f;
    
    public static EmptyCellManager Instance { get; private set; }

    [HideInInspector] public List<int> EmptyCellIdList;
    [SerializeField] private GameObject letterPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        EmptyCellIdList = new List<int>();
    }

    private void Start()
    {
        InitializeCellIds();
    }

    // Not random
    public Transform GetRandomEmptyCellTransform()
    {
        return EmptyCellIdList.Count > 0 ? transform.GetChild(EmptyCellIdList[0]) : null;
    }

    public void FillAllEmptyCells()
    {
        bool useVowel = true;
        while (EmptyCellIdList.Count > 0)   
        {
            FillEmptyCell(useVowel);
            useVowel = false;
        }
    }

    public void FillEmptyCell(bool useVowel)
    {
        if (EmptyCellIdList.Count == 0)
            return;
        
        Vector3 spawnPosition = new Vector3(Random.Range(-MAX_X, MAX_X), TOP_Y);
        GameObject letterGO = Instantiate(letterPrefab, spawnPosition, Quaternion.identity, transform.parent);
        letterGO.GetComponent<Letter>().LetterData = GameManager.GetRandomLetterData(useVowel);
        
        Transform emptyCellTransform = GetRandomEmptyCellTransform();
        EmptyCellIdList.Remove(EmptyCellIdList[0]);

        StartCoroutine(letterGO.GetComponent<LetterMovement>().MoveToTransform(emptyCellTransform));
    }

    private void InitializeCellIds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Cell>().CellId = i;
        }
    }
}
