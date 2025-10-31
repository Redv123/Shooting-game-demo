using UnityEngine;



public class ArrowMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>())
        {
            Destroy(gameObject);
            Unit unit = collision.GetComponent<Unit>();
            unit.Hit(1);
        }
    }
}
