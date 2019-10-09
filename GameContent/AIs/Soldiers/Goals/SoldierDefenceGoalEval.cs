using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class SoldierDefenceGoalEval : GoalEvaluator<BaseSoldier>
    {
        public SoldierDefenceGoalEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(BaseSoldier p)
        {
            if (!p.TargetSys.IsTargetAttackable())
            {
                return 1.0f;
            }

            return 0.0f;
        }

        public override void SetGoal(BaseSoldier p)
        {

        }
    }
}