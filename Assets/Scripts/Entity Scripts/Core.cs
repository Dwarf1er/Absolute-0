using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] int MaxHP;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int rawDamage)
    {
        HP -= rawDamage;
        if (HP <= 0)
            Destroy(gameObject);

        GameObject.Find("CoreHealthBar").transform.Find("TxtHealth").GetComponent<Text>().text = HP.ToString();
    }
}
