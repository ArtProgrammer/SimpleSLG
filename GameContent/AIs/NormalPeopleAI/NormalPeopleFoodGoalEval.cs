using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleFoodGoalEval : GoalEvaluator<NormalPeople>
    {
        public bool IsEnergyNeed = false;

        public NormalPeopleFoodGoalEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(NormalPeople p)
        {
            if (p.Energy >= 100)
            {
                IsEnergyNeed = false;
            }

            if (p.Energy < 10 || IsEnergyNeed)
            {
                IsEnergyNeed = true;
                return 1.0f * Bias;
            }

            return 0.0f;
        }

        public override void SetGoal(NormalPeople p)
        {
            p.Brain.AddGotFoodGoal();
        }
    }
}