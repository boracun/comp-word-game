using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager Instance { get; private set; }

    [SerializeField] private GameObject WordCellPrefabGO;

    private int _wordLength = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    
    public void HandleWordLength(bool previousIsWordCell, bool newIsWordCell)
    {
        if (previousIsWordCell && !newIsWordCell)
            HandleWordToGrid();
        else if (!previousIsWordCell && newIsWordCell)
            HandleGridToWord();
        else if (previousIsWordCell && newIsWordCell)
            HandleWordToWord();
    }
    
    public void RemoveWordCell(int cellIndex)
    {
        Destroy(transform.GetChild(cellIndex).gameObject);
    }
    
    public void ResetWordContainer()
    {
        int wordCellCount = transform.childCount;

        for (int i = wordCellCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).childCount > 0)
            {
                GameObject letterGO = transform.GetChild(i).GetChild(0).gameObject;
                letterGO.transform.SetParent(transform.root);
                letterGO.transform.SetAsLastSibling();
                StartCoroutine(letterGO.GetComponent<LetterMovement>().MoveDownAfterSubmission());
            }
            
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            CreateWordCell();
        }

        _wordLength = 0;
    }
    
    private void CreateWordCell()
    {
        GameObject newWordCell = Instantiate(WordCellPrefabGO, transform.position, Quaternion.identity);
        newWordCell.transform.SetParent(transform);
        newWordCell.transform.localScale = Vector3.one;
    }
    
    private bool CanCreateWordCell()
    {
        if (_wordLength < 3)
            return false;

        return transform.childCount == _wordLength;
    }

    private void HandleGridToWord()
    {
        _wordLength++;
        
        if (CanCreateWordCell())
            CreateWordCell();
    }

    private void HandleWordToGrid()
    {
        _wordLength--;
    }

    private void HandleWordToWord()
    {
        if (CanCreateWordCell())
            CreateWordCell();
    }
}
