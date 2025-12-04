using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard.enterKey.wasPressedThisFrame)
        {
            Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
            pause.SetActive(!pause.activeSelf);
        }
    }
}
