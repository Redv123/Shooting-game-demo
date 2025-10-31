using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Rigidbody2D character;
    private Vector2 moveInput;
    private SpriteRenderer sr;
    public GameObject arrowPrefab;
    public float speed = 5f;

    public AudioSource sound;
    public AudioClip soundEffect;
    public AudioClip lose;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        character = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); // Using for the flip
        Application.targetFrameRate = 120;
        character.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Vector2.zero;
        var keyboard = Keyboard.current;

        if (keyboard == null)
        {
            return;
        }

        if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed)
        {
            moveInput.y += 1;
        }
        if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed)
        {
            moveInput.y -= 1;
        }
        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
        {
            moveInput.x -= 1;
        }
        if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
        {
            moveInput.x += 1;
        }
        moveInput = moveInput.normalized;

        if (moveInput.x > 0.01f)
        {
            sr.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            sr.flipX = true;
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            sound.PlayOneShot(soundEffect);
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<ArrowMovement>().Init(sr.flipX);
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = character.position + moveInput * speed * Time.fixedDeltaTime;

        Camera cam = Camera.main;
        if (cam != null)
        {
            float vertExtent = cam.orthographicSize;
            float horzExtent = vertExtent * cam.aspect;

            float leftBound = -horzExtent;
            float rightBound = horzExtent;
            float bottomBound = -vertExtent;
            float topBound = vertExtent;

            newPos.x = Mathf.Clamp(newPos.x, leftBound, rightBound);
            newPos.y = Mathf.Clamp(newPos.y, bottomBound, topBound);
        }


        character.MovePosition(newPos);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fireball")
        {
            Destroy(collision.gameObject);
            sr.enabled = false;
            sound.PlayOneShot(lose);
            Destroy(gameObject, lose.length);
        }
    }
}
