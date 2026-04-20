using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField] private string sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneTransitionManager.Instance.TransitionScene(sceneName);   
    }
}
