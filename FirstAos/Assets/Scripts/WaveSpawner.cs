using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform enemyPrefab2;

    public Transform spawnPoint;

    //public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 1;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = 10f;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        for (int j = 0; j < 2; j++)
        {
            SpawnEnemy2();
            yield return new WaitForSeconds(0.5f);
        }
        //waveIndex++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemy2()
    {
        Instantiate(enemyPrefab2, spawnPoint.position, spawnPoint.rotation);
    }
}
