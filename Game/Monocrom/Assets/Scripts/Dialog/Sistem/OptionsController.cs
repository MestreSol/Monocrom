using System.Collections.Generic;
using UnityEngine;
public class OptionsController : MonoBehaviour{
    public void ShowOptions(){
        gameObject.SetActive(false);
            
    }
    public void ShowOptions(List<DialogueOptions> list)
    {
        gameObject.SetActive(true);
    }
}