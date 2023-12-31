using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartNewGame : MonoBehaviour
{
    public TMP_InputField saveName;
    public TMP_InputField titulo;
    public Animator anim;

    public void NewSave(int id)
    {
        if (saveName.text != "" && titulo.text != "")
        {
            GameDatabase.CreateSave(id.ToString(), saveName.text, titulo.text);
            Debug.Log("Save Criado");
            GameManager.instance.LoadScene("StartGame");
        }
        else
        {
            Debug.Log("SaveName or Titulo is empty");
        }
    }
}
