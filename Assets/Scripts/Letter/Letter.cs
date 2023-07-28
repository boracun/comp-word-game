using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform ParentTransform;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private LetterData _letterData;
    
    public LetterData LetterData
    {
        get => _letterData;
        set
        {
            _letterData = value;
            _textMeshProUGUI.text = _letterData.letter;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentTransform = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        _image.raycastTarget = false;
        _textMeshProUGUI.raycastTarget = false;
        
        if (ParentTransform.GetComponent<WordCell>() == null)
            EmptyCellManager.Instance.EmptyCellIdList.Add(ParentTransform.GetComponent<Cell>().CellId);

        if (ParentTransform.GetComponent<WordCell>() == null) 
            return;
        
        // If the letter is on a word cell
        SpaceManager.Instance.RemoveLetterFromWord();
            
        if (ParentTransform.parent.childCount > 3)
            SpaceManager.Instance.RemoveWordCell(ParentTransform.GetSiblingIndex());
    }

    public void OnDrag(PointerEventData eventData)
    {
        var cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ParentTransform == null)
            ParentTransform = EmptyCellManager.Instance.GetRandomEmptyCellTransform();
        
        if (ParentTransform == null)
            Destroy(gameObject);
        
        StartCoroutine(transform.GetComponent<LetterMovement>().MoveToParent(ParentTransform));
        _image.raycastTarget = true;
        _textMeshProUGUI.raycastTarget = true;
        
        if (ParentTransform.GetComponent<WordCell>() == null)
            EmptyCellManager.Instance.EmptyCellIdList.Remove(ParentTransform.GetComponent<Cell>().CellId);
    }
}
