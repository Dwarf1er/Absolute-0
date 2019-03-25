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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SpawnWorker();
    }

    void SpawnWorker()
    {
        Vector3 position = Vector3.zero;
        GameObject newWorker = Instantiate(WorkerPrefab, position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(newWorker);
    }
}
