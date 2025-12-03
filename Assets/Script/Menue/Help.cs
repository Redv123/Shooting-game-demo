using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private Button [] buttons = new Button[4];
    [SerializeField] private Button seleteButton;

    public void ShowHelpPanel()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
        EventSystem.current.SetSelectedGameObject(seleteButton.gameObject);

        foreach (Button btn in buttons)
        {
                btn.interactable = !btn.interactable;
        }
    }
}
