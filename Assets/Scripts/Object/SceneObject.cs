using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObject : MonoBehaviour
{
    public string currentSceneName;
    public string nextSceneName;
    private int score = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!StaticValues.ClearedStageSceneNames.Contains(currentSceneName))
            {
                score += 100;
                HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, score);
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
