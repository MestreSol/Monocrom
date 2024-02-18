using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text NameText;
    public Text ValueText;

    [NonSerialized]
    public EntityStatus Stat;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        NameText = texts[0];
        ValueText = texts[1];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string pointName = NameText.text;
        StatType statType = (StatType)Enum.Parse(typeof(StatType), pointName);
        StatTooltip.Instance.ShowTooltip(Stat, statType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StatTooltip.Instance.HideTooltip();
    }
}