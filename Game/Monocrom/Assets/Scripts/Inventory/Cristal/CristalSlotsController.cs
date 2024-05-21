using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CristalSlotsController : MonoBehaviour
{
    public List<CristalSlot> cristalSlots;
    public GameObject SlotArea;
    public GameObject CristalPrefab;
    public GameObject CristalSelect;
    public void Awake()
    {
        cristalSlots = new List<CristalSlot>()
        {
            new CristalSlot { isLocked = false, cristal = null },
            new CristalSlot { isLocked = false, cristal = null },
            new CristalSlot { isLocked = true, cristal = null },
            new CristalSlot { isLocked = true, cristal = null },
            new CristalSlot { isLocked = true, cristal = null },
        };
    }
    public void Start()
    {
        for (int i = 0; i < cristalSlots.Count; i++)
        {
            GameObject cristal = Instantiate(CristalPrefab, SlotArea.transform);
            CristalSlot cristalSlot = cristal.GetComponent<CristalSlot>();
            cristalSlot.slotIndex = i;
            cristalSlot.cristal = cristalSlots[i].cristal;
            cristalSlot.isLocked = cristalSlots[i].isLocked;
            cristalSlot.UpdateUI();
        }
    }
    public void OpenCristalSelector(int target)
    {
        CristalSelect.SetActive(true);
        Debug.Log("Open Cristal Selector From Slot: "+ target);
        var controller = FindObjectOfType<MoreCristalInventoryController>();
        controller.UpdateList(cristalSlots[target]);
    }
}
