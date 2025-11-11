using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("Start");
    }

    public void ToggleFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }


    public void ChangedVolume(float value)
    {
        AudioListener.volume = value;
    }

}
