using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;
        transform.position = new Vector3(player.position.x, player.position.y, -10f) + offset;
    }
}
