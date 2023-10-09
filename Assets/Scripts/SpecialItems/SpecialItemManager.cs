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
    
    private static string _itemsPath;
    
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
        _itemsPath = Application.persistentDataPath + "/items.json";
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
        SaveItemCounts();
    }

    public void StopUsingItem(SpecialItem specialItem, bool decremented = false)
    {
        _specialItemsInUse.Remove(specialItem);
        
        if (!decremented)
            return;
        
        _itemCounts[(int)specialItem]--;
        UpdateInventoryDisplay();
        SaveItemCounts();
    }

    public bool CanBeUsed(SpecialItem specialItem)
    {
        return _itemCounts[(int)specialItem] > 0;
    }

    public void GiveItem(SpecialItem specialItem, int count = 1)
    {
        _itemCounts[(int)specialItem] += count;
        SaveItemCounts();
        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        for (int i = 0; i < _itemCounts.Count; i++)
        {
            _itemCountDisplays[i].text = _itemCounts[i].ToString();
        }
    }

    private void SaveItemCounts()
    {
        string json = JsonUtility.ToJson(new ItemCounts(_itemCounts));
        File.WriteAllText(_itemsPath, EncryptDecrypt(json));
    }

    private static List<int> LoadItemCounts()
    {
        if (!File.Exists(_itemsPath))
            return new List<int> { 3, 3, 3 };
        string json = File.ReadAllText(_itemsPath);
        return JsonUtility.FromJson<ItemCounts>(EncryptDecrypt(json)).ItemCountList;
    }

    public static string EncryptDecrypt(string data)
    {
        string result = "";
        string keyword = "784632894";

        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyword[i % keyword.Length]);
        }
        
        return result;
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
