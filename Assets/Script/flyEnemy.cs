using UnityEngine;
using System.Collections;

public class flyEnemy : Unit
{
    private float speed = 8f;
    private float startPointX;
    private readonly WaitForSeconds shortWait = new(2f);
    [SerializeField] private GameObject fireball;
    private float localTime = 0f;
    void Start()
    {
        startPointX = transform.position.x;
    }

    void FixedUpdate()
    {
        localTime += Time.fixedDeltaTime;
        float x = startPointX - speed * localTime;
        float y = 2f * Mathf.Sin(2f * localTime);
        transform.position = new Vector2(x, y);
    }

    void OnBecameVisible()
    {
        StartCoroutine(Fireball());
    }

    private IEnumerator Fireball()
    {
        yield return shortWait;
        Instantiate(fireball, transform.position, Quaternion.identity);
        StartCoroutine(Fireball());
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
