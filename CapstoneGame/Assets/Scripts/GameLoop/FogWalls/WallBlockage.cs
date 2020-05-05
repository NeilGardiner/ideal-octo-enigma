﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlockage : MonoBehaviour
{
    public List<GameObject> EnemiesToDefeat;
    public Player player;


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemiesToDefeat.Count == 0)
        {
            player.AddSkillPoint();
            Destroy(this.gameObject);
            GameManager.Instance.Camera.ShowMouse();
        }
    }

   
}