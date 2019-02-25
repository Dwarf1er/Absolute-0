//https://www.youtube.com/watch?v=9w2kwGDZ6wM
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MultiplayerMenu : NetworkManager
{
    public void StartUpHost()
    {
        AssociatePort();
        NetworkManager.singleton.StartHost();
    }

    public void StartUpClient()
    {
        AssociateIPAddress();
        AssociatePort();
        NetworkManager.singleton.StartClient();
    }

    private void AssociateIPAddress()
    {
        NetworkManager.singleton.networkAddress =  GameObject.Find("InputIP").transform.FindChild("Text").GetComponent<Text>().text;
    }

    private void AssociatePort()
    {
        NetworkManager.singleton.networkPort = 5005;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            StartCoroutine(SetMenuButtons());
        }
        else
        {
            SetOtherButtons();
        }
    }


    private void SetOtherButtons()
    {
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

    IEnumerator SetMenuButtons()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("BtnHébergerLAN").GetComponent<Button>().onClick.AddListener(() => StartUpHost());
        GameObject.Find("BtnJoindrePartie").GetComponent<Button>().onClick.AddListener(()=>StartUpClient());
    }
}

