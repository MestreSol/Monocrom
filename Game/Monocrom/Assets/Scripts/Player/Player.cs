using System;
using UnityEngine;

[Serializable]
public class Player : Entity
{
    public Colors CurColor = Colors.WHITE;
    public int head = 0;
    public int hair = 0;
    public int body = 0;
    public int legs = 0;
    public int Accessory = 0;
    public int Preset = 0;

    public float Mana = 0;

    // Método para alterar a cor do jogador
    public void ChangeColor(Colors newColor)
    {
        CurColor = newColor;
    }

    
    // Método para alterar a aparência do jogador
    public void ChangeAppearance(int newHead, int newHair, int newBody, int newLegs, int newAccessory, int newPreset)
    {
        head = newHead;
        hair = newHair;
        body = newBody;
        legs = newLegs;
        Accessory = newAccessory;
        Preset = newPreset;
    }

    // Método para alterar a mana do jogador
    public void ChangeMana(float newMana)
    {
        Mana = newMana;
    }
}