using System;
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
        if (NumEnnemies < 6)
        {
            if (LastSpawnTime >= SpawnTimeInterval)
            {
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                SpawnWorker(EnnemySpawnPoints[IndexSpawn]);
                NumEnnemies++;
                IndexSpawn++;
                LastSpawnTime = 0;

            }
        }
        else
        {
            EndWave();
        }
        
                
          
                
    }

    private void EndWave()
    {
        if (AreAllDead())
        {
            foreach(GameObject g in ActiveEnnemies)
            {
                Destroy(g);
            }
        }
    }

    private bool AreAllDead()
    {
        bool result = true;
        foreach(GameObject g in ActiveEnnemies)
        {
            if (g != null)
                if (!g.GetComponent<Ennemy>().isDead)
                    result = false;
                
        }
        return result;
    }

    void ExecuteWaveTwo()
    {
        Debug.Log("Executing Wave 2");
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
    }



}
