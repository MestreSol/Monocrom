using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Entity
{
    public Transform target;
    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool soothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10.0f;
    public bool staticOffset = false;
    public float moveSpeed = 2.0f; // Variable to control enemy's move speed
    public float attackDamage = 0f;
    private void LateUpdate()
    {
        target = FindClosestPlayer();
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y), Time.deltaTime * moveSpeed);
    }

    public Transform FindClosestPlayer()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = player;
                distance = curDistance;
            }
        }
        return closest.transform;
    }

    public void Dead()
    {
        target.GetComponent<Player>().XP += 1;
        target.GetComponent<Player>().hud.UpdatePontosUI();
    }
    public void Update()
    {
        if (Life < 0)
        {
            Dead();
            Destroy(gameObject);
        }
        if(nier)
        {
            target.GetComponent<Player>().Life -= attackDamage;
        }
    }
    
    public bool nier = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nier = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            nier = false;
        }
    }
}
