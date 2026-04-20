using TMPro;
using UnityEngine;

public class TriggerTextShower : MonoBehaviour
{
    [SerializeField] private string Text;
    [SerializeField] private TextMeshPro textMesh;

    void Awake()
    {
        textMesh.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textMesh.text = Text;
            textMesh.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textMesh.gameObject.SetActive(false);
        }
    }
}
