using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private string currentSceneName;
    [SerializeField] private string nextSceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!StaticValues.ClearedStageSceneNames.Contains(currentSceneName))
            {
                StaticValues.ClearedStageSceneNames.Add(currentSceneName);
            }
            SceneTransitionManager.Instance.TransitionScene(nextSceneName);
        }
    }
}
