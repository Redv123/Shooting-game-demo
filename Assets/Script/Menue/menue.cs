using UnityEngine;
using UnityEngine.SceneManagement;
public class menue : MonoBehaviour
{
    public void StartClicked()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
