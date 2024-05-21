using UnityEngine;

public class PlayerAtributes : MonoBehaviour{
    public float attackSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float inteligence;
    public float strength;
    public float dexterity;
    public float charisma;
    public float luck;

    public void LevelUp(){
        attackSpeed += 0.1f;
        walkSpeed += 0.1f;
        runSpeed += 0.1f;
        inteligence += 0.1f;
        strength += 0.1f;
        dexterity += 0.1f;
        charisma += 0.1f;
        luck += 0.1f;
    }    
}