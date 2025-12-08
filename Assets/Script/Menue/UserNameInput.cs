using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserNameInput : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] TMP_InputField input;
    [SerializeField] GameObject warning;
    public void Input()
    {
        btn.interactable = !string.IsNullOrWhiteSpace(input.text);
        warning.SetActive(input.text.Length == 8);
    }


    public void Confirm()
    {
        GameData.playerName = input.text;
        SceneManager.LoadScene("Start Menue");
    }
}
