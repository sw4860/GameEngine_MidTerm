using UnityEngine;

public class ParallexBG : MonoBehaviour
{
    [SerializeField] public float parallaxEffect;
    private PlayerMovement player;
    private float startPosX;
    private float length;

    void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Start()
    {
        CreateClone(-1);
        CreateClone(1);
    }

    void CreateClone(int direction)
    {
        GameObject clone = Instantiate(gameObject, transform.position + new Vector3(length * direction, 0, 0), transform.rotation);
        
        Destroy(clone.GetComponent<ParallexBG>());
        clone.transform.SetParent(this.transform);
    }

    void LateUpdate()
    {
        float temp = player.transform.position.x * (1 - parallaxEffect);
        float distance = player.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);

        if (temp > startPosX + length) startPosX += length;
        else if (temp < startPosX - length) startPosX -= length;
    }
}
