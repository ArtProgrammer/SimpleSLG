﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class NormalPeopleHomeGoalEval : GoalEvaluator<NormalPeople>
    {
        

        public NormalPeopleHomeGoalEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(NormalPeople p)
        {
            

            return 0.0f;
        }

        public override void SetGoal(NormalPeople p)
        {
            p.Brain.AddGoHomeGoal();
        }
    }
}