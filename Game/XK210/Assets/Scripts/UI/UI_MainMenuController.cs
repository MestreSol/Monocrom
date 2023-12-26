using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenuController : MonoBehaviour
{
    public GameObject panelInicial;
    public GameObject jogar;
    public GameObject configurar;
    public GameObject extras;

    private void Start()
    {
        ShowPanel(panelInicial);
    }

    public void ShowPanel(string panelName)
    {
        switch (panelName)
        {
            case "Jogar":
                ShowPanel(jogar);
                break;
            case "Configurar":
                ShowPanel(configurar);
                break;
            case "Extras":
                ShowPanel(extras);
                break;
            default:
                ShowPanel(panelInicial);
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
        panelInicial.SetActive(false);
        jogar.SetActive(false);
        configurar.SetActive(false);
        extras.SetActive(false);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
