using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;        
    public Transform[] spawnPoints;          
    public Transform player;                
    public float spawnDistance = 10f;        

    private bool[] hasSpawned;               

    void Start()
    {
       
        hasSpawned = new bool[spawnPoints.Length];
    }

    void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!hasSpawned[i])
            {
                float distance = Vector3.Distance(player.position, spawnPoints[i].position);

                if (distance <= spawnDistance)
                {
                    SpawnEnemyAt(i);
                    hasSpawned[i] = true;
                }
            }
        }
    }

    void SpawnEnemyAt(int index)
    {
        if (enemyPrefabs.Length == 0) return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[index].position, Quaternion.identity);
    }
}
