using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level3Manager : NetworkBehaviour
{

    [SerializeField] GameObject WorkerPrefab;
    [SerializeField] GameObject Objective;

    public override void OnStartServer()
    {
        SpawnWorker();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.G))
    //        SpawnWorker();

    //}

    void SpawnWorker()
    {
        Vector3 position = new Vector3(7,-26,-40);
        GameObject newWorker = Instantiate(WorkerPrefab, position, Quaternion.identity) as GameObject;
        newWorker.GetComponent<WorkerAI>().CmdSetStats(30, 0, 2, 10);
        newWorker.GetComponent<WorkerAI>().CmdSetDefaultTarget(Objective);
        NetworkServer.Spawn(newWorker);
    }
   
}
