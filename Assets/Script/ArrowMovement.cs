using UnityEngine;
using System;

public class ArrowMovement : MonoBehaviour
{
    public Rigidbody2D arrow;
    [SerializeField] private float speed = 7f;
    private int direction;
    private SpriteRenderer sr;
    private Action<ArrowMovement> releaseArrow;
    private bool isReleased;

    void Awake()
    {
        arrow = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(bool flipX, Action<ArrowMovement> releaseCallback)
    {
        // The shooter owns the pool, so arrows call this when they are finished.
        releaseArrow = releaseCallback;
        isReleased = false;

        if (flipX)
        {
            direction = -1;
            sr.flipX = true;
        }
        else
        {
            direction = 1;
            sr.flipX = false;
        }
    }

    void OnBecameInvisible()
    {
        Release();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit)
        {
            unit.Hit(1);
            Release();
        }
    }

    private void Release()
    {
        // Collision and visibility callbacks can happen close together.
        if (isReleased)
        {
            return;
        }

        isReleased = true;
        releaseArrow?.Invoke(this);
    }
}
