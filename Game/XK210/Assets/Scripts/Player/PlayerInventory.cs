
using System.Collections.Generic;

public class PlayerInventory{
    public Player player;
    public List<Item> Items { get; set; }
    public float MaxWeight{get; set; }
    public float CurrentWeight{get; set; }

    public Weapon CurrentWeapon { get; set; }

    public PlayerInventory(List<Item> Items, float MaxWeight, float CurrentWeight)
    {
        this.Items = Items;
        this.MaxWeight = MaxWeight;
        this.CurrentWeight = CurrentWeight;
    }
    public PlayerInventory()
    {
        this.Items = new List<Item>();
        this.MaxWeight = 0;
        this.CurrentWeight = 0;
    }

    public bool AddItem(Item item)
    {
        if((CurrentWeight + item.weight)>MaxWeight)
        return false;

        Items.Add(item);
        CurrentWeight += item.weight;
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if(!Items.Contains(item))
            return false;
            Items.Remove(item);
            CurrentWeight -= item.weight;
            return true;	
    }
}