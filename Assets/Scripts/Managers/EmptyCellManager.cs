using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCellManager : MonoBehaviour
{
    public static EmptyCellManager Instance { get; private set; }

    [HideInInspector] public List<Transform> EmptyCellList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        EmptyCellList = new List<Transform>();
    }

    private void Start()
    {
        InitializeCellIds();
    }

    private void InitializeCellIds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Cell>().CellId = i;
        }
    }
}
