using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject scorePanel;
    public GameObject howToPlayPanel;

    [Header("Sound")]
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Button soundToggleButton;
    public AudioSource bgMusic;

    private bool isSoundOn = true;
    private int highScore = 0;

    void Start()
    {
        LoadData();
        UpdateSoundIcon();

        scorePanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void OnToggleScorePanel()
    {
        scorePanel.SetActive(true);
    }

    public void OnToggleHowToPlayPanel()
    {
        howToPlayPanel.SetActive(true);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnToggleSound()
    {
        isSoundOn = !isSoundOn;
        SaveData();
        UpdateSoundIcon();
    }

    private void UpdateSoundIcon()
    {
        if (soundToggleButton != null)
            soundToggleButton.image.sprite = isSoundOn ? soundOnIcon : soundOffIcon;

        AudioListener.volume = isSoundOn ? 1 : 0;
        if (bgMusic != null)
            bgMusic.mute = !isSoundOn;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        AudioListener.volume = isSoundOn ? 1 : 0;
        if (bgMusic != null)
            bgMusic.mute = !isSoundOn;
    }

    public void OnBackFromScorePanel()
    {
        scorePanel.SetActive(false);
    }

    public void OnBackFromHowToPlayPanel()
    {
        howToPlayPanel.SetActive(false);
    }
}
