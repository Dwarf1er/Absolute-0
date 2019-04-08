using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Manager : NetworkBehaviour
{
    [SerializeField] GameObject WorkerPrefab;
    [SerializeField] GameObject Objective;

    public override void OnStartServer()
    {
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
}
