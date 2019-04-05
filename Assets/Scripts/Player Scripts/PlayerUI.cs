using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    //References
    [SerializeField]
    RectTransform hpBar;

    PlayerStats Player { get; set; }
    PlayerController Controller { get; set; }

    //Getting access to the stats of our player
    public void SetPlayer (PlayerStats player)
    {
        Player = player;
        Controller = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        SetHpValue(Player.GetHpAmount());

        //For testing purposes
        //if (Input.GetKeyDown(KeyCode.G))
            //Player.TakeDamage(20);
    }

    void SetHpValue(float value)
    {
        //Changes the scale of the bar on the X axis to match the graphic's orientation
        if (value >= 0)
            hpBar.localScale = new Vector3(value, 1f, 1f);
    }
}
