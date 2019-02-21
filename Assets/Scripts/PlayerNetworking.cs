using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworking : NetworkBehaviour
{
    //References
    [SerializeField]
    List<Behaviour> offlineComponents = new List<Behaviour>();
    Camera lobbyCamera;

    void Start()
    {
        //Switches every online components to offline components if not related to the local player (added to the list in UI)
        if (!isLocalPlayer)
            foreach (Behaviour putOffline in offlineComponents)
                putOffline.enabled = false;
        
        //Deactivate the LobbyCamera when player logs in
        else
        {
            lobbyCamera = Camera.main;

            //Prevents error if there is no lobbyCamera in the scene
            if (lobbyCamera != null)
                lobbyCamera.enabled = true;
        }
    }

    //Reactivate the LobbyCamera
    void OnDisable()
    {
        if (lobbyCamera != null)
            lobbyCamera.enabled = true;
    }
}
