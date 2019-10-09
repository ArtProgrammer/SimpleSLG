using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleFunnyGoal : GoalComposite<NormalPeople>
    {
        public NormalPeopleFunnyGoal(NormalPeople p, int type) : base(p, type)
        {

        }
    }
}