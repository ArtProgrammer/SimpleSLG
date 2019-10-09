using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class SoldierMoveToGoalEval : GoalEvaluator<BaseSoldier>
    {
        public SoldierMoveToGoalEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(BaseSoldier p)
        {
            return p.ForceMove * Bias;
        }

        public override void SetGoal(BaseSoldier p)
        {
            p.Brain.AddForceMoveGoal();
        }
    }
}