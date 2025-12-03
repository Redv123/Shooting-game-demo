using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private float volume;
    private bool fullScreen;

    void Awake()
    {
        volume = AudioListener.volume;
        fullScreen = Screen.fullScreen;
    }
    public void SettingReset()
    {
        AudioListener.volume = volume;
        Screen.fullScreen = fullScreen;
        SceneManager.LoadScene("Start");
    }
}
