using UnityEngine;

public class SceneObject : MonoBehaviour
{
    public string currentSceneName;
    public string nextSceneName;

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

    public void SetSceneNames(string current, string next)
    {
        currentSceneName = current;
        nextSceneName = next;
    }
}
