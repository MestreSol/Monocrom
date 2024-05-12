using System.Text;
using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public static ItemTooltip Instance { get; private set; }

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text slotTypeText;
    [SerializeField] TMP_Text statsText;

    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }
    public void ShowTooltip(Item itemToShow)
    {
        if (!(itemToShow is EquippableItem))
        {
            return;
        }
        EquippableItem item = (EquippableItem)itemToShow;

        gameObject.SetActive(true);

        nameText.text = item.itemName;
        slotTypeText.text = item.EquipmentType.ToString();
        sb.Length = 0;

        AddStatText(item.StrengthBonus, "Strength");
        AddStatText(item.DexterityBonus, "Dexterity");
        AddStatText(item.ConstitutionBonus, "Constitution");
        AddStatText(item.IntelligenceBonus, "Intelligence");
        AddStatText(item.CharismaBonus, "Charisma");
        AddStatText(item.LuckBonus, "Luck");
        AddStatText(item.PerceptionBonus, "Perception");

        AddStatText(item.StrengthPercentBonus, "Strength");
        AddStatText(item.DexterityPercentBonus, "Dexterity");
        AddStatText(item.ConstitutionPercentBonus, "Constitution");
        AddStatText(item.IntelligencePercentBonus, "Intelligence");
        AddStatText(item.CharismaPercentBonus, "Charisma");
        AddStatText(item.LuckPercentBonus, "Luck");
        AddStatText(item.PerceptionPercentBonus, "Perception");
        
        statsText.text = sb.ToString();
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    private void AddStatText(float statBonus, string statName)
    {
        if (statBonus != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (statBonus > 0)
            {
                sb.Append("+");
            }
            sb.Append(statBonus);
            sb.Append(" ");
            sb.Append(statName);
        }
    }
}