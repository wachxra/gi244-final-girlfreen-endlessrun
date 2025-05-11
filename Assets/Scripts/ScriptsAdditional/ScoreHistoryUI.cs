using UnityEngine;
using TMPro;

public class ScoreHistoryUI : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI[] historyTexts;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {highScore}";
        }

        for (int i = 0; i < historyTexts.Length; i++)
        {
            string entry = PlayerPrefs.GetString($"History_{i}", "");
            if (string.IsNullOrEmpty(entry))
            {
                historyTexts[i].text = "No Data";
                continue;
            }

            bool isHighScoreEntry = entry.Contains($"Score: {highScore}");

            if (isHighScoreEntry)
            {
                historyTexts[i].text = entry + "  <color=yellow>(High Score!)</color>";
                historyTexts[i].color = Color.yellow;
            }
            else
            {
                historyTexts[i].text = entry;
                historyTexts[i].color = Color.white;
            }
        }
    }
}
