using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] int MaxHP;

    [SerializeField] GameObject GameOverMenu;
    bool gameIsOver;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        gameIsOver = false;
    }

    public void TakeDamage(int rawDamage)
    {
        HP -= rawDamage;
        if (HP < 0)
            HP = 0;
        if (HP == 0 && !gameIsOver)
            GameOver();

        GameObject.Find("CoreHealthBar").transform.Find("TxtHealth").GetComponent<Text>().text = HP.ToString();
        GameObject.Find("CoreHealthBar").transform.Find("Bar Fill").transform.localScale = new Vector3((float)HP / (float)MaxHP, 1, 1);
    }

    public void GameOver()
    {
        foreach (PlayerController playerController in FindObjectsOfType<PlayerController>())
        {
            playerController.SetCursorActive(true);
        }
        Instantiate(GameOverMenu, GameObject.Find("PlayerUI").transform);
        gameIsOver = true;
    }
}
