using UnityEngine;

[System.Serializable]
public class Entity
{
    public Rigidbody2D Rigidbody2D { get; }
    public float Speed { get; }

    public float Life = 0;

    public int Forca = 0;
    public int Destresa = 0;
    public int Sorte = 0;
    public int Persepcao = 0;
    public int Constituicao = 0;
    public int Inteligencia = 0;
    public int Carisma = 0;
    public int Energia = 0;

    public float CalcularDefesa()
    {
        return 0;
    }


}