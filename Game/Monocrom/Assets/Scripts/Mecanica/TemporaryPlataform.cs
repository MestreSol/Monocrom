using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlataform : MonoBehaviour
{
    public float timeToDestroy = 60f;
    // Quando o jogador colidir com a plataforma, ela deve se tornar intagivel por um tempo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyPlataform());
        }
    }
    // Função que destroi a plataforma após um tempo
    private IEnumerator DestroyPlataform()
    {
        // Desativa o colisor
        yield return new WaitForSeconds(timeToDestroy);

        GetComponent<Collider2D>().enabled = false;
        
        // Troca a cor da plataforma
        GetComponent<SpriteRenderer>().color = Color.red;

        StartCoroutine(ReconstructionPlataform());
    }
    private IEnumerator ReconstructionPlataform()
    {
        

        yield return new WaitForSeconds(timeToDestroy);

        // Reativa o colisor
        GetComponent<Collider2D>().enabled = true;

        // Restaura a cor original da plataforma
        GetComponent<SpriteRenderer>().color = Color.white;

    }

}
