using UnityEngine;

// Player Entity script
public class PlayerEntity : Entity
{
    // Player movement script
    public PlayerMovement playerMovement;

    // Entity information script
    public EntityInfo entityInfo;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize player movement
        playerMovement = AddComponent<PlayerMovement>();

        // Initialize entity information
        entityInfo = AddComponent<EntityInfo>();

        // Set player name
        entityInfo.Name = "Player";

        // Set player health
        entityInfo.Health = 100;

        // Set player mana
        entityInfo.Mana = 50;
    }
}