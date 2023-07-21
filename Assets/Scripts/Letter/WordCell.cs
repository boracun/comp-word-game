using UnityEngine;
using UnityEngine.EventSystems;

public class WordCell : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        SpaceManager.Instance.AddLetterToWord();
        SpaceManager.Instance.CreateWordCell();
    }
}
