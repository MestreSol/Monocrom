using UnityEngine;

[System.Serializable]
public class People {
    public string name;
    public Sprite sprite;
    public Color color;
    public People(string name, Sprite sprite, Color color){
        this.name = name;
        this.sprite = sprite;
        this.color = color;
    }
}