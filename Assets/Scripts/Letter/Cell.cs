using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            return;
        
        GameObject droppedGO = eventData.pointerDrag;
        Letter droppedLetter = droppedGO.GetComponent<Letter>();
        droppedLetter.ParentTransform = transform;
    }
}
