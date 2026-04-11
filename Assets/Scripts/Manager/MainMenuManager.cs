using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject helpMenuPanel;
    private bool isHelpMenuActive = false;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1_Scene");
    }

    public void ToggleHelpMenu()
    {
        isHelpMenuActive = !isHelpMenuActive;
        helpMenuPanel.SetActive(isHelpMenuActive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
