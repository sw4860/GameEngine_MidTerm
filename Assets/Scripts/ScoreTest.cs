using TMPro;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stage1;
    [SerializeField] private TextMeshProUGUI stage2;
    [SerializeField] private TextMeshProUGUI stage3;
    [SerializeField] private TextMeshProUGUI stage4;
    [SerializeField] private TextMeshProUGUI stage5;

    void Start()
    {
        stage1.text = $"STAGE 1 : {HighScore.Load(1)}";
        stage2.text = $"STAGE 2 : {HighScore.Load(2)}";
        stage3.text = $"STAGE 3 : {HighScore.Load(3)}";
        stage4.text = $"STAGE 4 : {HighScore.Load(4)}";
        stage5.text = $"STAGE 5 : {HighScore.Load(5)}";
    }
}
