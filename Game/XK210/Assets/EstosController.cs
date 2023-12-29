using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EstosController : MonoBehaviour
{
    public int Usos;

    public int UsosMax;

    public List<GameObject> Objeto;

    public GameObject PrefabUsado;
    public GameObject PrefabNaoUsado;
    public void Start()
    {
        Objeto = new List<GameObject>();
        for (int i = 0; i < UsosMax; i++)
        {
            GameObject ins = new GameObject();
            if(i >= Usos)
            {
                ins = Instantiate(PrefabNaoUsado, Objeto[i].transform);
            }
            else
            {
                ins = Instantiate(PrefabUsado, Objeto[i].transform);
            }
            Objeto.Add(ins);

        }
    }
}
