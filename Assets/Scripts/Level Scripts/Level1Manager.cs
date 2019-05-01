using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Manager : LevelManager
{
    public override void OnStartServer()
    {
        SpawnWorker(Vector3.zero, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
            SpawnWorker(Vector3.left * 4, 1);
        
    }

   
}
