using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public int CellId;
    public bool IsWordCell;

    private void Awake()
    {
        IsWordCell = transform.GetComponent<WordCell>() != null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            return;
        
        GameObject droppedGO = eventData.pointerDrag;
        Letter droppedLetter = droppedGO.GetComponent<Letter>();
        droppedLetter.ParentTransform = transform;
    }
}
