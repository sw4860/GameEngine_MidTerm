using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HpText;
    [SerializeField] private Image HpMainImage;
    [SerializeField] private Image HpSubImage;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject GamePausePanel;

    private Player player;
    private float hpRatio;
    [SerializeField] private float hpDecreaseDuration = 0.5f;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            hpRatio = player.GetHealth()[0] / player.GetHealth()[1];
            HpMainImage.fillAmount = hpRatio;
            HpSubImage.fillAmount = hpRatio;
            HpText.text = $"{player.GetHealth()[0]} / {player.GetHealth()[1]}";
        }
        EventManager.OnPlayerHPChanged += OnPlayerHPChanged;
        EventManager.OnPlayerDeath += OnPlayerDeath;
        EventManager.OnEscPressed += OnEscPressed;
    }

    void OnEscPressed()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            GamePausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            GamePausePanel.SetActive(true);
        }
    }

    void OnDestroy()
    {
        EventManager.OnPlayerHPChanged -= OnPlayerHPChanged;
        EventManager.OnPlayerDeath -= OnPlayerDeath;
        EventManager.OnEscPressed -= OnEscPressed;
    }

    void OnPlayerHPChanged()
    {
        if (player == null) return;

        float currentHealth = math.max(0, player.GetHealth()[0]);
        float maxHealth = player.GetHealth()[1];
        float currentHealthRatio = currentHealth / maxHealth;

        HpText.text = $"{currentHealth} / {maxHealth}";
        HpMainImage.fillAmount = currentHealthRatio;

        if (currentHealthRatio < hpRatio)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateHpSubImage(currentHealthRatio));
        }
        else if (currentHealthRatio > hpRatio)
        {
            HpSubImage.fillAmount = currentHealthRatio;
        }

        hpRatio = currentHealthRatio;
    }

    IEnumerator AnimateHpSubImage(float targetFillAmount)
    {
        float startFillAmount = HpSubImage.fillAmount;
        float timer = 0f;

        while (timer < hpDecreaseDuration)
        {
            timer += Time.unscaledDeltaTime;
            float newFillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, timer / hpDecreaseDuration);
            HpSubImage.fillAmount = newFillAmount;
            yield return null;
        }

        HpSubImage.fillAmount = targetFillAmount;
    }
    
    void OnPlayerDeath()
    {
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneTransitionManager.Instance.TransitionScene(SceneManager.GetActiveScene().name);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneTransitionManager.Instance.TransitionScene("MainMenu");
    }

    public void StartSceneButton(string sceneName)
    {
        Time.timeScale = 1f;
        SceneTransitionManager.Instance.TransitionScene(sceneName);
    }
}
