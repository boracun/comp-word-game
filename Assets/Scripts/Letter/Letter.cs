using System;
using SpecialItems;
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
    private Cell _parentCell;
    private LetterMovement _letterMovementScript;
    private SpecialAction _specialActionScript;

    private Animator _animator;
    
    public LetterData LetterData
    {
        get => _letterData;
        set
        {
            _letterData = value;
            _textMeshProUGUI.text = _letterData.letter;
        }
    }

    public Image Img => _image;
    public TextMeshProUGUI TMProUGUI => _textMeshProUGUI;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _letterMovementScript = GetComponent<LetterMovement>();
        _specialActionScript = GameObject.Find("Boost 1").GetComponent<SpecialAction>();
    }

    private void Start()
    {
        _parentCell = GetComponentInParent<Cell>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentTransform = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        _image.raycastTarget = false;
        _textMeshProUGUI.raycastTarget = false;
        
        _animator.SetInteger("DragState", 1);
        
        if (!_parentCell.IsWordCell)
        {
            EmptyCellManager.Instance.EmptyCellIdList.Add(_parentCell.CellId);
            return;
        }
        
        // If the letter is on a word cell
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
        _animator.SetInteger("DragState", 0);
        
        if (ParentTransform == null)
            ParentTransform = EmptyCellManager.Instance.GetRandomEmptyCellTransform();
        
        if (ParentTransform == null)
            Destroy(gameObject);
        
        Cell newCell = ParentTransform.GetComponent<Cell>();
        SpaceManager.Instance.HandleWordLength(_parentCell.IsWordCell, newCell.IsWordCell);

        _parentCell = newCell;
        
        StartCoroutine(_letterMovementScript.MoveToTransform(ParentTransform));
        
        if (!_parentCell.IsWordCell)
            EmptyCellManager.Instance.EmptyCellIdList.Remove(_parentCell.CellId);
    }

    public void ReplaceLetter()
    {
        if (!SpecialItemManager.Instance.IsInUse(SpecialItem.WildLetterItem))
            return;

        _letterData = SpecialItemManager.Instance.WildLetterData;
        _textMeshProUGUI.text = "?";
        _image.color = new Color(0.729f, 0.183f, 0.262f);
        _specialActionScript.UseWildLetter();
    }
}
