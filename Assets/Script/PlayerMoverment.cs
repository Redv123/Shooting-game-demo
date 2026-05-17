using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoverment : MonoBehaviour
{
    public Rigidbody2D character;
    private Vector2 moveInput;
    private SpriteRenderer sr;
    [SerializeField] private ArrowShooter arrowShooter;
    public float speed = 5f;

    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        arrowShooter = arrowShooter != null ? arrowShooter : GetComponent<ArrowShooter>();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = Vector2.zero;
        Keyboard keyboard = Keyboard.current;

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

        if ((keyboard.spaceKey.wasPressedThisFrame || keyboard.jKey.wasPressedThisFrame) && arrowShooter != null)
        {
            // ArrowShooter handles pooling and the active arrow limit.
            arrowShooter.TryShoot(transform.position, sr.flipX);
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
}
