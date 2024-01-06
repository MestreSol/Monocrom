using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Player player;

    public TMP_Text text;

    public Slider Life;
    public TMP_Text MaxLife;
    public TMP_Text CurLife;

    public Slider Postura;
    public TMP_Text MaxPostura;
    public TMP_Text CurPostura;

    public Slider Stamina;
    public TMP_Text MaxStamina;
    public TMP_Text CurStamina;

    public Slider ToLevel;

    public TMP_Text Esperanca;
    public TMP_Text Cristais;
    public TMP_Text Level;

    public Slider Exp;

    public EstosController estosController;

    private void Start()
    {
        Life.maxValue = player.maxLife;
        MaxLife.text = player.maxLife.ToString();

        Postura.maxValue = player.maxPosture;
        MaxPostura.text = player.maxPosture.ToString();
        
        Stamina.maxValue = player.maxStamina;
        MaxStamina.text = player.maxStamina.ToString();

        Exp.maxValue = player.PontosParaProxNivel;

        player.OnLifeChanged += UpdateLifeUI;
        player.OnPostureChanged += UpdatePosturaUI;
        player.OnStaminaChanged += UpdateStaminaUI;

        UpdateLifeUI(player.Life);
        UpdatePosturaUI(player.Posture);
        UpdateStaminaUI(player.Stamina);

        UpdateXPUI(player.XP);
        UpdatePontosUI();
        UpdateCristalUI();
        UpdateLevel();
    }
    public void UpdateLevel()
    {
        Level.text = player.Nivel.ToString();
        Debug.Log("Update level in HUD to: "+ player.Nivel.ToString());
    }
    public void UpdatePontosUI()
    {
        Level.text = player.Nivel.ToString();
        ToLevel.value = player.XP;
        UpdateXPUI(player.XP);
        Debug.Log("Update Pontos in HUD to: " + player.XP);
    }
    public void UpdateCristalUI()
    {
        Cristais.text = player.Cristais.ToString();
        Debug.Log("Update Cristais in HUD to: " + player.Cristais.ToString());
    }
    public void UpdateXPUI(float esperanca)
    {
        Esperanca.text = esperanca.ToString();
        Exp.value = esperanca;
        Debug.Log("Update Esperanca in HUD to: " + esperanca.ToString());
    }
    private void UpdateLifeUI(float life)
    {
        Life.value = life;
        CurLife.text = life.ToString();
        Debug.Log("Update Life in HUD to: " + life.ToString());
    }

    private void UpdatePosturaUI(float postura)
    {
        Postura.value = postura;
        CurPostura.text = postura.ToString();
        Debug.Log("Update Postura in HUD to: " + postura.ToString());
    }

    private void UpdateStaminaUI(float stamina)
    {
        Stamina.value = stamina;
        CurStamina.text = stamina.ToString();
        Debug.Log("Update Stamina in HUD to: " + stamina.ToString());
    }

    public void ShowMessage(string message)
    {
        text.text = message;
        text.enabled = true;
        Debug.Log("Show message in HUD: " + message);
    }

    public void HideMessage()
    {
        text.enabled = false;
        Debug.Log("Hide message in HUD");
    }
}
