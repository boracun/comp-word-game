using System;
using UnityEngine;

public class SpecialAction : MonoBehaviour
{
    private GameObject _gridContainerGO;
    
    private void Awake()
    {
        _gridContainerGO = GameObject.Find("Grid Container");
    }

    public void ReplaceHorizontally()
    {
        // Pick the row to replace first: Assume first row for now
        int row = 0;

        // Send the old letters down
        for (int i = row * 5; i < row * 5 + 5; i++)
        {
            StartCoroutine(_gridContainerGO.transform.GetChild(i).GetChild(0).GetComponent<LetterMovement>()
                .MoveDownAfterSubmission());
        }
        
        // Bring new letters from top

    }
}

