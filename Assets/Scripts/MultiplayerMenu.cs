//https://www.youtube.com/watch?v=9w2kwGDZ6wM
//https://docs.unity3d.com/ScriptReference/Networking.NetworkManager.ServerChangeScene.html 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MultiplayerMenu : NetworkManager
{
    const string ErrorMessage = "**Une erreur lors de la connexion est survenue. Veuillez vous assurez d'avoir bien entré l'adresse IP**";

    Button BtnHost { get; set; }
    Button BtnJoin { get; set; }
    Button BtnDisconnect { get; set; }
    Button BtnLevel1 { get; set; }
    Button BtnLevel2 { get; set; }
    Button BtnLevel3 { get; set; }
    Button BtnClient { get; set; }
    InputField IPAdress { get; set; }
    Text TxtError { get; set; }
    string PlayScene { get; set; }
    private void Start()
    {
        InitializeReferences();
    }

    /*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        Transform startPos = GetStartPosition();

        if(startPos != null)
        {
            if(playerPrefab.name == "Player1")
            {
                SkinnedMeshRenderer skin = playerPrefab.transform.Find("Soldier_mesh").GetComponent<SkinnedMeshRenderer>(); //Referencing the skinned mesh renderer to change the material used to have a different type of soldier
            }

            if(playerPrefab.name == "Player2")
            {

            }
        }
    }
    */

    private void InitializeReferences()
    {
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
        AssociateIPAddress();

        if (string.IsNullOrEmpty(NetworkManager.singleton.networkAddress))
            TxtError.text = ErrorMessage;
        else
        {
            TxtError.text = string.Empty;
            AssociatePort();
            NetworkManager.singleton.StartClient();
        }
        
    }

    private void AssociateIPAddress()
    {
        NetworkManager.singleton.networkAddress = IPAdress.text;
    }

    private void AssociatePort()
    {
        NetworkManager.singleton.networkPort = 5005;
    }

    void OnLevelWasLoaded(int level)
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
        BtnDisconnect = GameObject.Find("BtnDisconnect").GetComponent<Button>();
        BtnDisconnect.onClick.AddListener(() => StopGame());
        BtnLevel1 = GameObject.Find("BtnLevel1").GetComponent<Button>();
        BtnLevel1.onClick.AddListener(() => ChangeScene("Level 1"));
        BtnLevel2 = GameObject.Find("BtnLevel2").GetComponent<Button>();
        BtnLevel2.onClick.AddListener(() => ChangeScene("Level 2"));
        BtnLevel3 = GameObject.Find("BtnLevel3").GetComponent<Button>();
        BtnLevel3.onClick.AddListener(() => ChangeScene("Level 3"));
        BtnClient = GameObject.Find("BtnClient").GetComponent<Button>();
        BtnClient.onClick.AddListener(() => GoToHostScene());
    }

    private void GoToHostScene()
    {
        if (!string.IsNullOrEmpty(PlayScene))
        NetworkManager.singleton.ServerChangeScene(PlayScene);
    }

    private void ChangeScene(string sceneName)
    {
        NetworkManager.singleton.ServerChangeScene(sceneName);
        PlayScene = sceneName;
    }

    void SetMenuButtons()
    {
        BtnHost = GameObject.Find("BtnHébergerLAN").GetComponent<Button>();
        BtnHost.onClick.AddListener(() => StartUpHost());
        BtnJoin = GameObject.Find("BtnJoindrePartie").GetComponent<Button>();
        BtnJoin.onClick.AddListener(() => StartUpClient());
    }

    private void StopGame()
    {
        NetworkManager.singleton.StopHost();
    }

    
}

