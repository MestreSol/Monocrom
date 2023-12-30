using JetBrains.Annotations;
using UnityEngine;

public class PlayerActions: MonoBehaviour
{
    public Player player;
    public void UseHeal()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (player.hud.estosController.Usos > 0)
            {
                player.Healing(player.forca * player.energia);
                player.hud.estosController.Redus();
            }

        }
    }
}