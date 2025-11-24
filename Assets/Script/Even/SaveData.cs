using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    private int[] bestScores = new int[5];
    private const string SaveKey = "Save";

    public void SaveGame(int score)
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

    public int[] LoadGame()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            bestScores = new int[5];
        }
        else
        {
            string json = PlayerPrefs.GetString(SaveKey);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScores = (data.bestScores != null && data.bestScores.Length == 5)
                ? data.bestScores
                : new int[5]; // Make sure the player have save file
        }
        return bestScores;
    }

}
