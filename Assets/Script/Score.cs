using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class ScoreManager : MonoBehaviour
{
    private TMP_Text scoreText;
    public object character;
    [SerializeField] private AudioClip winSound;
    private static int Score;
    [SerializeField] private AudioClip music;
    private bool win = false;
    private SaveData data;
    void Awake()
    {
        GameData.Level = 1;
        data = new SaveData();
        data.LoadGame();
        Score = 0;
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
    }

    public void IsWin()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0 || win || !GameData.end)
        {
            return;
        }
        win = true;
        if (GameData.Level == 1)
        {
            GameData.Level++;
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
        Destroy(gameObject);
    }


    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);
        Sound.OnSound.Invoke(winSound);
        yield return new WaitForSeconds(winSound.length);
        SceneManager.LoadScene("Level" + GameData.Level);
        GameData.end = false;
        // For readability
        scoreText.color = Color.white;
        win = false;
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
