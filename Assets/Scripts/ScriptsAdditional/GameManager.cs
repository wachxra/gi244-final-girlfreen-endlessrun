using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    [Header("HUD")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinsText;

    [Header("Death Panel")]
    public GameObject deathPanel;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI finalCoinsText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public float currentDistance = 0f;
    public int currentCoins = 0;
    public float distancePerSecond = 5f;

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        currentDistance += distancePerSecond * Time.deltaTime;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        distanceText.text = $"Distance: {Mathf.FloorToInt(currentDistance)}m";
        coinsText.text = $"Coins: {currentCoins}";
    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        UpdateHUD();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int distanceInt = Mathf.FloorToInt(currentDistance);
        int score = distanceInt + currentCoins;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }

        SaveScoreHistory(distanceInt, currentCoins, score);

        finalDistanceText.text = $"Distance: {distanceInt}m";
        finalCoinsText.text = $"Coins: {currentCoins}";
        finalScoreText.text = $"Score: {score}";
        highScoreText.text = $"High Score: {highScore}";

        deathPanel.SetActive(true);
    }

    void SaveScoreHistory(int distance, int coins, int score)
    {
        List<string> history = new List<string>();

        for (int i = 0; i < 5; i++)
        {
            string entry = PlayerPrefs.GetString($"History_{i}", "");
            if (!string.IsNullOrEmpty(entry))
                history.Add(entry);
        }

        string newEntry = $"Distance: {distance}m, Coins: {coins}, Score: {score}";
        history.Insert(0, newEntry);

        if (history.Count > 5) history.RemoveAt(5);

        for (int i = 0; i < history.Count; i++)
        {
            PlayerPrefs.SetString($"History_{i}", history[i]);
        }

        PlayerPrefs.Save();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}