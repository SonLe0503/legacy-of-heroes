using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;        // Các enemy prefab để spawn
    public Transform[] spawnPoints;          // Các điểm spawn đặt sẵn trong scene
    public Transform player;                 // Player (kéo vào từ Hierarchy)
    public float spawnDistance = 10f;        // Khoảng cách để kích hoạt spawn

    private bool[] hasSpawned;               // Theo dõi điểm nào đã spawn

    void Start()
    {
        // Khởi tạo mảng theo số lượng spawn points
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
