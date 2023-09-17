using System;
using System.Collections.Generic;
using SpecialItems;
using UnityEngine;

public class SpecialItemManager : MonoBehaviour
{
    public static SpecialItemManager Instance { get; private set; }
    public LetterData WildLetterData;
    
    private List<SpecialItem> _specialItemsInUse;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        _specialItemsInUse = new List<SpecialItem>();
    }

    public bool IsInUse(SpecialItem specialItem)
    {
        return _specialItemsInUse.Contains(specialItem);
    }

    public void UseItem(SpecialItem specialItem)
    {
        _specialItemsInUse.Add(specialItem);
    }

    public void StopUsingItem(SpecialItem specialItem)
    {
        _specialItemsInUse.Remove(specialItem);
    }
}
