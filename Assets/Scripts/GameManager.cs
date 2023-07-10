using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject emptyCellPrefab;
    [SerializeField] private float spaceBetween;
    public int gridWidth;
    public int gridHeight;
    private float cellSide;   // TODO: Make this change according to the grid size

    private void Start()
    {
        cellSide = emptyCellPrefab.GameObject().GetComponent<BoxCollider2D>().size.x;
        Vector2 firstCellPos = new Vector2(-(gridHeight / 2) * (spaceBetween + cellSide),
            -(gridHeight / 2) * (spaceBetween + cellSide));
        
        for (int y = 0; y < gridHeight; y++)
        {
            Vector2 currentPos = firstCellPos + new Vector2(0, y * (cellSide + spaceBetween));
            
            for (int x = 0; x < gridWidth; x++)
            {
                CreateEmptyCell(currentPos, x, y);
                currentPos += new Vector2(cellSide + spaceBetween, 0);
            }
        }
    }

    private void CreateEmptyCell(Vector2 position, int x, int y)
    {
        var emptyCellObject = Instantiate(emptyCellPrefab, position, Quaternion.identity);
        emptyCellObject.transform.parent = transform;
        emptyCellObject.name = "Cell (" + x + ", " + y + ")";
    }
}
