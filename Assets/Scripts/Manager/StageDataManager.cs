using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class StageResult
{
    public string PlayerName;
    public int Stage;
    public int Score;
}

[System.Serializable]
public class StageResultList
{
    public List<StageResult> results = new();
}

public class StageDataSaver
{
    private const string FILE = "stage_result.json";
    private const string PLAYER_NAME = "PlayerName";
    private static string filepath = Path.Combine(Application.persistentDataPath, FILE);

    public static void SaveStage(int _stage, int _score)
    {
        StageResultList list = LoadInternal();

        string playerName = PlayerPrefs.GetString(PLAYER_NAME, "");
        StageResult entry = new StageResult
        {
            PlayerName = playerName,
            Stage = _stage,
            Score = _score
        };

        list.results.Add(entry);
        string json = JsonUtility.ToJson(list, true);
        File.WriteAllText(filepath, json);
    }

    public static StageResultList LoadRank()
    {
        return LoadInternal();
    }

    private static StageResultList LoadInternal()
    {
        if (!File.Exists(filepath)) return new StageResultList();

        string json = File.ReadAllText(filepath);
        StageResultList list = JsonUtility.FromJson<StageResultList>(json);

        if (list == null) return new StageResultList();
        else return list;
    }
}
