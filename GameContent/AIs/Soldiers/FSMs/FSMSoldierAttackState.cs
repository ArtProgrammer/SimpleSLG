using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class FSMSoldierAttackState : BaseFSMState
    {
        public BaseSoldier Owner;

        public FSMSoldierAttackState(BaseSoldier owner)// : base(owner)
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
            if (Owner.TargetSys.CurTarget && Owner.TargetSys.CurTarget.IsAlive)
            {
                if (!CombatHolder.Instance.IsInAttackRange(Owner, Owner.TargetSys.CurTarget))
                {
                    Owner.StopMove();
                    Owner.MoveTo(Owner.TargetSys.CurTarget.transform.position);
                }
                else
                {
                    Owner.StopMove();
                    Owner.TargetSys.CurTarget.HPs -= Random.Range(1, 2);
                }
            }
        }

        public override void CheckTransition()
        {

        }
    }
}