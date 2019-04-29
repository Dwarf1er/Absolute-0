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
    float DeleteTime { get; set; }
    bool IsWaveOne;
    bool IsWaveTwo;
    bool IsWaveThree;
    int NumEnnemies { get; set; }
    int IndexSpawn { get; set; }
    int NumActiveEnnemies { get; set; }
   
    public override void OnStartServer()
    {
        ActiveEnnemies = new List<GameObject>();
        EnnemySpawnPoints = new List<Vector3>();
        IsWaveOne = true;
        IsWaveTwo = false;
        IsWaveThree = false;
        EnnemySpawnPoints.Add(SpawnPoint1);
        EnnemySpawnPoints.Add(SpawnPoint2);
        EnnemySpawnPoints.Add(SpawnPoint3);
        LastSpawnTime = 0;
        DeleteTime = 0;
        NumEnnemies = 0;
        IndexSpawn = 0;
        NumActiveEnnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Objective != null)
        {
            if (IsWaveOne)
                ExecuteWaveOne();
            if (IsWaveTwo)
                ExecuteWaveTwo();
            if (IsWaveThree)
                ExecuteWaveThree();
        }
    }

   

    void ExecuteWaveOne()
    {
        Debug.Log("Executing Wave 1");
        LastSpawnTime += Time.deltaTime;
        DeleteTime += Time.deltaTime;
        if (NumEnnemies < 6)
        {
            if (LastSpawnTime >= SpawnTimeInterval)
            {
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                SpawnWorker(EnnemySpawnPoints[IndexSpawn], 0);
                NumEnnemies++;
                IndexSpawn++;
                LastSpawnTime = 0;
            }
        }
        else
        {
            EndWave(ref IsWaveOne, ref IsWaveTwo);
        }
        if(DeleteTime >= SpawnTimeInterval)
        {
            DestroyEnnemies();
            DeleteTime = 0;
        }
        
    }

    private void EndWave(ref bool IsCurrentWave, ref bool IsNextWave)
    {
        if (AreAllDead())
        {
            IsCurrentWave = false;
            IsNextWave = true;
            DestroyEnnemies();
            LastSpawnTime = 0;
            IndexSpawn = 0;
            NumEnnemies = 0;
        }
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
        DeleteTime += Time.deltaTime;
        if (NumEnnemies < 12)
        {
            if (NumEnnemies < 4)
            {
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    SpawnWorker(EnnemySpawnPoints[IndexSpawn], 0);
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
                    SpawnWorker(EnnemySpawnPoints[IndexSpawn], 0);
                else
                    SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 0);
                NumEnnemies++;
                IndexSpawn++;
                LastSpawnTime = 0;
            }
        }
        else
        {
            EndWave(ref IsWaveTwo, ref IsWaveThree);
        }
        if (DeleteTime >= SpawnTimeInterval)
        {
            DestroyEnnemies();
            DeleteTime = 0;
        }
            

    }
    private void ExecuteWaveThree()
    {
        Debug.Log("Executing Wave 3");
        LastSpawnTime += Time.deltaTime;
        DeleteTime += Time.deltaTime;
        if (NumEnnemies < 20)
        {
            if (NumEnnemies < 5)
            {
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    if (IndexSpawn % 2 == 0)
                        SpawnWorker(EnnemySpawnPoints[IndexSpawn], 0);
                    else
                        SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 0);
                    NumEnnemies++;
                    IndexSpawn++;
                    LastSpawnTime = 0;
                }
            }
            else
            {
                if (NumEnnemies < 10)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 0);
                    NumEnnemies++;
                    IndexSpawn++;
                    LastSpawnTime = 0;
                }
                else
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    if (IndexSpawn % 2 == 0)
                        SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 0);
                    else
                        SpawnWarrior(EnnemySpawnPoints[IndexSpawn], 0);
                    NumEnnemies++;
                    IndexSpawn++;
                    LastSpawnTime = 0;
                }
            }
        }
        else
        {
            
        }
        if (DeleteTime >= SpawnTimeInterval)
        {
            DestroyEnnemies();
            DeleteTime = 0;
        }
    }



}
