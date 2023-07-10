using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject emptyCellPrefab;
    [SerializeField] private float spaceBetween;
    public int gridSide;
    private float cellSide;   // TODO: Make this change according to the grid size

    private void Start()
    {
        InitializeGrid();
    }

    /**
     * The grid is always a square in the current version.
     */
    private void InitializeGrid()
    {
        cellSide = emptyCellPrefab.GameObject().GetComponent<BoxCollider2D>().size.x;
        float firstCellPosX = -(gridSide / 2) * (spaceBetween + cellSide);
        Vector2 firstCellPos = new Vector2(firstCellPosX, firstCellPosX); 
        
        for (int y = 0; y < gridSide; y++)
        {
            Vector2 currentPos = firstCellPos + new Vector2(0, y * (cellSide + spaceBetween));
            
            for (int x = 0; x < gridSide; x++)
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
