using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class BasicEnemy : Enemy
{
    public Transform targetPosition;
    private Seeker seeker;
    public void Start(){
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }
    public void OnPathComplete(Path p){
        Debug.Log("Path found. Error: " + p.error);
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public int health = 100;
    
}
