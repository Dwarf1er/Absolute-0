using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button PlayBtn { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        PlayBtn = GameObject.Find("BtnJouer").GetComponent<Button>();
        PlayBtn.onClick.AddListener(() => SceneManager.LoadScene("MenuJouer", LoadSceneMode.Single));
    }
}
