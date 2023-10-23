using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject configPanel;
    [SerializeField] private GameObject extraPanel;
    [SerializeField] private GameObject mainPanel;
    private void Start()
    {
        // Hide all panels except the play panel
        ShowPanel(playPanel);
    }

    public void ShowMainPanel()
    {
        ShowPanel(mainPanel);
    }
    public void ShowPlayPanel()
    {
        ShowPanel(playPanel);
    }

    public void ShowConfigPanel()
    {
        ShowPanel(configPanel);
    }

    public void ShowExtraPanel()
    {
        ShowPanel(extraPanel);
    }

    private void ShowPanel(GameObject panelToActivate)
    {
        mainPanel.SetActive(false);
        playPanel.SetActive(false);
        configPanel.SetActive(false);
        extraPanel.SetActive(false);
        panelToActivate.SetActive(true);
    }
}
