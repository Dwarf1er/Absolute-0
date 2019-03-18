using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyInstantiator : MonoBehaviour
{

    GameObject Worker = Resources.Load<GameObject>("Ennemy Worker");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWorker(Vector3 position, int tier)
    {
        GameObject newEnnemy = Instantiate(Worker, position, Quaternion.identity);
        newEnnemy.GetComponent<Ennemy>().SetStats(30, 0, 2, 10);
    }

    public void SpawnMelee(Vector3 position, int tier)
    {

    }

    public void SpawnAssault(Vector3 position, int tier)
    {

    }

    public void SpawnHeavy(Vector3 position, int tier)
    {

    }
}
