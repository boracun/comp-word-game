// using System;
// using System.Collections.Generic;
// using TMPro;
// using Unity.VisualScripting;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// public class GameManager : MonoBehaviour
// {
//     public static Camera mainCamera;
//     
//     [SerializeField] private GameObject emptyCellPrefab;
//     [SerializeField] private GameObject letterPrefab;
//     [SerializeField] private List<LetterData> letterDataList;
//     
//     [SerializeField] private float spaceBetween;
//     public int gridSide;
//     private float cellSide;   // TODO: Make this change according to the grid size
//
//     private void Start()
//     {
//         mainCamera = Camera.main;
//         InitializeGrid();
//     }
//
//     /**
//      * The grid is always a square in the current version.
//      */
//     private void InitializeGrid()
//     {
//         cellSide = emptyCellPrefab.transform.localScale.x;
//
//         float firstCellPosX;
//         if (gridSide % 2 == 1)
//             firstCellPosX = -(gridSide / 2) * (spaceBetween + cellSide);
//         else
//             firstCellPosX = -(gridSide / 2 - 0.5f) * (spaceBetween + cellSide);
//         
//         Vector2 firstCellPos = new Vector2(firstCellPosX, firstCellPosX); 
//         
//         for (int y = 0; y < gridSide; y++)
//         {
//             Vector2 currentPos = firstCellPos + new Vector2(0, y * (cellSide + spaceBetween));
//             
//             for (int x = 0; x < gridSide; x++)
//             {
//                 CreateEmptyCell(currentPos, x, y);
//                 currentPos += new Vector2(cellSide + spaceBetween, 0);
//             }
//         }
//     }
//
//     private void CreateEmptyCell(Vector2 position, int x, int y)
//     {
//         var emptyCellObject = Instantiate(emptyCellPrefab, (Vector3) position + transform.position, Quaternion.identity);
//         emptyCellObject.transform.parent = transform;
//         emptyCellObject.name = "Cell (" + x + ", " + y + ")";
//         AssignLetterToCell(emptyCellObject);
//     }
//
//     private void AssignLetterToCell(GameObject cellObject)
//     {
//         int randomIndex = Random.Range(0, letterDataList.Count);
//         LetterData letterData = letterDataList[randomIndex];
//         
//         var letterObject = Instantiate(letterPrefab, cellObject.transform.position, cellObject.transform.rotation);
//         letterObject.GetComponent<Letter>().LetterData = letterData;
//         letterObject.transform.SetParent(cellObject.transform);
//     }
// }
