using UnityEngine;

public class Item
{
    public string name { get; set; }
    public float value { get; set; }
    public float price { get; set; }
    public string description { get; set; }
    public Sprite icon { get; set; }

    public Category category { get; set; }
    public float weight { get; set; }
}