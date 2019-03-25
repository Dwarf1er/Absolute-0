using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Manager : MonoBehaviour
{
    EnnemyInstantiator Instantiator;

    // Start is called before the first frame update
    void Start()
    {
        Instantiator = GetComponent<EnnemyInstantiator>();
        Instantiator.SpawnWorker(new Vector3(0, 0, 0), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
