using UnityEngine;

public class BallonAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource sound;
    public AudioClip destroySound;
    private SpriteRenderer sprite;
    public ScoreManager score;

    private bool isDestroy = false;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Destroyed()
    {
        if (isDestroy == false)
        {
            gameObject.tag = "Untagged";
            sprite.enabled = false;
            isDestroy = true;
            sound.PlayOneShot(destroySound);
            score.AddScore(1);
            Destroy(gameObject, destroySound.length);
        }
    }
}
