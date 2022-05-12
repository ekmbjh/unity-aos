using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform adEnemyPrefab;
    public Transform apEnemyPrefab;
    public Transform canonEnemyPrefab;
    public Transform adEnemyPrefabBlue;
    public Transform apEnemyPrefabBlue;
    public Transform canonEnemyPrefabBlue;

    public Transform redTeamSpawnPoint;
    public Transform blueTeamSpawnPoint;

    //public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    //private int waveIndex = 1;

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
            SpawnAdEnemy();
            yield return new WaitForSeconds(0.8f);
        }

        //for (int j = 0; j < 2; j++)
        //{
        //    SpawnApEnemy();
        //    yield return new WaitForSeconds(1f);
        //}

        //for (int k = 0; k < 1; k++)
        //{
        //    SpawnCanonEnemy();
        //    yield return new WaitForSeconds(1f);
        //}
    }

    void SpawnAdEnemy()
    {
        Instantiate(adEnemyPrefab, redTeamSpawnPoint.position, redTeamSpawnPoint.rotation);
        Instantiate(adEnemyPrefabBlue, blueTeamSpawnPoint.position, blueTeamSpawnPoint.rotation);
    }

    void SpawnApEnemy()
    {
        Instantiate(apEnemyPrefab, redTeamSpawnPoint.position, redTeamSpawnPoint.rotation);
        Instantiate(apEnemyPrefabBlue, blueTeamSpawnPoint.position, blueTeamSpawnPoint.rotation);
    }

    void SpawnCanonEnemy()
    {
        Instantiate(canonEnemyPrefab, redTeamSpawnPoint.position, redTeamSpawnPoint.rotation);
        Instantiate(canonEnemyPrefabBlue, blueTeamSpawnPoint.position, blueTeamSpawnPoint.rotation);
    }
}
