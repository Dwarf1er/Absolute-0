using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Level1Manager : LevelManager
{
    [SerializeField] Transform LargeSpawn1;
    [SerializeField] Transform LargeSpawn2;
    [SerializeField] Transform SmallSpawn1;
    [SerializeField] Transform SmallSpawn2;

    int waveCount;

    public override void OnStartServer()
    {
        ActiveEnnemies = new List<GameObject>(); //Should be in LevelManager
        waveCount = 0;
        NextWave();

        //StartCoroutine(SpawnWorkers(3, 2)); //Test only - not final
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextWave()
    {
        foreach (GameObject deadEnnemy in ActiveEnnemies)
        {
            if (deadEnnemy.GetComponent<Ennemy>().isDead)
                Destroy(deadEnnemy.gameObject);
        }
            

        //ActiveEnnemies = new List<GameObject>(); //Clear the list in case of unnecessary references

        waveCount++;
        //GameObject.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave " + waveCount;
        StartCoroutine("Wave" + waveCount);
    }

    //For test purposes
    IEnumerator SpawnWorkers(int nbWorkers, int workerTier)
    {
        for (int cpt = 0; cpt < nbWorkers; cpt++)
        {
            SpawnWorker(Vector3.right * 4, workerTier);
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator Wave1()
    {
        //Block 1
        SpawnWorker(LargeSpawn1.position, 0);
        SpawnWorker(LargeSpawn2.position, 0);
        SpawnWorker(SmallSpawn1.position, 0);
        SpawnWorker(SmallSpawn2.position, 0);

        yield return new WaitForSeconds(2);

        SpawnWarrior(LargeSpawn1.position, 0);
        SpawnWarrior(LargeSpawn2.position, 0);

        yield return new WaitForSeconds(10);


        //Block 2
        SpawnWorker(LargeSpawn1.position, 0);
        SpawnWorker(LargeSpawn2.position, 0);
        SpawnWorker(SmallSpawn1.position, 0);
        SpawnWorker(SmallSpawn2.position, 0);

        yield return new WaitForSeconds(2);

        SpawnWorker(LargeSpawn1.position, 1);
        SpawnWorker(LargeSpawn2.position, 1);

        yield return new WaitForSeconds(30);
        NextWave();
    }

    IEnumerator Wave2()
    {
        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWorker(LargeSpawn1.position, 0);
            SpawnWorker(LargeSpawn2.position, 0);
            SpawnWorker(SmallSpawn1.position, 0);
            SpawnWorker(SmallSpawn2.position, 0);

            yield return new WaitForSeconds(2);
        }

        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWorker(LargeSpawn1.position, 1);
            SpawnWorker(LargeSpawn2.position, 1);
            SpawnWorker(SmallSpawn1.position, 0);
            SpawnWorker(SmallSpawn2.position, 0);

            yield return new WaitForSeconds(2);
        }

        yield return new WaitForSeconds(18);

        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWarrior(LargeSpawn1.position, 1);
            SpawnWarrior(LargeSpawn2.position, 1);
            SpawnWarrior(SmallSpawn1.position, 0);
            SpawnWarrior(SmallSpawn2.position, 0);

            yield return new WaitForSeconds(2);
        }

        for (int cpt = 0; cpt < 3; cpt++) //3 times
        {
            SpawnAssaultGunner(LargeSpawn1.position, 0);
            SpawnAssaultGunner(LargeSpawn2.position, 0);

            yield return new WaitForSeconds(2);
        }


    }


   
}
