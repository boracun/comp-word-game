using System.Collections;
using UnityEngine;

public class LetterMovement : MonoBehaviour
{
    private const float TOTAL_DURATION = 0.3f;
    
    private const float BOTTOM_Y = -7.6f;   // Used for moving the letter out after submission
    private const float MAX_X = 3f;
    
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _time;
    private bool _isMoving;

    private Letter _letterScript;

    private void Start()
    {
        _letterScript = GetComponent<Letter>();
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

        transform.position = EaseInOutSine(_startPosition, _endPosition, _time);
        _time += Time.deltaTime / TOTAL_DURATION;
    }

    public IEnumerator MoveToTransform(Transform parentTransform)
    {
        _isMoving = true;
        _startPosition = transform.position;
        _endPosition = parentTransform.position;
        yield return new WaitForSeconds(TOTAL_DURATION);
        
        // Previously handled in the OnEndDrag event
        transform.SetParent(parentTransform);
        _letterScript.Img.raycastTarget = true;
        _letterScript.TMProUGUI.raycastTarget = true;
    }

    private Vector3 EaseInOutSine(Vector3 start, Vector3 end, float value)
    {
        end -= start;
        return 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) * (-end) + start;
    }
}
