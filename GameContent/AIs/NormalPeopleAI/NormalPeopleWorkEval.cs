using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleWorkEval : GoalEvaluator<NormalPeople>
    {
        public NormalPeopleWorkEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(NormalPeople p)
        {
            return Bias;
        }

        public override void SetGoal(NormalPeople p)
        {
            p.Brain.AddWorkGoal();
        }
    }
}