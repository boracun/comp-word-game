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

    public void AddLetterToWord()
    {
        _wordLength++;
        Debug.Log("word length after: " + _wordLength);
    }

    public void CreateWordCell()
    {
        if (!CanCreateWordCell())
            return;
        
        GameObject newWordCell = Instantiate(WordCellPrefabGO, transform.position, Quaternion.identity);
        newWordCell.transform.SetParent(transform);
        newWordCell.transform.localScale = Vector3.one;
        
        
        Debug.Log("child count after: " + transform.childCount);
    }

    private bool CanCreateWordCell()
    {
        if (_wordLength < 3)
            return false;

        return transform.childCount == _wordLength;
    }
}
