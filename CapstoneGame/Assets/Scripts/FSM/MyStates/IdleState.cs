﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.FSM.MyStates
{

    [CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/MyStates/Idle", order =1)]//First in List
    public class IdleState : FSMState
    {
        [SerializeField]
        float idleDuration = 3f;

        float totalDuration;

        

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();
            if (EnteredState)
            {
                Debug.Log("Entered Idle State");
                totalDuration = 0f;
            }
            
            return EnteredState;
        }


        public override void UpdateState()
        {
            if (EnteredState)
            {
                totalDuration += Time.deltaTime;
                Debug.Log("Updating Idle State" + totalDuration + " seconds.");

                if(totalDuration >= idleDuration)
                {
                    fsm.EnterState(FSMStateType.PATROL);
                }
            }
        }

        public override bool ExitState()
        {
            
            base.ExitState();
            Debug.Log("Exiting Idle state");
           

            return true;
        }

        
    }
}