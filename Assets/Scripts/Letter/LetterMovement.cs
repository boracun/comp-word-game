using System;
using UnityEngine;

public class LetterMovement : MonoBehaviour
{
    private const float TOTAL_DURATION = 0.5f;
    
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private float _time;
    private bool _isMoving = true;

    private RectTransform _rectTransform;

    private void Start()
    {
        _startPosition = new Vector2(-750, -750);
        _endPosition = new Vector2(500, 500);
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!_isMoving)
            return;
        
        if (_time >= 1f)
        {
            _isMoving = false;
            _time = 0f;
            return;
        }

        _rectTransform.anchoredPosition = EaseInOutSine(_startPosition, _endPosition, _time);
        _time += Time.deltaTime / TOTAL_DURATION;
    }
        
    private Vector2 EaseInOutSine(Vector2 start, Vector2 end, float value)
    {
        end -= start;
        return 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) * (-end) + start;
    }
}
