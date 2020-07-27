﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTakeDamage : MonoBehaviour
{

    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<NPCHealth>().HurtEnemy(damageToGive);
        }
    }
}