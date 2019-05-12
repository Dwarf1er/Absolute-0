using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    //Méthode permettant de compter le temps entre chaque mise à jour
    private void CountTime()
    {
        LastSpawnTime += Time.deltaTime;
        DeleteTime += Time.deltaTime;
    }

    //Méthode permettant d'incrémenter des variable entières
    void AjustCounters()
    {
        NumEnnemies++;
        IndexSpawn++;
        LastSpawnTime = 0;
    }

    void ExecuteWaveOne()
    {
        GameObject.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave 1"; //Ajout par Pierre
        Debug.Log("Executing Wave 1");

        if (NumEnnemies < 6)
        {
            if (LastSpawnTime >= SpawnTimeInterval)
            {
                //On fait instancier les ennemis à différents points dans le niveau en les alternant
                if (IndexSpawn == 3) //Lorsque l'indice est plus grand que 2 (égale à 3), on le remet à zéro 
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

    //Méthode permettant de mettre fin à une vague
    private void EndWave(ref bool isCurrentWave, ref bool isNextWave)
    {
        //Tant que les ennemis ne sont pas tous morts, on ne met pas fin à la vague
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

    //Méthode permettant de vérifier si tous les ennemis dans une vague sont morts
    private bool AreAllDead()
    {
        bool result = true;

        foreach (GameObject g in ActiveEnnemies)
            if (g != null)
                if(!g.GetComponent<Ennemy>().isDead)
                    result = false; //Le résultat sera faux s'il y a encore un ennemi vivant
        return result;
    }

    //Méthode permettant de détruire tous les ennemis qui sont morts dans le niveau
    private void DestroyEnnemies()
    {
        foreach (GameObject g in ActiveEnnemies)
            if (g != null)
                if(g.GetComponent<Ennemy>().isDead)
                    Destroy(g);
    }

    //Méthode qui, après un certain temps, vient élimniner les ennemis morts de map et remet le compteur à zéro
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
        GameObject.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave 2"; //Ajout par Pierre
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
                    if (IndexSpawn % 2 == 0) //L'ennemi instancié est décidé par le fait que l'indice soit paire ou non.
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
        GameObject.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave 3"; //Ajout par Pierre
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
        GameObject.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave 4"; //Ajout par Pierre
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
                    NumEnnemies += 2; //On fait +2, car on instancie 2 ennemis à la fois
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
        GameObject.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave 5"; //Ajout par Pierre
        Debug.Log("Executing Wave 5");

        if(NumEnnemies < 22)
        {
            if (NumEnnemies < 18)
            {
                if (IndexSpawn == 3)
                    IndexSpawn = 0;
                SpawnAtRandom(IndexSpawn); //On choist un type d'ennemi au hazard
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

    //Méthode permettant de choisir un ennemy au hazard et de l'instancier
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

    //Méthode permettant de de retirer tous les références non existants de la liste des ennemis
    void ClearList()
    {
        ActiveEnnemies.RemoveAll(g => g == null);
    }


}
