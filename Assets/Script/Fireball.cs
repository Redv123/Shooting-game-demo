using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 15f;
    private Vector3 tager;
    private Vector3 direction;

    void Start()
    {
        Vector3 start = transform.position;
        tager = GameObject.FindWithTag("Player").transform.position;
        direction = (tager - start).normalized;
        transform.right = -direction;
    }

    void FixedUpdate()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
