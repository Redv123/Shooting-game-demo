using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    private float value;
    private bool fullScreen;

    void Awake()
    {
        value = AudioListener.volume;
        fullScreen = Screen.fullScreen;
    }
    public void SettingReset()
    {
        AudioListener.volume = value;
        Screen.fullScreen = fullScreen;
        SceneManager.LoadScene("Start");
        
    }
}
