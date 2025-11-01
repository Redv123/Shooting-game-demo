using UnityEngine;
using System.Collections;

public class flyEnemy : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int speed = 6;
    private float startPointX;
    [SerializeField] private GameObject fireball;
    void Start()
    {
        startPointX = transform.position.x;
        StartCoroutine(Fireball());
    }

    void FixedUpdate()
    {
        float x = startPointX - speed * Time.fixedTime;
        float y = 4f * Mathf.Sin(2f * Time.fixedTime);
        transform.position = new Vector2(x, y);
    }


    private IEnumerator Fireball()
    {
        Instantiate(fireball, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Fireball());
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
