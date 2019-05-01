using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnReturnToLobby : MonoBehaviour
{
    Button btnReturnToLobby { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        btnReturnToLobby = GetComponent<Button>();
        btnReturnToLobby.onClick.AddListener(() => SceneManager.LoadScene("Menu Jouer"));
    }
}
