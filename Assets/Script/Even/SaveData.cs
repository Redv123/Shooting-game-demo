using System;
using UnityEngine;

public struct BestScores { public string name; public int score; }

[Serializable]
public class SaveData
{
    [SerializeField] private BestScores[] bestScores = new BestScores[5];
    private const string SaveKey = "Save";

    public void SaveGame(string name ,int score)
    {
        if (PlayerPrefs.HasKey(SaveKey)) // If player have save file
        {
            string oldJson = PlayerPrefs.GetString(SaveKey);
            SaveData oldData = JsonUtility.FromJson<SaveData>(oldJson);


            if (oldData.bestScores != null && oldData.bestScores.Length == bestScores.Length)
            {
                bestScores = oldData.bestScores;
            }

            int minIndex = 0;
            for (int i = 1; i < bestScores.Length; i++)
            {
                if (bestScores[i].score < bestScores[minIndex].score)
                    minIndex = i;
            }

            if (score > bestScores[minIndex].score)
            {
                bestScores[minIndex].score = score;
                bestScores[minIndex].name = name;
            }

            Array.Sort(bestScores, (a, b) => b.score.CompareTo(a.score));
        }
        else
        {
            bestScores[0].score = score;
        }


        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public BestScores[] LoadGame()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            bestScores = new BestScores[5];
        }
        else
        {
            string json = PlayerPrefs.GetString(SaveKey);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScores = (data.bestScores != null && data.bestScores.Length == 5)
                ? data.bestScores
                : new BestScores[5]; // Make sure the player have save file
        }
        return bestScores;
    }

}
