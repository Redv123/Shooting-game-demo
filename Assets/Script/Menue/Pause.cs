using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.enterKey.wasPressedThisFrame && GameObject.FindWithTag("Player") != null)
        {
            Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
            pause.SetActive(!pause.activeSelf);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
