using UnityEngine;
using System.Collections.Generic;

public class UI_MainMenuController : MonoBehaviour
{
    public GameObject PanelInicial;
    public GameObject Jogar;
    public GameObject Configurar;
    public GameObject Extras;

    private void Start()
    {
        ShowPanel(PanelInicial);
    }

    public void ShowPanel(string panelName)
    {
        switch (panelName)
        {
            case "Jogar":
                ShowPanel(Jogar);
                break;
            case "Configurar":
                ShowPanel(Configurar);
                break;
            case "Extras":
                ShowPanel(Extras);
                break;
            default:
                ShowPanel(PanelInicial);
                break;
        }
    }

    public void ShowPanel(GameObject panelToActivate)
    {
        HideAll();
        panelToActivate.SetActive(true);
    }

    private void HideAll()
    {
        PanelInicial.SetActive(false);
        Jogar.SetActive(false);
        Configurar.SetActive(false);
        Extras.SetActive(false);
    }
    public void Sair()
    {
        Application.Quit();
    }
}