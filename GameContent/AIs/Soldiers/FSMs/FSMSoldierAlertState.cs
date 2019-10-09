using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class FSMSoldierAlertState : BaseFSMState
    {
        public BaseSoldier Owner;

        public FSMSoldierAlertState(BaseSoldier owner)// : base(owner)
        {
            Owner = owner;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Process(float dt = 0.0f)
        {

        }

        public override void CheckTransition()
        {

        }
    }
}