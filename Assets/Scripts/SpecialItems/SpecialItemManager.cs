using System;
using System.Collections.Generic;
using System.IO;
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

        _itemCounts = LoadItemCounts();
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
        SaveItemCounts(_itemCounts);
    }

    public void StopUsingItem(SpecialItem specialItem, bool decremented = false)
    {
        _specialItemsInUse.Remove(specialItem);
        
        if (!decremented)
            return;
        
        _itemCounts[(int)specialItem]--;
        UpdateInventoryDisplay();
        SaveItemCounts(_itemCounts);
    }

    public bool CanBeUsed(SpecialItem specialItem)
    {
        return _itemCounts[(int)specialItem] > 0;
    }

    public static void GiveItem(SpecialItem specialItem, int count = 1)
    {
        List<int> itemCounts = LoadItemCounts();
        itemCounts[(int)specialItem] += count;
        SaveItemCounts(itemCounts);
    }

    private void UpdateInventoryDisplay()
    {
        for (int i = 0; i < _itemCounts.Count; i++)
        {
            _itemCountDisplays[i].text = _itemCounts[i].ToString();
        }
    }

    private static void SaveItemCounts(List<int> itemCounts)
    {
        string json = JsonUtility.ToJson(new ItemCounts(itemCounts));
        File.WriteAllText(Application.persistentDataPath + "/items.json", json);
    }

    private static List<int> LoadItemCounts()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/items.json");
        return JsonUtility.FromJson<ItemCounts>(json).ItemCountList;
    }
}

public struct ItemCounts
{
    public List<int> ItemCountList;

    public ItemCounts(List<int> itemCountList)
    {
        ItemCountList = itemCountList;
    }
}
