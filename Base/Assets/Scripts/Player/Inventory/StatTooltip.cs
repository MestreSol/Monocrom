using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StatTooltip : MonoBehaviour
{
    public static StatTooltip Instance;

    [SerializeField] TMP_Text statNameText;
    [SerializeField] TMP_Text finalValueText;
    [SerializeField] TMP_Text modifiersListText;
    [SerializeField] StatType statType;
    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        gameObject.SetActive(false);
    }

    public void ShowTooltip(EntityStatus stat, StatType statName)
    {
        gameObject.SetActive(true);

        finalValueText.text = GetValueText(stat, statName);
        modifiersListText.text = GetModifiersText(stat, statName);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private string GetValueText(EntityStatus stat, StatType statType)
    {
        sb.Length = 0;

        sb.Append(statType.ToString());
        sb.Append(" (");
        sb.Append(stat.GetBaseValue(statType));
        sb.Append(" + ");
        sb.Append((float)System.Math.Round(stat.GetBaseValue(statType) - stat.GetValue(statType), 4));
        sb.Append(")");

        return sb.ToString();
    }

    private string GetModifiersText(EntityStatus stat, StatType statType)
    {
        sb.Length = 0;

        
        for (int i = 0; i < stat.GetStat(statType).StatModifiers.Count; i++)
        {
            StatModifier mod = stat.GetStat(statType).StatModifiers[i];

            sb.Append(((Item)mod.Source).name);
            sb.Append(": ");

            if (mod.Value > 0)
            {
                sb.Append("+");
            }

            if (mod.Type == StatModifierType.Flat)
            {
                sb.Append(mod.Value);
            }
            else
            {
                sb.Append(mod.Value * 100);
                sb.Append("%");
            }

            if (i < stat.StatModifiers.Count - 1)
            {
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }

    private string FirstLetterToUpper(string s)
    {
        if (string.IsNullOrEmpty(s))
            return null;

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}