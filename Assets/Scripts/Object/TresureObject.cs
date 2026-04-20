using UnityEngine;

public class TresureObject : MonoBehaviour
{
    public void Init(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
