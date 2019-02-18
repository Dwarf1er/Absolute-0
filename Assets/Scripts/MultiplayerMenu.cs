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
        AssociatePort();
        AssociateIPAddress();
        NetworkManager.singleton.StartClient();
    }

    private void AssociateIPAddress()
    {
        string IpAdress = GameObject.Find("InputIP").GetComponent<InputField>().transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = IpAdress;
    }

    private void AssociatePort()
    {
        NetworkManager.singleton.networkPort = 5005;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetMenuButtons();
        }
        else
        {
            SetOtherButtons();
        }
    }

    private void SetOtherButtons()
    {
        
    }

    private void SetMenuButtons()
    {
        GameObject.Find("BtnHébergerLAN").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("BtnHébergerLAN").GetComponent<Button>().onClick.AddListener(StartUpHost);

        GameObject.Find("BtnJoindrePartie").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("BtnJoindrePartie").GetComponent<Button>().onClick.AddListener(StartUpClient);
    }
}

