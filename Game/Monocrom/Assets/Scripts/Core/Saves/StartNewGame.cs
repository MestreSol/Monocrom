using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartNewGame : MonoBehaviour
{
    public TMP_InputField SaveName;
    public TMP_InputField Titulo;
    public void NewSave(int id)
    {
        if (SaveName.text != "" && Titulo.text != "")
        {
            GameDatabase.CreateSave(id.ToString(), SaveName.text, Titulo.text);
            Debug.Log("Save Criado");
            GameManager.instance.LoadScene("StartGame");
        }
        else
        {
            Debug.Log("SaveName or Titulo is empty");
        }
    }
}
