using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level3Manager : LevelManager
{
    [SerializeField]
    Vector3 SpawnPoint1;
    [SerializeField]
    Vector3 SpawnPoint2;
    [SerializeField]
    Vector3 SpawnPoint3;
    const float SpawnTimeInterval = 10f;
    const float WaveOneLimit = 6;
    float LastSpawnTime { get; set; }
    bool IsWaveOne;
    bool IsWaveTwo;
    int NumEnnemies { get; set; }
    int IndexSpawn { get; set; }
    int NumActiveEnnemies { get; set; }
   
    public override void OnStartServer()
    {
        ActiveEnnemies = new List<GameObject>();
        EnnemySpawnPoints = new List<Vector3>();
        IsWaveOne = true;
        IsWaveTwo = false;
        EnnemySpawnPoints.Add(SpawnPoint1);
        EnnemySpawnPoints.Add(SpawnPoint2);
        EnnemySpawnPoints.Add(SpawnPoint3);
        LastSpawnTime = 0;
        NumEnnemies = 0;
        IndexSpawn = 0;
        NumActiveEnnemies = 0;
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
            EndWave(ref IsWaveOne, ref IsWaveTwo);
        }
        if (NumEnnemies > 0 && NumEnnemies % 4 == 0)
        {
            DestroyEnnemies();
            NumActiveEnnemies = 0;
        }
            
        
    }

    private void EndWave(ref bool IsCurrentWave, ref bool IsNextWave)
    {
        if (AreAllDead()) 
            IsCurrentWave = false;
            IsNextWave = true;
        
    }

    private bool AreAllDead()
    {
        bool result = true;

        foreach (GameObject g in ActiveEnnemies)
            if (g != null)
                if(!g.GetComponent<Ennemy>().isDead)
                    result = false;
        return result;
    }

    private void DestroyEnnemies()
    {
        foreach (GameObject g in ActiveEnnemies)
            if (g != null)
                if(g.GetComponent<Ennemy>().isDead)
                    Destroy(g);
    }

    void ExecuteWaveTwo()
    {
        Debug.Log("Executing Wave 2");
        LastSpawnTime += Time.deltaTime;
        if (NumEnnemies < 12)
        {
            if (NumEnnemies < 4)
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
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                if (IndexSpawn % 2 == 0)
                    SpawnWorker(EnnemySpawnPoints[IndexSpawn]);
                else
                    SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn]);
                    NumEnnemies++;
                    IndexSpawn++;
                    LastSpawnTime = 0;
            }
        }
        else
        {
            //EndWave(IsWaveOne, IsWaveTwo);
        }
        if (NumEnnemies > 0 && NumEnnemies % 4 == 0)
            DestroyEnnemies();

    }



}
