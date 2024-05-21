using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCristalInventoryController : MonoBehaviour
{
    public GameObject area;
    public GameObject cristalPref;
    public List<GameObject> cristals;
    public void Start()
    {
        cristals = new List<GameObject>();
    }
    public void UpdateList(CristalSlot slotTarget)
    {
        // Limpa a lista de cristais
        cristals.ForEach(cristal =>
        {
            Destroy(cristal);
        });
        // Pega a lista de cristais do player e exibe na UI apenas os cristais daquele slot
        if (PlayerController.instance.inventory.cristals.Count > 0)
        {
            PlayerController.instance.inventory.cristals.ForEach(cristal =>
            {
                if (cristal.cristalType == slotTarget.slotCristalType)
                {
                    GameObject cristalObj = Instantiate(cristalPref, area.transform);
                    CristalSlot cristalSlot = cristalObj.GetComponent<CristalSlot>();
                    cristals.Add(cristalObj);
                    cristalSlot.cristal = cristal;
                    cristalSlot.UpdateUI();
                }
            });
        }

    }
}
