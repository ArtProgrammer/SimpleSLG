using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleWorkGoal : GoalComposite<NormalPeople>
    {
        public NormalPeopleWorkGoal(NormalPeople p, int type) : base(p, type)
        {

        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
            Owner.StopMove();
            Owner.Move(Owner.Memory.GetLocation("Work").position);

            Owner.ExitBuilding();
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsArrive())
            {
                Owner.EnterBuilding();
                Owner.Work();
            }

            if (Owner.Energy < 0)
            {
                Status = GoalStatus.Failed;
                Owner.ExitBuilding();
            }

            return Status;
        }

        public override void Terminate()
        {
            Owner.ExitBuilding();
        }
    }
}