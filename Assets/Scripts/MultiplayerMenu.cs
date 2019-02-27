//https://www.youtube.com/watch?v=9w2kwGDZ6wM
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MultiplayerMenu : NetworkManager
{
    Button BtnHost { get; set; }
    Button BtnJoin { get; set; }
    InputField IPAdress { get; set; }
    Text TxtError { get; set; }
    
    private void Start()
    {
        InitializeReferences();
       
    }

    private void InitializeReferences()
    {
        BtnHost = GameObject.Find("BtnHébergerLAN").GetComponent<Button>();
        BtnJoin = GameObject.Find("BtnJoindrePartie").GetComponent<Button>();
        IPAdress = GameObject.Find("InputIP").GetComponent<InputField>();
        TxtError = GameObject.Find("TxtError").GetComponent<Text>();
    }

    public void StartUpHost()
    {
        AssociatePort();
        NetworkManager.singleton.StartHost();
    }

    public void StartUpClient()
    {
        try
        {
            AssociateIPAddress();
            AssociatePort();
            NetworkManager.singleton.StartClient();
        }
        catch(UnityException)
        {
            TxtError.text = "Erreur surveneue";
        }
    
    }

    private void AssociateIPAddress()
    {
        string IPTest = IPAdress.text;
        
            NetworkManager.singleton.networkAddress = IPTest;
        
    
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
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(() => StopGame());
    }

    IEnumerator SetMenuButtons()
    {
        yield return new WaitForSeconds(0.3f);
        BtnHost.onClick.AddListener(() => StartUpHost());
        BtnJoin.onClick.AddListener(()=>StartUpClient());
    }

    private void StopGame()
    {
        NetworkManager.singleton.StopHost();
    }

  
}

