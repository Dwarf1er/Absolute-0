﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.Find("BigExplosionEffect").GetComponent<AudioSource>().Play();
        Destroy(gameObject, 6);
    }
}
