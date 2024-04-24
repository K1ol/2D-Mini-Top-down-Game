using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager Instance;
    [Header("Obstacles spawn point")]
    public Transform[] spawnPoints;
    public ObstaclesData obstaclesPref;
    public int maxObjectsToSpawn = 15;
    public List<int> usedIndex = new List<int>();

    void Awake()
    {
        Instance = this;
    }


   

    public void StartObs()
    {
        for (int i = 0; i < maxObjectsToSpawn; i++)
        {
            GameObject obstacle1 = Instantiate(obstaclesPref.obstaclesPref1, GetRandomSpawnPoint(), Quaternion.identity);
            GameObject obstacle2 = Instantiate(obstaclesPref.obstaclesPref2, GetRandomSpawnPoint(), Quaternion.identity);
            //Debug.Log("Generate successfully!!!!");

        }

        AstarPath.active.Scan();

    }


    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnPoints.Length);
        } while (usedIndex.Contains(randomIndex));

        // �ҵ�һ��δ��ʹ�õ������������ӵ���ʹ�������б���
        usedIndex.Add(randomIndex);
        return spawnPoints[randomIndex].position;
    }  
    
}

[System.Serializable]

public class ObstaclesData
{
    public GameObject obstaclesPref1;
    public GameObject obstaclesPref2;
}