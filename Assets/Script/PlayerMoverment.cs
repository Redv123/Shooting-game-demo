using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoverment : MonoBehaviour
{
    public Rigidbody2D character;
    private Vector2 moveInput;
    private SpriteRenderer sr;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string playerActionMapName = "Player";
    [SerializeField] private string moveActionName = "Move";
    [SerializeField] private string attackActionName = "Attack";
    public float speed = 5f;
    [SerializeField] private AudioClip soundEffect;
    private InputActionMap playerActionMap;
    private InputAction moveAction;
    private InputAction attackAction;
    private PlayerInput playerInput;
    private bool controlsManagedInternally;

    void Awake()
    {
        character = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        ResolveInputActions();
    }

    void OnEnable()
    {
        if (playerActionMap == null)
        {
            return;
        }

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        attackAction.performed += OnAttackPerformed;

        if (controlsManagedInternally)
        {
            playerActionMap.Enable();
        }
    }

    void OnDisable()
    {
        if (playerActionMap == null)
        {
            return;
        }

        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        attackAction.performed -= OnAttackPerformed;

        if (controlsManagedInternally)
        {
            playerActionMap.Disable();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            moveInput = Vector2.zero;
            return;
        }

        if (moveInput.x > 0.01f)
        {
            sr.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            sr.flipX = true;
        }
    }

    private void ResolveInputActions()
    {
        if (playerInput != null && playerInput.actions != null)
        {
            inputActions = playerInput.actions;
            controlsManagedInternally = false;
        }
        else
        {
            controlsManagedInternally = inputActions != null;
        }

        if (inputActions == null)
        {
            Debug.LogError("PlayerMoverment requires a PlayerInput component or an InputActionAsset reference.", this);
            return;
        }

        playerActionMap = inputActions.FindActionMap(playerActionMapName, true);
        moveAction = playerActionMap.FindAction(moveActionName, true);
        attackAction = playerActionMap.FindAction(attackActionName, true);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f || GameData.bowCount >= 4)
        {
            return;
        }

        GameData.bowCount++;
        Sound.OnSound.Invoke(soundEffect);
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<ArrowMovement>().Init(sr.flipX);
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
