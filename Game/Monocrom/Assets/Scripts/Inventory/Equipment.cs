using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Legs,
    Weapon,
    Shield,
}
// Classe escriptavel que contem um sprite e um tipo de equipamento
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipmentType;

    public float damageModifier;
    public float armorModifier;

    

    
}