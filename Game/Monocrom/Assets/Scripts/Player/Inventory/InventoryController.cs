using System.Collections.Generic;
using UnityEngine;

public class InvetoryController: MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int space = 20;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public float Peso = 0;
    public static InvetoryController instance;
    public bool Add(Item item){
        if (!item.isDefaultItem){
            if (items.Count >= space){
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null){
                onItemChangedCallback.Invoke();
            }
        }
        return true;
    }
    public void Remove(Item item){
        items.Remove(item);
        if (onItemChangedCallback != null){
            onItemChangedCallback.Invoke();
        }
    }
}