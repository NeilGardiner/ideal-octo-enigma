﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using Assets.Code.NPCCode;

namespace Assets.Code.FSM.MyStates
{
    [RequireComponent(typeof (IdleState))]
    [CreateAssetMenu(fileName ="PatrolState", menuName = "Unity-FSM/MyStates/Patrol", order = 2)]//Second In List

    public class PatrolState : FSMState
    {
        NPCPatrolPoints[] patrolPoints;
        int patrolPointIndex;

        [SerializeField]
        float scanDegrees;
        [SerializeField]
        float scanDistance;
        private Vector3 lastPos;
        private float lastPosTime;








        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            patrolPointIndex = -1;
        }

        public override bool EnterState()
        {
            lastPosTime = 0;
            lastPos = navMeshAgent.transform.position;
            EnteredState = false;
            if (base.EnterState())//Did it work correctly
            {
                //Grab and Store Patrol Points
                patrolPoints = npc.PatrolPoints;

                if (patrolPoints == null || patrolPoints.Length == 0)
                {
                    Debug.LogError("PatrolState: Failed to retreive patrol points from the NPC");
                }
                else
                {
                    if (patrolPointIndex < 0)
                    {
                        patrolPointIndex = UnityEngine.Random.Range(0, patrolPoints.Length);
                    }
                    else
                    {
                        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
                    }

                    SetDestination(patrolPoints[patrolPointIndex]);
                    EnteredState = true;
                }
            }

            

            return EnteredState;

            
        }

        public override void UpdateState()
        {
            if(EnteredState)
            {
                Debug.Log("Patrol " + Time.time);
                if(Scan())
                {
                    return;
                }

                if(lastPos == navMeshAgent.transform.position)
                {
                    Debug.Log("Patrol state stuck");
                    lastPosTime += Time.deltaTime;
                    if(lastPosTime >= 3)
                    {
                        Debug.Log("Patrol State UnStuck");
                        fsm.EnterState(FSMStateType.IDLE);
                    }
                }
                lastPos = navMeshAgent.transform.position;
                //Logic
                if(Vector3.Distance(navMeshAgent.transform.position, patrolPoints[patrolPointIndex].transform.position) <= 1f)
                {
                    fsm.EnterState(FSMStateType.IDLE);
                }
                
            }
        }

        public override bool ExitState()
        {
            
            base.ExitState();
            Debug.Log("Exiting Patrol State");
            

            return true;
        }

        private void SetDestination(NPCPatrolPoints destination)
        {
            if(navMeshAgent != null && destination != null)
            {
                //Position of patrol point
                navMeshAgent.SetDestination(destination.transform.position);
            }
        }

        public bool Scan()
        {
            RaycastHit hit; 
            //For Loop for Ray Cast
            for(int i = 0; i <= scanDegrees / 5; i++)
            {
                Vector3 rayDir = Quaternion.Euler(0, (i - scanDegrees / 10) * 5, 0) * npc.transform.forward;
                Debug.DrawRay(npc.transform.position, rayDir * scanDistance, Color.red);
                if(Physics.Raycast(npc.transform.position, rayDir, out hit, scanDistance))
                {
                    if(hit.transform.CompareTag("Player"))
                    {
                        npc.target = hit.transform;
                        fsm.EnterState(FSMStateType.CHASE);
                        return true;
                    }
                }
            }
                //if raycast hits
                    //if target is a player 
                        //set the target
                        //start the chase state
                       // return true;
            return false;
        }
    }
}