using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class SoldierDefenceGoal : GoalComposite<BaseSoldier>
    {
        public SoldierDefenceGoal(BaseSoldier p, int type) : base(p, type)
        {

        }

        public override void Activate()
        {
            Status = GoalStatus.Active;

            Owner.MoveTo(Owner.Memory.GetLocation("Work").position);
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            return Status;
        }

        public override void Terminate()
        {
        }
    }
}