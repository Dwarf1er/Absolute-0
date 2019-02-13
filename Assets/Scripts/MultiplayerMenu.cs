using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Site pour le OnMouseOver:
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseOver.html
public class MultiplayerMenu : MonoBehaviour
{
    Button HostButton { get; set; }
    Button JoinButton { get; set; }
    Button JoinGame { get; set; }
    GameObject HostDescription { get; set; }
    GameObject JoinDescription { get; set; }
    InputField InputIP { get; set; }
    Text TxtInputIP { get; set; }
    bool EstCliqué { get; set; }
       

    // Start is called before the first frame update
    void Start()
    {
        InitializeReferences();
        EstCliqué = false;
        JoinButton.onClick.AddListener(() => EstCliqué = !EstCliqué);
    }

    private void CheckComponenets()
    {
        InputIP.gameObject.SetActive(EstCliqué);
        TxtInputIP.gameObject.SetActive(EstCliqué);
        JoinGame.gameObject.SetActive(EstCliqué);
    }

    private void InitializeReferences()
    {
        HostButton = GameObject.Find("BtnHébergerLAN").GetComponent<Button>();
        JoinButton = GameObject.Find("BtnJoindreLAN").GetComponent<Button>();
        JoinGame = GameObject.Find("BtnJoindrePartie").GetComponent<Button>();
        InputIP = GameObject.Find("InputIP").GetComponent<InputField>();
        TxtInputIP = GameObject.Find("TxtInputIP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckComponenets();
    }
}

