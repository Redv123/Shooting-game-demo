using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public object character;
    private AudioSource sound;
    public AudioClip winSound;

    public void Start()
    {
        sound = GetComponent<AudioSource>();
        scoreText.text = "Score:" + GameData.Score;
    }

    public void AddScore(int value)
    {
        GameData.Score += value;
        scoreText.text = "Score: " + GameData.Score;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && GameData.Level != 3)
        {
            StartCoroutine(nextLevel());
        }
    }

    private IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(2f);
        sound.PlayOneShot(winSound);
        yield return new WaitForSeconds(winSound.length);
        GameData.Level++;
        SceneManager.LoadScene("Level" + GameData.Level);
    }
}