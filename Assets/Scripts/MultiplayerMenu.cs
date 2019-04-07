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
    bool IsClicked { get; set; }
    float ConnectionTime { get; set; }
    private void Start()
    {
        InitializeReferences();
        IsClicked = false;
        ConnectionTime = 0;
        singleton.maxConnections = 2;
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


    //Méthode pemettant d'instancier le joueur hôte
    public void StartUpHost()
    {
        AssociatePort();
        singleton.StartHost();
    }

    //Méthode permettant d'instancier le joueur invité
    public void StartUpClient()
    {
        IsClicked = true; //En cliquant sur le bouton, on déclanche le décompte pour calculer le temps de la connexion.
        AssociateIPAddress();
        //Nous vérifions s'il y a bel et bien une adresse IP pour faire la connexion
        //S'il y en a pas, un message d'erreur est lancé
        if (string.IsNullOrEmpty(singleton.networkAddress))
            TxtError.text = "**Aucune adresse IP. Veuillez SVP entrer une adresse IP.**";
        //S'il y en a un, la connexion peut être établie
        else
        {
            TxtError.text = string.Empty;
            AssociatePort();
            singleton.StartClient();
        }
    }

    //Méthode permettant d'associer l'adresse IP pour le Network Manager
    private void AssociateIPAddress()
    {
        singleton.networkAddress = IPAdress.text;
    }

    //Méthode permettant d'associer un port pour établir la connexion
    private void AssociatePort()
    {
        singleton.networkPort = 5005;
    }

    //Méthode permettant de faire la transition entre le mode hors ligne et le mode en ligne
    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            SetMenuButtons();
        }
        else
        {
            SetOtherButtons();
        }
    }

   //Méthode pour instancier tous les gameobjects qui ne sont pas dans la scène du menu multijoueur
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
    
    //Méthode permettant au joueur invité de se rendre à la scène choisit par le joueur hôte
    private void GoToHostScene()
    {
        //if(string.IsNullOrEmpty(PlayScene))

        
        //else
            singleton.ServerChangeScene(PlayScene);
    }

    //Méthode permettant de changer vers la scène choisit par le joueur hôte
    private void ChangeScene(string sceneName)
    {
        //if (singleton.numPlayers == 2)
        //{
            singleton.ServerChangeScene(sceneName);
            PlayScene = sceneName;
        //}
    }

    //Méthode permettant d'instancier les gameobjects dans le menu multijoueur
    void SetMenuButtons()
    {
        BtnHost = GameObject.Find("BtnHébergerLAN").GetComponent<Button>();
        BtnHost.onClick.AddListener(() => StartUpHost());
        BtnJoin = GameObject.Find("BtnJoindrePartie").GetComponent<Button>();
        BtnJoin.onClick.AddListener(() => StartUpClient());
        IPAdress = GameObject.Find("InputIP").GetComponent<InputField>();
        TxtError = GameObject.Find("TxtError").GetComponent<Text>();
    }

    //Méthode permettant d'arrêter la connexion
    private void StopGame()
    {
        singleton.StopHost();
    }

    private void Update()
    {
        CheckConnectionTime();
    }

    //Ici, on vérifie si la connexion prend trop de temps pour se faire. S'il n'y a pas de connexion après 10 secondes,
    //on lance un message d'erreur.
    void CheckConnectionTime()
    {
        if (IsClicked)
        {
            ConnectionTime += Time.deltaTime;
        }
        if (ConnectionTime >= 10 && !string.IsNullOrEmpty(singleton.networkAddress))
        {
            TxtError.text = ErrorMessage;
            IsClicked = false;
            ConnectionTime = 0;
        }
    }


}

