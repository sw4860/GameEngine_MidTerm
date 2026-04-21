using UnityEngine;

public class ShotTrap : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector2 spawnOffset;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int damage = 1;
    [SerializeField] private float minInterval = 0.25f;
    [SerializeField] private float maxInterval = 0.25f;
    [SerializeField] private float projectileDuration = 3f;

    private float timer = 0f;
    private float nextInterval = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextInterval)
        {
            timer = 0f;
            nextInterval = Random.Range(minInterval, maxInterval);
            GameObject GO = Instantiate(projectile, transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0), Quaternion.identity);
            GO.GetComponent<ProjectileObject>().Init(damage, Random.Range(minSpeed, maxSpeed), projectileDuration, direction);
        }
    }
}
