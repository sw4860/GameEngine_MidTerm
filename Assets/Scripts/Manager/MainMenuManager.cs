using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject helpMenuPanel;
    public GameObject stageSelectPanel;
    private bool isHelpMenuActive = false;
    private bool isStageSelectActive = false;

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
