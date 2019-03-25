using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnnemyInstantiator : NetworkBehaviour
{
    GameObject WorkerPrefab;
    private void Awake()
    {
        WorkerPrefab = Resources.Load<GameObject>("Ennemy Worker");
    }

    [Command]
    public void CmdSpawnWorker(Vector3 position, int tier)
    {
        GameObject newEnnemy = Instantiate(WorkerPrefab, position, Quaternion.identity);
        newEnnemy.GetComponent<WorkerAI>().SetStats(30, 0, 2, 10);
    }

    static public void SpawnMelee(Vector3 position, int tier)
    {

    }

    static public void SpawnAssault(Vector3 position, int tier)
    {

    }

    static public void SpawnHeavy(Vector3 position, int tier)
    {

    }
}
