using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class SoldierMoveToGoal : Goal<BaseSoldier>
    {
        public SoldierMoveToGoal(BaseSoldier p, int type) : base(p, type)
        {

        }

        public override void Activate()
        {
            Status = GoalStatus.Active;

            if (!Owner.IsArrive())
            {
                Owner.ForceMove = 0.5f;
            }
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsArrive())
            {
                Owner.StopMove();

                Status = GoalStatus.Complete;
            }

            return Status;
        }

        public override void Terminate()
        {
        }
    }
}