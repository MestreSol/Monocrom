using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("Equipment")]
    public Equipment Helmet;
    public Equipment Chest;
    public Equipment Legs;
    public Equipment Weapon;
    public Equipment Shield;
    
    public bool isEquipped = false;

    public void Equip(Equipment equipment){
        switch(equipment.equipmentType){
            case EquipmentType.Helmet:
                Helmet = equipment;
                
                break;
            case EquipmentType.Chest:
                Chest = equipment;
                break;
            case EquipmentType.Legs:
                Legs = equipment;
                break;
            case EquipmentType.Weapon:
                Weapon = equipment;
                break;
            case EquipmentType.Shield:
                Shield = equipment;
                break;
        }
    }
}