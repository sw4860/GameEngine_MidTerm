using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    private string stageSceneName;
    private TextMeshProUGUI text;
    private Button button;

    public void InitButton(string name, bool isUnlocked)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();

        stageSceneName = name;
        text.text = stageSceneName;
        SetInteractable(isUnlocked);
    }
    
    public void SetInteractable(bool isInteractable)
    {
        button = GetComponent<Button>();
        button.interactable = isInteractable;

        Color textColor = text.color;
        textColor.a = isInteractable ? 1.0f : 0.3f;
        text.color = textColor;
    }

    public void OnClick()
    {
        SceneTransitionManager.Instance.TransitionScene($"{stageSceneName}_Scene");
    }
}