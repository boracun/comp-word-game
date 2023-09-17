using System;
using System.Collections.Generic;
using SpecialItems;
using TMPro;
using UnityEngine;

public class SpecialItemManager : MonoBehaviour
{
    public static SpecialItemManager Instance { get; private set; }
    public LetterData WildLetterData;
    
    private List<SpecialItem> _specialItemsInUse;
    private List<int> _itemCounts;
    [SerializeField] private List<TextMeshProUGUI> _itemCountDisplays;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        _specialItemsInUse = new List<SpecialItem>();
        
        // TODO: Load from save file
        _itemCounts = new List<int> { 5, 5, 5, 5 };
        UpdateInventoryDisplay();
    }

    public bool IsInUse(SpecialItem specialItem)
    {
        return _specialItemsInUse.Contains(specialItem);
    }

    public void UseItem(SpecialItem specialItem, bool decremented = false)
    {
        _specialItemsInUse.Add(specialItem);
        
        if (!decremented)
            return;
        
        _itemCounts[(int)specialItem]--;
        UpdateInventoryDisplay();
    }

    public void StopUsingItem(SpecialItem specialItem, bool decremented = false)
    {
        _specialItemsInUse.Remove(specialItem);
        
        if (!decremented)
            return;
        
        _itemCounts[(int)specialItem]--;
        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        for (int i = 0; i < _itemCounts.Count; i++)
        {
            _itemCountDisplays[i].text = _itemCounts[i].ToString();
        }
    }
}
