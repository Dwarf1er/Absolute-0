using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyInstantiator : MonoBehaviour
{
    GameObject WorkerPrefab;
    private void Awake()
    {
        WorkerPrefab = Resources.Load<GameObject>("Ennemy Worker");
    }
    public void SpawnWorker(Vector3 position, int tier)
    {
        GameObject newEnnemy = Instantiate(WorkerPrefab, position, Quaternion.identity);
        newEnnemy.GetComponent<Ennemy>().SetStats(30, 0, 2, 10);
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
