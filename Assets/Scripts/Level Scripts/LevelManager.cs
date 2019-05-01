using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour
{
    [SerializeField] protected GameObject WorkerPrefab;
    [SerializeField] protected GameObject WarriorPrefab;
    [SerializeField] protected GameObject AssaultGunnerPrefab;
    [SerializeField] protected GameObject HeavyGunnerPrefab;
    [SerializeField] protected GameObject Objective;
    protected List<GameObject> ActiveEnnemies { get; set; }
    protected List<Vector3>EnnemySpawnPoints { get; set; }

    private void Start()
    {
        foreach (PlayerController playerController in FindObjectsOfType<PlayerController>())
        {
            playerController.SetCursorActive(false);
        }
    }

    protected void SpawnWorker(Vector3 spawnPoint, int tier)
    {
        GameObject newWorker = Instantiate(WorkerPrefab, spawnPoint, Quaternion.identity) as GameObject;
        
        newWorker.GetComponent<WorkerAI>().CmdSpawn(tier);
        newWorker.GetComponent<WorkerAI>().CmdSetDefaultTarget(Objective);
        //GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("Assets/Resources/Skins/Droid_dark_Worker.mat");

        NetworkServer.Spawn(newWorker);
        ActiveEnnemies.Add(newWorker);
    }
    protected void SpawnWarrior(Vector3 spawnPoint, int tier)
    {
        GameObject newWarrior = Instantiate(WarriorPrefab, spawnPoint, Quaternion.identity) as GameObject;

        newWarrior.GetComponent<WarriorAI>().CmdSpawn(tier);
        newWarrior.GetComponent<WarriorAI>().CmdSetDefaultTarget(Objective);

        NetworkServer.Spawn(newWarrior);

        ActiveEnnemies.Add(newWarrior);
    }

    protected void SpawnAssaultGunner(Vector3 spawnPoint, int tier)
    {
        GameObject newAssaultGunner = Instantiate(AssaultGunnerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        newAssaultGunner.GetComponent<AssaultGunnerAI>().CmdSpawn(tier);
        newAssaultGunner.GetComponent<AssaultGunnerAI>().CmdSetDefaultTarget(Objective);

        NetworkServer.Spawn(newAssaultGunner);

        ActiveEnnemies.Add(newAssaultGunner);
    }

    protected void SpawnHeavyGunner(Vector3 spawnPoint, int tier)
    {
        GameObject newHeavyGunner = Instantiate(HeavyGunnerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        newHeavyGunner.GetComponent<HeavyGunnerAI>().CmdSpawn(tier);
        newHeavyGunner.GetComponent<HeavyGunnerAI>().CmdSetDefaultTarget(Objective);

        NetworkServer.Spawn(newHeavyGunner);
        ActiveEnnemies.Add(newHeavyGunner);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
