using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerUpPrefab;

    [SerializeField] private int enemyCount;
    [SerializeField] private int waweCount = 1;

    [SerializeField] private float spawnRange = 9;

    private Vector3 spawnPos;

    void Start()
    {
        SpawnEnemyWawe(waweCount);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            waweCount++;
            SpawnEnemyWawe(waweCount);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWawe(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        spawnPos = new Vector3(spawnPosX, 0.1f, spawnPosZ);

        return spawnPos;
    }
}
