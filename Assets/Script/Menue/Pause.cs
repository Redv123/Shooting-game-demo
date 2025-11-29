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
                Debug.Log("暂停成功。");

            }
            else
            {
                Time.timeScale = 1f;
                pause.SetActive(false);
                Debug.Log("解除暂停成功。");
            }
        }
    }

    void Start()
    {
        pause.SetActive(false);
        Debug.Log("初始化成功。");
    }
}
