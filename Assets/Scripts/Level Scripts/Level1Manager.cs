using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Manager : NetworkBehaviour
{
    [SerializeField] GameObject WorkerPrefab;

    //EnnemyInstantiator Instantiator;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiator = GetComponent<EnnemyInstantiator>();
        //Instantiator.CmdSpawnWorker(new Vector3(0, 0, 0), 0);
    }

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
        newWorker.GetComponent<WorkerAI>().SetStats(30, 0, 2, 10, 0.8f);
        NetworkServer.Spawn(newWorker);
    }
}
