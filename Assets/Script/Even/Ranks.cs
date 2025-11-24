using UnityEngine;
using TMPro;

public class Ranks : MonoBehaviour
{
    private ScoreManager scoreManager;
    private BestScores[] scores;
    [SerializeField] private TMP_Text[] scoreText = new TMP_Text[5];

    void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        scores = scoreManager.Load();

        for (int i = 0; i < scoreText.Length; i++)
        {
            if (scores[i].score != 0)
            {
                scoreText[i].text = scores[i].name + " : " + scores[i].score;
            }
            else
            {
                scoreText[i].text = "……";
            }
        }
    }
}
