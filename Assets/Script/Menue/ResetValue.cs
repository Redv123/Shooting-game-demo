using UnityEngine;
using UnityEngine.UI;

public class isFullScreen : MonoBehaviour
{
    [SerializeField] private Toggle FullScreenToggle;
    [SerializeField] private Slider slider;

    void Awake()
    {
        FullScreenToggle.isOn = Screen.fullScreen;
        slider.value = AudioListener.volume;
    }
}
