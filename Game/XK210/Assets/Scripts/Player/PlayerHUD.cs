using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Player player;

    public TMP_Text text;

    public Slider Life;
    public Slider Postura;

    private void Start()
    {
        Life.maxValue = player.maxLife;
        Postura.maxValue = player.maxPostura;
    }

    private void Update()
    {
        Life.value = player.life;
        Postura.value = player.postura;
    }
    public void ShowMessage(string message)
    {
        text.text = message;
        text.enabled = true;
    }

    public void HideMessage()
    {
        text.enabled = false;
    }
}