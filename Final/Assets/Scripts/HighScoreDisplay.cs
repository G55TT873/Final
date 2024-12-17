using UnityEngine;
using TMPro;
using System.Linq;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        var highScores = HighScoreManager.Instance.GetHighScores();

        var sortedHighScores = highScores.OrderByDescending(score => score).ToList();

        string displayText = "Top 5 High Scores:\n";

        int topCount = Mathf.Min(sortedHighScores.Count, 5);

        for (int i = 0; i < topCount; i++)
        {
            displayText += $"{i + 1}. {sortedHighScores[i]:F2} seconds\n";
        }

        highScoreText.text = displayText;
    }
}
