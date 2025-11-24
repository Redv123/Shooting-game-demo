using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int score;
    public int[] bestScores = new int[5];

    private const string SaveKey = "Save";

    public void SaveGame()
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
                if (bestScores[i] < bestScores[minIndex])
                    minIndex = i;
            }

            if (score > bestScores[minIndex])
            {
                bestScores[minIndex] = score;
            }

            Array.Sort(bestScores);
            Array.Reverse(bestScores);
        }
        else
        {
            bestScores[0] = score;
        }


        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            score = 0;
            bestScores = new int[5];
            return;
        }

        string json = PlayerPrefs.GetString(SaveKey);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        score = data.score;
        bestScores = (data.bestScores != null && data.bestScores.Length == 5)
            ? data.bestScores
            : new int[5];
    }
}