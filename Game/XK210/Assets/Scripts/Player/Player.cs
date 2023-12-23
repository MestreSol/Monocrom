[System.Serializable]
public class Player : Entity
{
    public PlayerProgress progress;
    public PlayerState state;
    public PlayerMovement movement;
    public PlayerCombat combat;
    public PlayerAnimation animation;
    public PlayerInput input;
    public PlayerHealth health;
    public PlayerInventory inventory;

    public Player()
    {
        progress = new PlayerProgress();
        state = new PlayerState();
        movement = new PlayerMovement();
        combat = new PlayerCombat();
        animation = new PlayerAnimation();
        input = new PlayerInput();
        health = new PlayerHealth();
        inventory = new PlayerInventory();
    }
}