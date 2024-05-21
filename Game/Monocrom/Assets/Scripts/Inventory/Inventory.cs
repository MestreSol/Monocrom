using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Cristal> cristals;
    public List<Nucleo> nucleos;
    public List<Item> items = new List<Item>();
    public float maxWeight;
    public float currentWeight;
    public GameObject itemPrefab;

    public InventoryUIController UI;
    private void Start()
    {
        instance = this;
        cristals = new List<Cristal>();
    }

    public void AddItem(Item item)
    {
        if (currentWeight + item.weight > maxWeight)
        {
            return;
        }

        items.Add(item);
        currentWeight += item.weight;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        currentWeight -= item.weight;
    }
}