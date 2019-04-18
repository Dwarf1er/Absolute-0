using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Level3Manager : LevelManager
{
    public override void OnStartServer()
    {
        SpawnWarrior(new Vector3(7, -26, -40));
        SpawnWarrior(new Vector3(7, -26, -40));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
            SpawnWorker(new Vector3(7, -26, -40));

    }



}
