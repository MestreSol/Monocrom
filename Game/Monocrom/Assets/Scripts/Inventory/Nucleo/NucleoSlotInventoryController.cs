using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NucleSlotController : MonoBehaviour
{
    public NucleoSlot nucleoSlot;
    public Image Image;
    public TMP_Text title;
    public TMP_Text description;

    public void SetSlot(NucleoSlot slot)
    {
        nucleoSlot = slot;
        Image.sprite = slot.nucleo.sprite;
        title.text = slot.nucleo.name;
        description.text = slot.nucleo.Description;

    }
}
