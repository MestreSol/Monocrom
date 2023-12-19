using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class BasicEnemy : Enemy
{
    public Transform targetPosition;
    private Seeker seeker;
    public float recognitionRange = 10f;
     public void Update(){
        // Verifica se a distância entre o inimigo e o jogador é menor que o alcance de reconhecimento
        if (Vector3.Distance(transform.position, targetPosition.position) < recognitionRange){
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }else
        {
            seeker.StopAllCoroutines();
        }
    }
     public void Start(){
        seeker = GetComponent<Seeker>();
    }

    public void OnPathComplete(Path p){
        Debug.Log("Path found. Error: " + p.error);
    }

    public int health = 100;
    
}
