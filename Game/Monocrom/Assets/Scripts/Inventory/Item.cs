using UnityEngine;

public enum RarityType
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public string Description;
    public int Value;
    public bool isStackable;
    public int maxStackSize;    
    public RarityType rarity;
    public bool isCraftable;
    public bool isConsumable;
    public bool isEquippable;
    public bool isQuestItem;
    public float weight;

    public void Use()
    {
        Debug.Log("Using " + Name);
    }

    public virtual void Equip()
    {
        Debug.Log("Equipping " + Name);
    }

    public virtual void Unequip()
    {
        Debug.Log("Unequipping " + Name);
    }

    public virtual void Drop()
    {
        Debug.Log("Dropping " + Name);
    }

    public virtual void Sell()
    {
        Debug.Log("Selling " + Name);
    }

    public virtual void Consume()
    {
        Debug.Log("Consuming " + Name);
    }

    public virtual void Craft()
    {
        Debug.Log("Crafting " + Name);
    }

    public virtual void Quest()
    {
        Debug.Log("Questing " + Name);
    }

    public virtual void Stack()
    {
        Debug.Log("Stacking " + Name);
    }

}
