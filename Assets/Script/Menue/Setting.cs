using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{

    public void Confirm()
    {
        SceneManager.LoadScene("Start Menue");
    }


    public void ChangedVolume(float value)
    {
        AudioListener.volume = value;
    }

}
