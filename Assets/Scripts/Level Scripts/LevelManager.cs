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
        GameObject newWorker = Instantiate(WorkerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(newWorker);

        newWorker.GetComponent<WorkerAI>().CmdSetStats(40, 0, 4, 10);
        newWorker.GetComponent<WorkerAI>().CmdSetDefaultTarget(Objective);
        
        GetComponent<SkinnedMeshRenderer>().material = Resources.Load<Material>("Assets/Resources/Skins/Droid_dark_Worker.mat");
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
