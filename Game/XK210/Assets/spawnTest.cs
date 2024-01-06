using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTest : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 2f; // The rate at which enemies are spawned
    public float spawnIncreaseRate = 0.1f; // The rate at which spawn rate increases
    public float speedIncreaseRate = 0.1f; // The rate at which enemy speed increases
    public float strengthIncreaseRate = 0.1f; // The rate at which enemy strength increases
    public GameObject[] spawnPoints;
    private float nextSpawnTime = 0f;
    private int spawnCount = 0;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
            spawnRate -= spawnIncreaseRate;
            spawnCount++;
        }
    }

    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnPointIndex].transform.position;
        GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity);
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        enemyBase.moveSpeed += spawnCount * speedIncreaseRate;
        enemyBase.attackDamage += spawnCount * strengthIncreaseRate;
    }
}
