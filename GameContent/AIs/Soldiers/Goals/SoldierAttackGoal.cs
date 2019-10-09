using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class SoldierAttackGoal : GoalComposite<BaseSoldier>
    {
        public SoldierAttackGoal(BaseSoldier p, int type) : base(p, type)
        {

        }

        public override void Activate()
        {
            Status = GoalStatus.Active;

            if (Owner.TargetSys.CurTarget &&
                Owner.TargetSys.CurTarget.IsAlive)
            {
                if (!Owner.IsArrive())
                {
                    Owner.MoveTo(Owner.TargetSys.CurTarget.Position);
                }
                else
                {
                    Owner.StopMove();
                    if (CombatHolder.Instance.IsInAttackRange(Owner,
                        Owner.TargetSys.CurTarget))
                    {
                        Owner.Attack();
                    }
                    else
                    {
                        Owner.MoveTo(Owner.TargetSys.CurTarget.Position);
                    }
                }
            }
            else
            {
                Owner.StopMove();
                Status = GoalStatus.Inactive;
            }
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsArrive())
            {
                Owner.StopMove();
                if (CombatHolder.Instance.IsCloseEnough(Owner,
                    Owner.TargetSys.CurTarget, Owner.CollideRadius))
                {
                    Owner.Attack();
                }
                //else
                //{
                //    Owner.MoveTo(Owner.TargetSys.CurTarget.Position);
                //}

                if (Owner.TargetSys.CurTarget == null ||
                    !Owner.TargetSys.CurTarget.IsAlive)
                {
                    Status = GoalStatus.Complete;
                }
            }

            return Status;
        }

        public override void Terminate()
        {
        }
    }
}