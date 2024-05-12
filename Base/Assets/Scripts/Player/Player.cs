using System;
using UnityEngine;

[Serializable]
public class Player : Entity
{
    public Colors CurColor = Colors.WHITE;
  
    public float Mana = 0;

    public Weapon equipedWeapon;


}