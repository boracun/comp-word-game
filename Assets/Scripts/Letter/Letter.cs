using System;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private LetterData _letterData;
    private bool _isHeld;
    private TextMeshPro _textMeshPro;
    
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
    }

    private void Update()
    {
        if (!_isHeld) return;
        var cameraPosition = GameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -1f);
    }

    private void OnMouseDown()
    {
        _isHeld = true;
        Debug.Log("true");
    }

    private void OnMouseUp()
    {
        _isHeld = false;
        Debug.Log("false");
    }
}
