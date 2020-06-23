﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour
{
    public LineRenderer line; //to hold the line Renderer
    [SerializeField]
    Transform targetTransform; //to hold the transform of the target
    GameObject closestEnemyGO;
    public WolfSense wolfSenseScript;
    GameObject Player;
    void Awake()
    {
        line = GetComponent<LineRenderer>(); //get the line renderer
        line.startWidth = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (wolfSenseScript.ClosestEnemy != null)
        {
            closestEnemyGO = wolfSenseScript.ClosestEnemy;
        }
        getPath();
    }
    void getPath()
    {
        if (Player != null)
        {
            line.SetPosition(0, Player.transform.position); //set the line's origin
        }
        if (closestEnemyGO != null && wolfSenseScript.WolfSenseOn)
        {
            line.startWidth = 1;
            line.SetPosition(1, closestEnemyGO.transform.position);
        }
        else
        {
            line.startWidth = 0;
        }

    }

    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2) //if the path has 1 or no corners, there is no need
            return;

        line.positionCount = path.corners.Length; //set the array of positions to the amount of corners

        for (var i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }
    }
}