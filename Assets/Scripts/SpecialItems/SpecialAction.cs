using System;
using System.Collections;
using SpecialItems;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAction : MonoBehaviour
{
    private GameObject _gridContainerGO;
    private int _gridContainerSiblingIndex;
    private Button _button;

    [SerializeField] private CooldownDisplayer _cooldownDisplayerScript;
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private GameObject specialItemPrefab;
    
    private Transform _originalParentTransform;
    
    private void Awake()
    {
        _gridContainerGO = GameObject.Find("Grid Container");
        _gridContainerSiblingIndex = _gridContainerGO.transform.GetSiblingIndex();
        _button = GetComponent<Button>();
    }

    public void UseWildLetter(bool decremented = false)
    {
        if (!SpecialItemManager.Instance.CanBeUsed(SpecialItem.WildLetterItem))
            return;
        
        // Deactivate
        if (SpecialItemManager.Instance.IsInUse(SpecialItem.WildLetterItem))
        {
            Destroy(_originalParentTransform.GetChild(0).gameObject);
            
            SpecialItemManager.Instance.StopUsingItem(SpecialItem.WildLetterItem, decremented);
            transform.SetParent(_originalParentTransform);
            transform.SetAsFirstSibling();
            
            _gridContainerGO.transform.SetParent(transform.root);
            _gridContainerGO.transform.SetSiblingIndex(_gridContainerSiblingIndex);
            
            darkPanel.SetActive(false);
        }
        // Activate
        else
        {
            SpecialItemManager.Instance.UseItem(SpecialItem.WildLetterItem);
            _originalParentTransform = transform.parent;
            transform.SetParent(darkPanel.transform);
            
            _gridContainerGO.transform.SetParent(darkPanel.transform);
            
            darkPanel.SetActive(true);

            GameObject placeHolderItem = Instantiate(specialItemPrefab, transform.position, Quaternion.identity, _originalParentTransform);
            placeHolderItem.transform.SetAsFirstSibling();
        }
    }

    public void MultiplyPointsByTwo()
    {
        if (!SpecialItemManager.Instance.CanBeUsed(SpecialItem.Multiplier2Item))
            return;
        
        StartCoroutine(UseItemForDuration(SpecialItem.Multiplier2Item, 30f));
    }

    IEnumerator UseItemForDuration(SpecialItem specialItem, float durationSeconds)
    {
        _button.interactable = false;
        SpecialItemManager.Instance.UseItem(specialItem, true);
        _cooldownDisplayerScript.StartCooldown(durationSeconds);
        yield return new WaitForSeconds(durationSeconds);
        SpecialItemManager.Instance.StopUsingItem(specialItem);
        _button.interactable = true;
    }
}

