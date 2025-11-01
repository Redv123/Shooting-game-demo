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


    public void Start()
    {
        scoreText.text = "Score:" + Score;
    }

    private void OnEnable()
    {
        Unit.OnScored += AddScore;
    }

    private void OnDisable()
    {
        Unit.OnScored -= AddScore;
    }
    
    public void AddScore(int value)
    {
        Score += value;
        scoreText.text = "Score: " + Score;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && GameData.Level != 3)
        {
            StartCoroutine(nextLevel());
        }
    }

    private IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(2f);
        Sound.OnSound.Invoke(winSound);
        yield return new WaitForSeconds(winSound.length);
        GameData.Level++;
        SceneManager.LoadScene("Level" + GameData.Level);
    }
}