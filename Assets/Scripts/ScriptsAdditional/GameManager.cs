using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    [Header("UI")]
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI finalDistanceText;
    public TextMeshProUGUI finalCoinsText;
    public TextMeshProUGUI highScoreResultText;
    public GameObject gameOverPanel;

    [Header("Gameplay Values")]
    public float currentDistance = 0f;
    public int currentCoins = 0;
    public float distancePerSecond = 5f;
    private bool isGameOver = false;

    private float bestDistance;
    private int bestCoins;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        bestDistance = PlayerPrefs.GetFloat("BestDistance", 0f);
        bestCoins = PlayerPrefs.GetInt("BestCoins", 0);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        currentDistance += distancePerSecond * Time.deltaTime;
        distanceText.text = $"Distance: {Mathf.FloorToInt(currentDistance)}m";
        coinsText.text = $"Coins: {currentCoins}";
    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        coinsText.text = $"Coins: {currentCoins}";
    }

    public void EndGame()
    {
        isGameOver = true;

        int finalDist = Mathf.FloorToInt(currentDistance);
        finalDistanceText.text = $"Distance: {finalDist}m";
        finalCoinsText.text = $"Coins: {currentCoins}";

        bool brokeDistance = finalDist > bestDistance;
        bool brokeCoins = currentCoins > bestCoins;

        if (brokeDistance)
        {
            bestDistance = finalDist;
            PlayerPrefs.SetFloat("BestDistance", bestDistance);
        }

        if (brokeCoins)
        {
            bestCoins = currentCoins;
            PlayerPrefs.SetInt("BestCoins", bestCoins);
        }

        PlayerPrefs.Save();

        if (brokeDistance || brokeCoins)
            highScoreResultText.text = "New Record!";
        else
            highScoreResultText.text = "You didn't beat your record.";

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Prototype");
    }

    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
