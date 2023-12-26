using UnityEngine;

public class People
{
    public string name;
    public Sprite sprite;
    public Colors color;
    public People(string name, Sprite sprite, Colors color)
    {
        this.name = name;
        this.sprite = sprite;
        this.color = color;
    }
}