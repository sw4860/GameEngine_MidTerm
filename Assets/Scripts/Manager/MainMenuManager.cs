using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject helpMenuPanel;
    public GameObject stageSelectPanel;
    public GameObject scoreMenuPanel;
    private bool isHelpMenuActive = false;
    private bool isStageSelectActive = false;
    private bool isScoreMenuActive = false;

    public void StartGame()
    {
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
