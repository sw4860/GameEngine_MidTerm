using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemAbility itemAbility;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public void Init(ItemAbility ability)
    {
        itemAbility = ability;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemAbility.ItemSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        if (collision.TryGetComponent(out Player player))
        {
            itemAbility.ApplyAbility(player);
            animator.enabled = true;
            animator.SetTrigger("Triggerd");
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
