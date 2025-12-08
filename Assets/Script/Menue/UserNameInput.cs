using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserNameInput : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] TMP_InputField input;
    public void Confirm()
    {
        GameData.playerName = input.text;
        SceneManager.LoadScene("Start Menue");
    }

    public void Input()
    {
        btn.interactable = input.text != "";
    }
}
