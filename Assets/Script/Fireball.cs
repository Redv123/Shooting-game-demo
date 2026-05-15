using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fireball : MonoBehaviour
{
    private float speed = 15f;
    private Vector3 tager;
    private Vector3 direction;
    [SerializeField] private AudioClip loseMusic;
    [SerializeField] private AudioClip newMusic;

    void Start()
    {
        Vector3 start = transform.position;
        tager = GameObject.FindWithTag("Player").transform.position;
        direction = (tager - start).normalized;
        transform.right = -direction; // Look at the player
    }

    void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * direction; // Direction still is a unit vector. 
    }

    void OnBecameInvisible()
    {
        if (!GameData.isGameOver)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit && !GameData.isGameOver)
        {
            GameData.isGameOver = true;
            GetComponent<SpriteRenderer>().enabled = false;
            speed = 0;
            unit.Hit(1);
            FindAnyObjectByType<ScoreManager>().Save(); // Save the score if player die
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        MusicManager.Instance.PlayMusic(loseMusic);
        yield return new WaitForSeconds(loseMusic.length);
        SceneManager.LoadScene("Start Menue");
        MusicManager.Instance.PlayMusic(newMusic);
        Destroy(gameObject);
    }
}
