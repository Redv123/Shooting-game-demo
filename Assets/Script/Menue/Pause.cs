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
            if (Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
                pause.SetActive(true);

            }
            else
            {
                Time.timeScale = 1f;
                pause.SetActive(false);
            }
        }
    }
}
