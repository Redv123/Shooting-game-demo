using UnityEngine;

public class Fireball : MonoBehaviour
{
        private float speed = 15f;

    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
