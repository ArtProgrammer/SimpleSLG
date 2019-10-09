using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleFleeGoalEval : GoalEvaluator<NormalPeople>
    {
        public NormalPeopleFleeGoalEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(NormalPeople p)
        {
            return 0.0f;
        }

        public override void SetGoal(NormalPeople p)
        {

        }
    }
}