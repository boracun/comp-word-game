using System;
using System.Collections;
using SpecialItems;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAction : MonoBehaviour
{
    private GameObject _gridContainerGO;
    private Button _button;

    [SerializeField] private CooldownDisplayer _cooldownDisplayerScript;
    
    private void Awake()
    {
        _gridContainerGO = GameObject.Find("Grid Container");
        _button = GetComponent<Button>();
    }

    public void MultiplyPointsByTwo()
    {
        StartCoroutine(UseItemForDuration(SpecialItem.Multiplier2Item, 30f));
    }

    IEnumerator UseItemForDuration(SpecialItem specialItem, float durationSeconds)
    {
        _button.interactable = false;
        SpecialItemManager.Instance.UseItem(specialItem);
        _cooldownDisplayerScript.StartCooldown(durationSeconds);
        yield return new WaitForSeconds(durationSeconds);
        SpecialItemManager.Instance.StopUsingItem(specialItem);
        _button.interactable = true;
    }
}

