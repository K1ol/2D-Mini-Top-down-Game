using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }


    [Header("Enemy spawn point")]
    public Transform[] spawnPoints;

    [Header("Enemies in round")]
    public List<EnemyWave> enemyWaves;

    public int currentWaveIndex = 0;

    public int enemyCount = 0;

    private int hasBorn = 0;

    public int maxCount = 0;

    private void Awake()
    {
        Instance = this;
    }



    private void Update()
    {
        if (enemyCount == 0 && GameManager.instance.currentGameState == GameState.inGame)
        {
            StartCoroutine(nameof(startNextWaveCoroutine));
        }
        
    }

  

    //public IEnumerator GenerateEnemy()
    //{
        
       // yield return new WaitForSeconds(2f);
      //  if (hasBorn <= maxCount)
       // {
        //    GameObject enemy = Instantiate(enemyPrefabs.enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        //    hasBorn++;
       // }
        
   // }


    private IEnumerator startNextWaveCoroutine()
    {
        if (currentWaveIndex >= enemyWaves.Count)
            yield break;

        List<EnemyData> enemies = enemyWaves[currentWaveIndex].enemies;


        foreach (EnemyData enemyData in enemies)
        {
            for(int i=0;i< enemyData.waveEnemyCount; i++)
            {
                GameObject enemy = Instantiate(enemyData.enemyPrefab, GetRandomSpawnPoint(),Quaternion.identity);
                hasBorn++;
                //Debug.Log("Generate enemy successfully!!!!");
                yield return new WaitForSeconds(enemyData.spawnInterval);
            }

            currentWaveIndex++;
        }

    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
}



[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab;
    public float spawnInterval;//enemy spawn interval
    public float waveEnemyCount;
}


[System.Serializable]
public class EnemyWave
{
    public List<EnemyData> enemies;//enemies of each wave
}