using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] ItemAbility itemAbility;
    [SerializeField] GameObject itemPrefab;

    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        GameObject GO = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        GO.GetComponent<ItemObject>().Init(itemAbility);
    }
}
