using System.Collections.Generic;
using UnityEngine;

public class EstosController : MonoBehaviour
{
    public int Usos;

    public int UsosMax;

    public List<GameObject> Objeto;

    public GameObject PrefabUsado;
    public void Redus()
    {
        Usos--;
        Objeto[Usos].GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
    public void RecuperarTudo()
    {
        Usos = UsosMax;
        for (int i = 0; i < UsosMax; i++)
        {
            Objeto[i].GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }
    public void Start()
    {
        Objeto = new List<GameObject>();
        float x = this.transform.position.x-2.3f;
        for (int i = 0; i < UsosMax; i++)
        {
            Vector3 position = new Vector3(i+x, this.transform.position.y, this.transform.position.z);
            GameObject ins;
            if (i >= Usos)
            {
                ins = Instantiate(PrefabUsado, position, Quaternion.identity, this.transform);
                ins.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
            else
            {
                ins = Instantiate(PrefabUsado, position, Quaternion.identity, this.transform);
                ins.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
            Objeto.Add(ins);
        }
    }
}
