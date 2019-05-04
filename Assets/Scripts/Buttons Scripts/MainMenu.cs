using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button PlayBtn { get; set; }
    // Start is called before the first frame update
    void Update()
    {
        if (Input.anyKey)
            SceneManager.LoadScene("Menu Jouer", LoadSceneMode.Single);
    }
}
