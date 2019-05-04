using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuJouerButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    //Detects when the cursor moves over the button
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        MenuJouerButtonSizing buttonSizing = GetComponent<MenuJouerButtonSizing>();
        buttonSizing.changeFontSize(40); //Arbitrary number
    }

    //Detects when the cursor moves away from the button
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        MenuJouerButtonSizing buttonSizing = GetComponent<MenuJouerButtonSizing>();
        buttonSizing.changeFontSize(28); //28 by default
    }
}
