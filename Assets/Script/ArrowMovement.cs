using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Rigidbody2D arrow;
    public float speed = 7f;
    private int direction;
    private SpriteRenderer sr;

    void Start()
    {
        arrow = GetComponent<Rigidbody2D>();
    }

    public void Init(bool flipX)
    {
        sr = GetComponent<SpriteRenderer>();
        if (flipX)
        {
            direction = -1;
            sr.flipX = true;
        }
        else
        {
            direction = 1;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime * direction);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit)
        {
            Destroy(gameObject);
            unit.Hit(1);
        }
    }
}
