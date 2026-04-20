using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TresureChest : MonoBehaviour
{
    [SerializeField] private Sprite tresureSprite;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject PortalPrefab;
    [SerializeField] private Vector2 PortalSpawnPosition;
    [SerializeField] private float openDuration = 2f;

    private bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;

        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            GameObject GO = Instantiate(itemPrefab, collision.transform.position, Quaternion.identity);
            GO.transform.SetParent(collision.transform);
            GO.GetComponent<TresureObject>().Init(tresureSprite);
            StartCoroutine(OpenChest());
        }
    }

    IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(openDuration);

        string currentSceneName = SceneManager.GetActiveScene().name;
        string currentStage = currentSceneName.Replace("_Scene", "");
        string nextSceneName = "MainMenu";

        int currentIndex = StaticValues.StageSceneNames.IndexOf(currentStage);
        if (currentIndex != -1 && currentIndex + 1 < StaticValues.StageSceneNames.Count)
        {
            nextSceneName = StaticValues.StageSceneNames[currentIndex + 1] + "_Scene";
        }

        GameObject GO = Instantiate(PortalPrefab, PortalSpawnPosition, Quaternion.identity);
        GO.GetComponent<SceneObject>().SetSceneNames(currentStage, nextSceneName);
        Destroy(gameObject);
    }
}