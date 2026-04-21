using System.Collections;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    [SerializeField] private float waitDuration;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isTriggered = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            animator.SetTrigger("Triggerd");
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(WaitForSeconds(waitDuration));
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        rb.gravityScale = 1;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}