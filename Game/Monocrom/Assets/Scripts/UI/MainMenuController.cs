using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painel{
    public GameObject panel;
    public string name;
    public Painel(GameObject panel, string name){
        this.panel = panel;
        this.name = name;
    }
}
public class MainMenuController : MonoBehaviour
{
    // Lista de painéis do menu principal
    [SerializeField] private List<Painel> Paineis;

    // No início do jogo, mostramos o primeiro painel da lista
    private void Start()
    {
        ShowPanel(Paineis[0].panel);
    }

    // Método para mostrar um painel específico baseado no nome
    public void ShowPanel(string panelName)
    {
        // Percorremos todos os painéis
        foreach (var panel in Paineis)
        {
            // Se o nome do painel corresponder ao nome fornecido, ativamos o painel
            // Caso contrário, desativamos o painel
            panel.panel.SetActive(panel.name == panelName);
        }
    }

    // Método para mostrar um painel específico baseado no GameObject
    private void ShowPanel(GameObject panelToActivate)
    {
        // Percorremos todos os painéis
        foreach (var panel in Paineis)
        {
            // Se o GameObject do painel corresponder ao GameObject fornecido, ativamos o painel
            // Caso contrário, desativamos o painel
            panel.panel.SetActive(panel.panel == panelToActivate);
        }
    }

    // Método para sair do jogo
    public void Sair()
    {
        // Sai do jogo
        Application.Quit();
    }
}
