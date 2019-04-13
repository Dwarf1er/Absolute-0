using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsStoreController : MonoBehaviour
{
    Button BuyBtnM4 { get; set; }
    Button BuyBtnM4A1 { get; set; }
    Button BuyBtnM249 { get; set; }
    Button BuyBtnM110 { get; set; }
    Button BuyBtnSMAW { get; set; }
    bool IsTriggered { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetTrigger(bool condition)
    {
        IsTriggered = condition;
        Debug.Log("Condition changed");
    }

    // Update is called once per frame
    void Update()
    {
            if (IsTriggered)
            {
                gameObject.SetActive(true);
            }
            
        
    }
}
