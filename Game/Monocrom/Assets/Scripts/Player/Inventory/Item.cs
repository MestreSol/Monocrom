using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";  // Nome do item
    public Sprite icon = null;             // Ícone do item
    public bool isDefaultItem = false;     // É um item padrão?

    // Método chamado quando o item é usado
    public virtual void Use()
    {
        // Use o item
        // Algo como
        // PlayerHealth.instance.Heal(healAmount);
    }

    // Método para remover o item do inventário
    public void RemoveFromInventory()
    {
        InvetoryController.instance.Remove(this);
    }
}