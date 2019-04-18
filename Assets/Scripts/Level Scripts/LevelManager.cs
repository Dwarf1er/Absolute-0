using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour
{
    [SerializeField] protected GameObject WorkerPrefab;
    [SerializeField] protected GameObject WarriorPrefab;
    [SerializeField] protected GameObject Objective;
    protected void SpawnWorker(Vector3 spawnPoint)
    {
        GameObject newWarrior = Instantiate(WorkerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(newWarrior);

        newWarrior.GetComponent<WarriorAI>().CmdSetStats(100, 5, 4, 30);
        newWarrior.GetComponent<WarriorAI>().CmdSetDefaultTarget(Objective);
    }
    protected void SpawnWarrior(Vector3 spawnPoint)
    {
        GameObject newWarrior = Instantiate(WarriorPrefab, spawnPoint, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(newWarrior);

        newWarrior.GetComponent<WarriorAI>().CmdSetStats(100, 5, 4, 30);
        newWarrior.GetComponent<WarriorAI>().CmdSetDefaultTarget(Objective);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
