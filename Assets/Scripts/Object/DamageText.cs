using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro Text;
    
    public void Init(float damage)
    {
        ColorUtility.TryParseHtmlString("#FF5050", out Color newColor);
        Init(damage, newColor);
    }

    public void Init(float damage, Color color)
    {
        Text.text = damage.ToString();
        Text.color = color;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
