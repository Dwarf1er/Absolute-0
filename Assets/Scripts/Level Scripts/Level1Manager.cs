using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//Auteur: Pierre Mercier
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
        StartCoroutine(WaitForPlayers());
    }

    IEnumerator WaitForPlayers()
    {
        yield return new WaitForSeconds(5);

        Players = new List<PlayerController>();
        foreach (PlayerController playerController in FindObjectsOfType<PlayerController>())
        {
            Players.Add(playerController);
            playerController.SetCursorActive(false);
        }

        StartCoroutine(NextWave());
    }

    IEnumerator NextWave()
    {
        List<GameObject> newEnnemyList = new List<GameObject>();
        foreach (GameObject ennemy in ActiveEnnemies)
        {
            if (ennemy.GetComponent<Ennemy>().isDead)
                Destroy(ennemy.gameObject); //Remove corpses
            else
                newEnnemyList.Add(ennemy);
        }
        ActiveEnnemies = newEnnemyList; //Delete references to dead ennemies

        waveCount++;
        for (int cpt = 10; cpt > 0; cpt--)
        {
            foreach (PlayerController player in Players)
                player.transform.Find("PlayerUI").transform.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Next wave in " + cpt;
            yield return new WaitForSeconds(1);
        }

        foreach (PlayerController player in Players)
        {
            player.GetComponent<PlayerStats>().currentHp = 100;
            player.transform.Find("PlayerUI").transform.Find("WaveCounter").transform.Find("Text").GetComponent<Text>().text = "Wave " + waveCount;
        }

        StartCoroutine("Wave" + waveCount);
    }

    IEnumerator Wave1()
    {
        //Block 1
        SpawnWorker(LargeSpawn1.position, 0);
        SpawnWorker(LargeSpawn2.position, 0);
        SpawnWorker(SmallSpawn1.position, 0);
        SpawnWorker(SmallSpawn2.position, 0);

        yield return new WaitForSeconds(6);

        SpawnWarrior(LargeSpawn1.position, 0);
        SpawnWarrior(LargeSpawn2.position, 0);

        yield return new WaitForSeconds(10);


        //Block 2
        SpawnWorker(LargeSpawn1.position, 0);
        SpawnWorker(LargeSpawn2.position, 0);
        SpawnWorker(SmallSpawn1.position, 0);
        SpawnWorker(SmallSpawn2.position, 0);

        yield return new WaitForSeconds(6);

        SpawnWorker(LargeSpawn1.position, 1);
        SpawnWorker(LargeSpawn2.position, 1);

        yield return new WaitForSeconds(30);
        StartCoroutine(NextWave());
    }

    IEnumerator Wave2()
    {
        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWorker(LargeSpawn1.position, 0);
            SpawnWorker(LargeSpawn2.position, 0);
            SpawnWorker(SmallSpawn1.position, 0);
            SpawnWorker(SmallSpawn2.position, 0);

            yield return new WaitForSeconds(6);
        }

        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWorker(LargeSpawn1.position, 1);
            SpawnWorker(LargeSpawn2.position, 1);
            SpawnWorker(SmallSpawn1.position, 0);
            SpawnWorker(SmallSpawn2.position, 0);

            yield return new WaitForSeconds(6);
        }

        yield return new WaitForSeconds(14);

        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWarrior(LargeSpawn1.position, 1);
            SpawnWarrior(LargeSpawn2.position, 1);
            SpawnWarrior(SmallSpawn1.position, 0);
            SpawnWarrior(SmallSpawn2.position, 0);

            yield return new WaitForSeconds(6);
        }

        for (int cpt = 0; cpt < 3; cpt++) //3 times
        {
            SpawnAssaultGunner(LargeSpawn1.position, 0);
            SpawnAssaultGunner(LargeSpawn2.position, 0);

            yield return new WaitForSeconds(6);
        }

        yield return new WaitForSeconds(24);
        StartCoroutine(NextWave());
    }

    IEnumerator Wave3()
    {
        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWarrior(LargeSpawn1.position, 1);
            SpawnWarrior(LargeSpawn2.position, 1);
            SpawnWorker(SmallSpawn1.position, 1);
            SpawnWorker(SmallSpawn2.position, 1);

            yield return new WaitForSeconds(6);
        }

        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnWarrior(LargeSpawn1.position, 1);
            SpawnWarrior(LargeSpawn2.position, 1);
            SpawnWorker(SmallSpawn1.position, 2);
            SpawnWorker(SmallSpawn2.position, 2);

            yield return new WaitForSeconds(6);
        }

        yield return new WaitForSeconds(14);

        for (int cpt = 0; cpt < 2; cpt++) //2 times
        {
            SpawnHeavyGunner(LargeSpawn1.position, 2);
            SpawnHeavyGunner(LargeSpawn2.position, 2);
            SpawnAssaultGunner(SmallSpawn1.position, 1);
            SpawnAssaultGunner(SmallSpawn2.position, 1);

            yield return new WaitForSeconds(10);
        }
    }


   
}
