using System;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private LetterData _letterData;
    private bool isHeld;

    private void Update()
    {
        if (!isHeld) return;
        var cameraPosition = GameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -1f);
    }

    private void OnMouseDown()
    {
        isHeld = true;
        Debug.Log("true");
    }

    private void OnMouseUp()
    {
        isHeld = false;
        Debug.Log("false");
    }
}
