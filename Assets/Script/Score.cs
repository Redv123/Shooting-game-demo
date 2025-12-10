using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    private TMP_Text scoreText;
    public object character;
     [SerializeField]private AudioClip winSound;
    private static int Score = 0;

    [SerializeField] private AudioClip music;

    private SaveData data;

    void Awake()
    {
        GameData.Level = 1;
        data = new SaveData();
        data.LoadGame();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
            scoreText.text = "Score: " + Score;
        }
    }

    void OnEnable()
    {
        Unit.OnScored += AddScore;
        Unit.CheckWin += IsWin;
    }

    void OnDisable()
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
        if (enemies.Length == 0 && GameData.end && GameData.Level != 2)
        {
            StartCoroutine(NextLevel());
        }
        else if (GameData.Level == 2)
        {
            Save();
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        MusicManager.Instance.StopMusic();
        Sound.OnSound.Invoke(winSound);
        yield return new WaitForSeconds(winSound.length);
        SceneManager.LoadScene("Start Menue");
        MusicManager.Instance.PlayMusic(music);
    }


    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);
        Sound.OnSound.Invoke(winSound);
        yield return new WaitForSeconds(winSound.length);
        GameData.Level++;
        SceneManager.LoadScene("Level" + GameData.Level);
        // For readability
        scoreText.color = Color.white;
    }

    public void Save()
    {
        data.SaveGame(GameData.playerName, Score);
    }

    public BestScores[] Load()
    {
        return data.LoadGame();
    }
}