
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Player player;
    public List<Item> Items { get; set; }
    public float MaxWeight{get; set; }
    public float CurrentWeight{get; set; }

    public Weapon CurrentWeapon { get; set; }
    public void Start()
    {
        CurrentWeapon = new Weapon() { category = WeaponCategory.Sword, damage = 10, weight = 1, name = "Espada" };
    }
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