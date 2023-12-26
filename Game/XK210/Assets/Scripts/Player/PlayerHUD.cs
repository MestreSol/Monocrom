using TMPro;

public class PlayerHUD
{
    public Player player;

    public TMP_Text text;

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