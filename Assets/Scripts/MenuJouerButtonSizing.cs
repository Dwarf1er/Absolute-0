using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuJouerButtonSizing : MonoBehaviour
{
    //References
    Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<Text>();
    }

    //Change the text from the button to the desired font size
    public void changeFontSize(int newfontSize)
    {
        txt.fontSize = newfontSize;
    }
}
