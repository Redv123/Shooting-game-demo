using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private ScoreManager score;

    [SerializeField] protected int point = 0;
    protected SpriteRenderer characterSprite;
    private bool die = false;
    protected int hp;
    [SerializeField] protected AudioSource sound;
    [SerializeField] protected AudioClip destroySound;
    void Start()
    {
        characterSprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
    }

    public void Hit(int damage)
    {
        if (die == true)
        {
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            die = true;
            Die();
        }
    }

    private void Die()
    {
        characterSprite.enabled = false;
        sound.PlayOneShot(destroySound);
        Destroy(gameObject, destroySound.length);
        if (point > 0)
        {
            gameObject.tag = "Untagged";
            score.AddScore(point);
        }
    }
}
