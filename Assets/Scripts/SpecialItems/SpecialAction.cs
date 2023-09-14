using System;
using System.Collections;
using SpecialItems;
using UnityEngine;

public class SpecialAction : MonoBehaviour
{
    private GameObject _gridContainerGO;

    [SerializeField] private CooldownDisplayer _cooldownDisplayerScript;
    
    private void Awake()
    {
        _gridContainerGO = GameObject.Find("Grid Container");
    }

    public void MultiplyPointsByTwo()
    {
        StartCoroutine(UseItemForDuration(SpecialItem.Multiplier2Item, 30f));
    }

    IEnumerator UseItemForDuration(SpecialItem specialItem, float durationSeconds)
    {
        SpecialItemManager.Instance.UseItem(specialItem);
        _cooldownDisplayerScript.StartCooldown(durationSeconds);
        yield return new WaitForSeconds(durationSeconds);
        SpecialItemManager.Instance.StopUsingItem(specialItem);
    }
}

