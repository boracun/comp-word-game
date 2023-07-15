using UnityEngine;

public class WordSubmission : MonoBehaviour
{
    public void SubmitWord()
    {
        foreach (Transform cellTransform in transform)
        {
            if (cellTransform.childCount == 0)
                continue;

            ScoreManager.Instance.IncreaseScore(cellTransform.GetChild(0).GetComponent<Letter>().LetterData.points);
        }
    }
}
