using UnityEngine;
using TMPro;

public class Ranks : MonoBehaviour
{
    private ScoreManager scoreManager;
    private BestScores[] scores;
    [SerializeField] private TMP_Text[] scoreText = new TMP_Text[5];
    [SerializeField] private TMP_Text[] playerName = new TMP_Text[5];

    void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        scores = scoreManager.Load();

        for (int i = 0; i < scoreText.Length; i++)
        {

            playerName[i].text = scores[i].name;
            scoreText[i].text = scores[i].score.ToString();

            if (scores[i].score != 0)
            {
                playerName[i].text = scores[i].name;
                scoreText[i].text = scores[i].score.ToString();
            }
            else
            {
                playerName[i].text = "";
                scoreText[i].text = "";
            }
        }
    }
}
