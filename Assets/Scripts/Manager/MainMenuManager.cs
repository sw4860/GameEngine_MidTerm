using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject helpMenuPanel;
    public GameObject stageSelectPanel;
    public GameObject scoreMenuPanel;
    public TMP_InputField inputField;
    private bool isHelpMenuActive = false;
    private bool isStageSelectActive = false;
    private bool isScoreMenuActive = false;

    public void StartGame()
    {
        string playerName = inputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("이름 입력 필요");
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        Debug.Log($"{playerName}님 환영");

        if (StaticValues.ClearedStageSceneNames.Count == 0)
        {
            SceneTransitionManager.Instance.TransitionScene($"{StaticValues.StageSceneNames[0]}_Scene");
        }
        else
        {
            stageSelectPanel.SetActive(true);
            isStageSelectActive = true;
        }
    }

    public void ToggleHelpMenu()
    {
        isHelpMenuActive = !isHelpMenuActive;
        helpMenuPanel.SetActive(isHelpMenuActive);
    }
    
    public void ToggleScoreMenu()
    {
        isScoreMenuActive = !isScoreMenuActive;
        scoreMenuPanel.SetActive(isScoreMenuActive);
    }

    public void ToggleStageSelect()
    {
        isStageSelectActive = !isStageSelectActive;
        stageSelectPanel.SetActive(isStageSelectActive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
