using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCellManager : MonoBehaviour
{
    public static EmptyCellManager Instance { get; private set; }

    [HideInInspector] public List<int> EmptyCellIdList;

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

    public Transform GetRandomEmptyCellTransform()
    {
        return EmptyCellIdList.Count > 0 ? transform.GetChild(EmptyCellIdList[0]) : null;
    }

    private void InitializeCellIds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Cell>().CellId = i;
        }
    }
}
