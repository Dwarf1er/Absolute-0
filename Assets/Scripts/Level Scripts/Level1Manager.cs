using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Manager : NetworkBehaviour
{
    [SerializeField] GameObject WorkerPrefab;
    [SerializeField] GameObject WarriorPrefab;
    [SerializeField] GameObject Objective;

    public override void OnStartServer()
    {
        SpawnWarrior();
        SpawnWorker();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
            SpawnWorker();
        
    }

    void SpawnWorker()
    {
        Vector3 position = Vector3.forward * 4;
        GameObject newWorker = Instantiate(WorkerPrefab, position, Quaternion.identity) as GameObject;
        
        NetworkServer.Spawn(newWorker);

        newWorker.GetComponent<WorkerAI>().CmdSetStats(30, 0, 4, 10);
        newWorker.GetComponent<WorkerAI>().CmdSetDefaultTarget(Objective);
    }

    void SpawnWarrior()
    {
        Vector3 position = Vector3.left * 4;
        GameObject newWarrior = Instantiate(WarriorPrefab, position, Quaternion.identity) as GameObject;

        NetworkServer.Spawn(newWarrior);

        newWarrior.GetComponent<WarriorAI>().CmdSetStats(100, 5, 4, 30);
        newWarrior.GetComponent<WarriorAI>().CmdSetDefaultTarget(Objective);
    }
}
