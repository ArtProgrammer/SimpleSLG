using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleHomeGoal : GoalComposite<NormalPeople>
    {
        public NormalPeopleHomeGoal(NormalPeople p, int type) : base(p, type)
        {

        }

        public override void Activate()
        {
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            return GoalStatus.Inactive;
        }

        public override void Terminate()
        {
        }
    }
}