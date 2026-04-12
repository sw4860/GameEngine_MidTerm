using UnityEngine;

public class ParallexBG : MonoBehaviour
{
    [SerializeField] public float parallaxEffectX;
    [SerializeField] public float parallaxEffectY;
    private Player player;
    private float startPosX;
    private float startPosY;
    private float length;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        startPosX = transform.position.x;
        startPosY = transform.position.y;
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
        clone.transform.SetParent(transform);
    }

    void LateUpdate()
    {
        if (player == null) return;

        float temp = player.transform.position.x * (1 - parallaxEffectX);
        float distanceX = player.transform.position.x * parallaxEffectX;
        float distanceY = player.transform.position.y * parallaxEffectY;

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if (temp > startPosX + length) startPosX += length;
        else if (temp < startPosX - length) startPosX -= length;
    }
}
