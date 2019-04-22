using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level3Manager : LevelManager
{
    const float SpawnTimeInterval = 10f;
    const float WaveOneLimit = 6;
    float LastSpawnTime { get; set; }
    bool IsWaveOne { get; set; }
    bool IsWaveTwo { get; set; }
    int NumEnnemies { get; set; }
    int IndexSpawn { get; set; }
   
    public override void OnStartServer()
    {
        ActiveEnnemies = new List<GameObject>();
        EnnemySpawnPoints = new List<Vector3>();
        IsWaveOne = true;
        IsWaveTwo = false;
        EnnemySpawnPoints.Add(new Vector3(7, -26, -40));
        EnnemySpawnPoints.Add(new Vector3(-62, -28, -28));
        EnnemySpawnPoints.Add(new Vector3(74, -26, 28));
        LastSpawnTime = 0;
        NumEnnemies = 0;
        IndexSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWaveOne)
            ExecuteWaveOne();
        if (IsWaveTwo)
            ExecuteWaveTwo();
        
    }
    void ExecuteWaveOne()
    {
        LastSpawnTime += Time.deltaTime;
        if (LastSpawnTime >= SpawnTimeInterval)
        {
            if (IndexSpawn == 3)
                IndexSpawn = 0;
            SpawnWorker(EnnemySpawnPoints[IndexSpawn]);
            NumEnnemies++;
            IndexSpawn++;
            LastSpawnTime = 0;
        }
        if (NumEnnemies == 6)
        {
            IsWaveOne = false;
            IsWaveTwo = true;
            LastSpawnTime = 0;
            IndexSpawn = 0;
        }
    }
    void ExecuteWaveTwo()
    {
        LastSpawnTime += Time.deltaTime;
        if (LastSpawnTime >= SpawnTimeInterval)
        {
            if (IndexSpawn == 3)
                IndexSpawn = 0;
            SpawnWarrior(EnnemySpawnPoints[IndexSpawn]);
            NumEnnemies++;
            IndexSpawn++;
            LastSpawnTime = 0;
        }
        if (NumEnnemies >= 6)
        {
            IsWaveOne = false;
            IsWaveTwo = true;
            LastSpawnTime = 0;
        }
    }



}
