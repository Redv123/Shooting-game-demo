using UnityEngine;
using System.Collections;

public class flyEnemy : Unit
{
    private float speed = 6f;
    private float startPointX;
    private readonly WaitForSeconds shortWait = new(2f);
    [SerializeField] private GameObject fireball;
    private GameObject player;
    private float localTime = 0f;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        startPointX = transform.position.x;
    }

    void FixedUpdate()
    {
        localTime += Time.fixedDeltaTime;
        float x = startPointX - speed * localTime;
        float y = 3f * Mathf.Sin(2f * localTime);
        transform.position = new Vector2(x, y);
    }

    void OnBecameVisible()
    {
        StartCoroutine(Fireball());
    }

    private IEnumerator Fireball()
    {
        yield return shortWait;
        if (player && player.transform.position.x <= transform.localPosition.x)
        {
            Instantiate(fireball, transform.position, Quaternion.identity);
            StartCoroutine(Fireball());
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
