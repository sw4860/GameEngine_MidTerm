using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObject : MonoBehaviour
{
    public string currentSceneName;
    public string nextSceneName;
    public int _score = 100;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!StaticValues.ClearedStageSceneNames.Contains(currentSceneName))
            {
                RankPage.Score += _score;
                HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, RankPage.Score);
                StageDataSaver.SaveStage(SceneManager.GetActiveScene().buildIndex, RankPage.Score);
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
