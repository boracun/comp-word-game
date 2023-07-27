using UnityEngine;
using UnityEngine.EventSystems;

public class WordCell : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            return;
        
        SpaceManager.Instance.AddLetterToWord();
        
        if (SpaceManager.Instance.CanCreateWordCell())
            SpaceManager.Instance.CreateWordCell();
    }
}
