using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleFoodGoal : GoalComposite<NormalPeople>
    {
        public NormalPeopleFoodGoal(NormalPeople p, int type) : base(p, type)
        {

        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
            Owner.StopMove();
            Owner.Move(Owner.Memory.GetLocation("Food").position);

            Owner.ExitBuilding();
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsArrive())
            {
                if (Owner.Energy > 100)
                {
                    Status = GoalStatus.Complete;
                    Owner.ExitBuilding();
                }
                else
                {
                    Owner.EnterBuilding();
                    Owner.GetEnergy();
                    Status = GoalStatus.Active;
                }
            }

            return Status;
        }

        public override void Terminate()
        {
            Owner.ExitBuilding();
        }
    }
}