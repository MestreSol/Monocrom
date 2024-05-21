using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CristalSlot : Slot
{
    public int slotIndex;
    public Cristal cristal;
    public CristalType slotCristalType;
    public Image CristalSprite;
    public TMP_Text CristalName;
    public TMP_Text CristalDescription;
    public Button button;

    public Color defaultColor = Color.white; // Cor padrão do botão
    public Color hoverColor = Color.red; // Cor do botão quando o mouse passa sobre ele

    public void Start()
    {
        button = GetComponent<Button>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Muda a cor do botão quando o mouse entra
        if (button != null)
            gameObject.GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Muda a cor do botão de volta ao normal quando o mouse sai
        if (button != null)
            gameObject.GetComponent<Image>().color = defaultColor;
    }
    public void UpdateUI()
    {
        if(cristal != null )
        {
            // Set button action
            button.onClick.AddListener(()=>FindObjectOfType<CristalSlotsController>().OpenCristalSelector(slotIndex));
            button.interactable = true;
            CristalSprite.sprite = cristal.sprite;
            CristalName.text = cristal.name;
            CristalDescription.text = cristal.description;
        }
        else if(isLocked)
        {
            button.interactable = false;
            CristalSprite.sprite = null;
            CristalName.text = "Locked Slot";
            CristalDescription.text = "";
        }
        else if(cristal == null)
        {
            // Set button action
            button.onClick.AddListener(() => FindObjectOfType<CristalSlotsController>().OpenCristalSelector(slotIndex));
            CristalSprite.sprite = null;
            CristalName.text = "Empty Slot";
            CristalDescription.text = "";
        }

    }
    public void SetCristral(Cristal cristal) { 
        if(cristal.cristalType == slotCristalType)
        {
            this.cristal = cristal;
            UpdateUI();
        }
    }
}
