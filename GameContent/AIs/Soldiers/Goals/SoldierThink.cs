using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public enum SoldierGoalType
    {
        Attack,
        Defence,
        ForceMove
    }

    public class SoldierThink : GoalComposite<BaseSoldier>
    {
        private List<GoalEvaluator<BaseSoldier>> Evaluators =
            new List<GoalEvaluator<BaseSoldier>>();

        private GoalEvaluator<BaseSoldier> MostDesirable = null;

        public SoldierThink(BaseSoldier p, int type) : base(p, type)
        {
            //AddEvaluator(new SoldierDefenceGoalEval(1.0f));
            AddEvaluator(new SoldierAttackGoalEval(1.0f));
            AddEvaluator(new SoldierMoveToGoalEval(0.8f));
        }

        ~SoldierThink()
        {
            Evaluators.Clear();
            MostDesirable = null;
        }

        public void AddEvaluator(GoalEvaluator<BaseSoldier> p)
        {
            Evaluators.Add(p);
        }

        public void Arbitrate()
        {
            double best = 0;
            MostDesirable = null;

            for (int i = 0; i < Evaluators.Count; i++)
            {
                double desire = Evaluators[i].CalculateDesirability(Owner);

                if (desire >= best)
                {
                    best = desire;
                    MostDesirable = Evaluators[i];
                }
            }

            if (!System.Object.ReferenceEquals(MostDesirable, null))
                MostDesirable.SetGoal(Owner);
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            GoalStatus substatus = ProcessSubgoals();

            if (substatus == GoalStatus.Complete ||
                substatus == GoalStatus.Failed)
            {
                Status = GoalStatus.Inactive;
            }

            return Status;
        }

        public override void Activate()
        {
            Arbitrate();

            Status = GoalStatus.Active;
        }

        public bool NotPresent(int goaltype)
        {
            if (SubGoals.Count > 0)
            {
                return SubGoals[0].GoalType != goaltype;
            }

            return true;
        }

        public void AddAttackGoal()
        {
            if (NotPresent((int)SoldierGoalType.Attack))
            {
                RemoveAllSubgoals();
                AddSubGoal(new SoldierAttackGoal(Owner, (int)SoldierGoalType.Attack));
            }
        }

        public void AddDefenceGoal()
        {
            if (NotPresent((int)SoldierGoalType.Defence))
            {
                RemoveAllSubgoals();
                AddSubGoal(new SoldierDefenceGoal(Owner, (int)SoldierGoalType.Defence));
            }
        }

        public void AddForceMoveGoal()
        {
            if (NotPresent((int)SoldierGoalType.ForceMove))
            {
                RemoveAllSubgoals();
                AddSubGoal(new SoldierMoveToGoal(Owner, (int)SoldierGoalType.ForceMove));
            }
        }
    }
}