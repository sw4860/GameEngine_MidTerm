using UnityEngine;

public class StageSelectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectBtn;
    [SerializeField] private Transform btnContainer;

    void Start()
    {
        gameObject.SetActive(false);
        
        for (int i = 0; i < StaticValues.StageSceneNames.Count; i++)
        {
            string currentStage = StaticValues.StageSceneNames[i];

            bool isUnlocked = (i == 0) || StaticValues.ClearedStageSceneNames.Contains(StaticValues.StageSceneNames[i - 1]);

            GameObject btn = Instantiate(stageSelectBtn, transform);
            btn.transform.SetParent(btnContainer);
            StageSelectButton btnScript = btn.GetComponent<StageSelectButton>();
            btnScript.InitButton(currentStage, isUnlocked);
        }
    }
}
    