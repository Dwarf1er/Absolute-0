using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//Site pour le OnMouseOver:
//https://docs.unity3d.com/ScriptReference/UI.Selectable.OnPointerEnter.html
public class LANButtonManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    GameObject Text;
    bool EstDessus { get; set; }
    public void OnPointerEnter(PointerEventData eventData)
    {
        EstDessus = true;
    }

    // Start is called before the first frame update
    void Start()
    {
      
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.SetActive(EstDessus);

    }
}
