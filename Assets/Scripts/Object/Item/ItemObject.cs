using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemAbility itemAbility;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemAbility.ItemSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        if (collision.TryGetComponent(out Player player))
        {
            itemAbility.ApplyAbility(player);
            Destroy(gameObject);
        }
    }
}
