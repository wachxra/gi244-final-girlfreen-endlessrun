using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject scorePanel;
    public GameObject howToPlayPanel;

    public Sprite soundOnIcon;
    public Sprite soundOffIcon;
    public Button soundToggleButton;

    private bool isSoundOn = true;
    private int highScore = 0;

    void Start()
    {
        LoadData();
        UpdateSoundIcon();
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void OnToggleScorePanel()
    {
        scorePanel.SetActive(!scorePanel.activeSelf);
    }

    public void OnToggleHowToPlayPanel()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
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
        {
            soundToggleButton.image.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        AudioListener.volume = isSoundOn ? 1 : 0;
    }

    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;

        AudioListener.volume = isSoundOn ? 1 : 0;
    }
}