using System;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private LetterData _letterData;
    private TextMeshPro _textMeshPro;
    private SpriteRenderer _spriteRenderer;
    
    public LetterData LetterData
    {
        get => _letterData;
        set
        {
            _letterData = value;
            _textMeshPro.text = _letterData.letter;
        }
    }

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDrag()
    {
        var cameraPosition = GameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -2f);
        ToFront();
    }

    private void OnMouseUp()
    {
        ToBack();
    }

    private void ToFront()
    {
        _spriteRenderer.sortingOrder = 5;
        _textMeshPro.sortingOrder = 6;
    }

    private void ToBack()
    {
        _spriteRenderer.sortingOrder = 0;
        _textMeshPro.sortingOrder = 1;
    }
}
