using UnityEngine;

public class BallonAction : Unit
{
    public float speed = 7f;
    public bool move = true;

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (move)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
