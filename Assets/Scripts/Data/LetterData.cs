using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LetterData")]
public class LetterData : ScriptableObject
{
    public string letter;
    public int points;

    public void SetValues(string letterValue, int pointsValue)
    {
        this.letter = letterValue;
        this.points = pointsValue;
    }
}
