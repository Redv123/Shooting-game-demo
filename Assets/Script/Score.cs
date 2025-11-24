using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public object character;
    public AudioClip winSound;
    private static int Score = 0;

    [SerializeField] string playerName = "unknow";

    private SaveData data;

    private void Awake()
    {
        data = new SaveData();
        data.LoadGame();
    }


    public void Start()
    {
        scoreText.text = "Score: " + Score;
    }

    private void OnEnable()
    {
        Unit.OnScored += AddScore;
        Unit.CheckWin += IsWin;
    }

    private void OnDisable()
    {
        Unit.OnScored -= AddScore;
        Unit.CheckWin -= IsWin;
    }

    public void AddScore(int value)
    {
        Score += value;
        scoreText.text = "Score: " + Score;
        IsWin();
    }

    public void IsWin()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && GameData.end && GameData.Level != 3)
        {
            data.SaveGame(playerName,Score);
            StartCoroutine(NextLevel());
        }
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);
        Sound.OnSound.Invoke(winSound);
        yield return new WaitForSeconds(winSound.length);
        GameData.Level++;
        SceneManager.LoadScene("Level" + GameData.Level);
    }

    public BestScores[] Load()
    {
        return data.LoadGame();
    }
}
