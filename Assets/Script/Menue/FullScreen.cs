using UnityEngine;

public class FullScreen : MonoBehaviour
{
    public void ToggleFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
}
