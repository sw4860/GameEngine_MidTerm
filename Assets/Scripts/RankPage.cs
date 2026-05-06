using System.Linq;
using TMPro;
using UnityEngine;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject rowPrefab;
    [HideInInspector] public static int Score;

    StageResultList allData;

    void Awake()
    {
        allData = StageDataSaver.LoadRank();
        RefreshRankList(1);
    }

    public void SetRankPage(int stage)
    {
        RefreshRankList(stage);
    }

    void RefreshRankList(int _stage)
    {
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        var sortedData = allData.results.Where(r => r.Stage == _stage).OrderByDescending(x => x.Score).ToList();

        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{sortedData[i].Stage}스테이지\n{i + 1}위. 이름: {sortedData[i].PlayerName} - {sortedData[i].Score}";
        }
    }
}
