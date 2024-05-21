using System.Collections.Generic;
using UnityEngine;

public class MoreNucleoInventoryController : MonoBehaviour
{
	public GameObject area;
    public List<GameObject> nucleos;
    public GameObject nucleoPrefab;
    public void Start()
    {
        
    }
    public void UpdateUI(NucleoSlot slot)
	{
        nucleos.Clear();
		// Lista todos os nucles do tipo do slot
		PlayerController.instance.inventory.nucleos.ForEach(nucleo =>
        {
            if (nucleo.nucleoType == slot.nucleoType)
            {
                // Cria um novo GameObject
                GameObject nucleoObject = Instantiate(area, slot.transform.position, Quaternion.identity);
                // Adiciona o nucleo ao GameObject
                nucleoObject.GetComponent<Nucleo>().nucleoID = nucleo.nucleoID;
                // Adiciona o GameObject ao slot
                nucleoObject.transform.SetParent(slot.transform);
                // Ajusta a escala do GameObject
                nucleoObject.transform.localScale = new Vector3(1, 1, 1);
                // Ajusta a posição do GameObject
                nucleoObject.transform.position = slot.transform.position;
                nucleos.Add(nucleoObject);
            }
        });
    }
}