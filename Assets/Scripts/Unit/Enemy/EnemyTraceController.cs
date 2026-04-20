using UnityEngine;

public class EnemyTraceContoller : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float raycastDistance = .2f;
    public float traceDistance = 2f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;

        if (direction.magnitude > raycastDistance)
            return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction.normalized, raycastDistance);

        foreach (RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
            {
                Vector3 alternaticeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternaticeDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}