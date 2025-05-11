using UnityEngine;
using TMPro;

public class ScoreHistoryUI : MonoBehaviour
{
    public TextMeshProUGUI[] historyTexts;

    void Start()
    {
        for (int i = 0; i < historyTexts.Length; i++)
        {
            string entry = PlayerPrefs.GetString($"History_{i}", "No Data");
            historyTexts[i].text = entry;
        }
    }
}