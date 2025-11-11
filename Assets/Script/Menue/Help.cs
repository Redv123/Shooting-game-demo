using UnityEngine;

public class Help : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    void Start()
    {
        helpPanel.SetActive(false);
    }


    public void ShowHelpPanel()
    {
        helpPanel.SetActive(true);
    }

        public void HideHelpPanel()
    {
        helpPanel.SetActive(false);
    }
}
