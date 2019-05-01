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
    const float SpawnTimeInterval = 8f;
    const float WaveOneLimit = 6;
    float LastSpawnTime { get; set; }
    float DeleteTime { get; set; }
    int NumEnnemies { get; set; }
    int IndexSpawn { get; set; }
    int NumActiveEnnemies { get; set; }
    public int WaveNumber { get; set; }

    bool IsWaveOne;
    bool IsWaveTwo;
    bool IsWaveThree;
    bool IsWaveFour;
    bool IsWaveFive;
   
    public override void OnStartServer()
    {
        ActiveEnnemies = new List<GameObject>();
        EnnemySpawnPoints = new List<Vector3>();
        IsWaveOne = true;
        IsWaveTwo = false;
        IsWaveThree = false;
        IsWaveFour = false;
        IsWaveFive = false;
        WaveNumber = 1;
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
            CountTime();
            if (IsWaveOne)
                ExecuteWaveOne();
            if (IsWaveTwo)
                ExecuteWaveTwo();
            if (IsWaveThree)
                ExecuteWaveThree();
            if (IsWaveFour)
                ExecuteWaveFour();
            if (IsWaveFive)
                ExecuteWaveFive();
        }
    }

    private void CountTime()
    {
        LastSpawnTime += Time.deltaTime;
        DeleteTime += Time.deltaTime;
    }

    void AjustCounters()
    {
        NumEnnemies++;
        IndexSpawn++;
        LastSpawnTime = 0;
    }

    void ExecuteWaveOne()
    {
        Debug.Log("Executing Wave 1");
        if (NumEnnemies < 6)
        {
            if (LastSpawnTime >= SpawnTimeInterval)
            {
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                SpawnWorker(EnnemySpawnPoints[IndexSpawn], 0);
                AjustCounters();
            }
        }
        else
        {
            EndWave(ref IsWaveOne, ref IsWaveTwo);
        }
        ClearLevel(8);
    }

    private void EndWave(ref bool isCurrentWave, ref bool isNextWave)
    {
        if (AreAllDead())
        {
            isCurrentWave = false;
            isNextWave = true;
            DestroyEnnemies();
            ClearList();
            WaveNumber++;
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

    void ClearLevel(float timeLimit)
    {
        if (DeleteTime >= timeLimit)
        {
            DestroyEnnemies();
            DeleteTime = 0;
        }
    }

    void ExecuteWaveTwo()
    {
        Debug.Log("Executing Wave 2");
        if (NumEnnemies < 10)
        {
            if (NumEnnemies < 4)
            {
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    SpawnWorker(EnnemySpawnPoints[IndexSpawn], 1);
                    AjustCounters();

                }
            }
            else
            {
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    if (IndexSpawn % 2 == 0)
                        SpawnWorker(EnnemySpawnPoints[IndexSpawn], 1);
                    else
                        SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 0);
                    AjustCounters();
                }
            }
        }
        else
        {
            EndWave(ref IsWaveTwo, ref IsWaveThree);
        }
        ClearLevel(8);
    }
    private void ExecuteWaveThree()
    {
        Debug.Log("Executing Wave 3");
        if (NumEnnemies < 14)
        {
            if (NumEnnemies < 5)
            {
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    if (IndexSpawn % 2 == 0)
                        SpawnWorker(EnnemySpawnPoints[IndexSpawn], 2);
                    else
                        SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 1);
                    AjustCounters();
                }
            }
            else
            {
                if (NumEnnemies < 10)
                {
                    if (LastSpawnTime >= SpawnTimeInterval)
                    {
                        if (IndexSpawn == 3)
                            IndexSpawn = 0;
                        SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 1);
                        AjustCounters();
                    }
                }
                else
                {
                    if (LastSpawnTime >= SpawnTimeInterval)
                    {
                        if (IndexSpawn == 3)
                            IndexSpawn = 0;
                        if (IndexSpawn % 2 == 0)
                            SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 1);
                        else
                            SpawnWarrior(EnnemySpawnPoints[IndexSpawn], 0);
                        AjustCounters();
                    }
                }
            }
        }
        else
        {
            EndWave(ref IsWaveThree, ref IsWaveFour);
        }
        ClearLevel(8);
    }
    void ExecuteWaveFour()
    {
        Debug.Log("Executing Wave 4");
        if(NumEnnemies < 18)
        {
            if(NumEnnemies < 6)
            {
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    if (IndexSpawn == 0)
                        SpawnWorker(EnnemySpawnPoints[IndexSpawn], 2);
                    if (IndexSpawn == 1)
                        SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 1);
                    if (IndexSpawn == 2)
                        SpawnWarrior(EnnemySpawnPoints[IndexSpawn], 0);
                    AjustCounters();
                }
                

            }
            else
            {
                
                if (LastSpawnTime >= SpawnTimeInterval)
                {
                    if (IndexSpawn == 3)
                        IndexSpawn = 0;
                    SpawnAssaultGunner(EnnemySpawnPoints[IndexSpawn], 1);
                    SpawnWarrior(EnnemySpawnPoints[IndexSpawn + 1], 0);
                    NumEnnemies += 2;
                    IndexSpawn++;
                    LastSpawnTime = 0;
                }
                
            }
        }
        else
        {
            EndWave(ref IsWaveFour, ref IsWaveFive);
        }
        ClearLevel(8);
    }

    void ExecuteWaveFive()
    {
        Debug.Log("Executing Wave 5");
        if(NumEnnemies < 22)
        {
            if (NumEnnemies < 18)
            {
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                SpawnAtRandom(IndexSpawn);
                AjustCounters();
            }
            else
            {
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                SpawnHeavyGunner(EnnemySpawnPoints[IndexSpawn], 0);
                AjustCounters();
            }
        }


    }

    private void SpawnAtRandom(int indexSpawn)
    {
        int ennemy = UnityEngine.Random.Range(0, 2);
        if (ennemy == 2)
            SpawnWorker(EnnemySpawnPoints[indexSpawn], 3);
        if (ennemy == 1)
            SpawnAssaultGunner(EnnemySpawnPoints[indexSpawn], 2);
        if (ennemy == 2)
            SpawnWarrior(EnnemySpawnPoints[indexSpawn], 1);

    }

    void ClearList()
    {
        ActiveEnnemies.RemoveAll(g => g == null);
    }


}
