using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class SoldierAttackGoalEval : GoalEvaluator<BaseSoldier>
    {
        public SoldierAttackGoalEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(BaseSoldier p)
        {
            return p.AttackWilling * Bias;
        }

        public override void SetGoal(BaseSoldier p)
        {
            p.Brain.AddAttackGoal();
        }
    }
}