using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HpText;
    [SerializeField] private Image hpBarImage;

    [SerializeField] private GameObject GameOverPanel;

    private Player player;
    void Awake()
    {
        player = GameObject.FindAnyObjectByType<Player>();
        EventManager.OnPlayerHPChanged += OnPlayerHPChanged;
        EventManager.OnPlayerDeath += OnPlayerDeath;
    }

    void OnDestroy()
    {
        EventManager.OnPlayerHPChanged -= OnPlayerHPChanged;
        EventManager.OnPlayerDeath -= OnPlayerDeath;
    }

    void OnPlayerHPChanged()
    {
        HpText.text = $"{player.GetHealth()[0]}/{player.GetHealth()[1]}";
        hpBarImage.fillAmount = player.GetHealth()[0] / player.GetHealth()[1];
    }

    void OnPlayerDeath()
    {
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void StartSceneButton(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
