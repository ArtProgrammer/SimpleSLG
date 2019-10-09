using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public enum NormalPeopleGoalType
    {
        Home,
        Food,
        Work,
        Funny
    }

    public class NormalPeopleThink : GoalComposite<NormalPeople>
    {
        private List<GoalEvaluator<NormalPeople>> Evaluators =
            new List<GoalEvaluator<NormalPeople>>();

        private GoalEvaluator<NormalPeople> MostDesirable = null;

        public NormalPeopleThink(NormalPeople p, int type) : base(p, type)
        {
            Evaluators.Add(new NormalPeopleFoodGoalEval(0.9f));
            Evaluators.Add(new NormalPeopleWorkEval(0.5f));
            //Evaluators.Add(new NormalPeopleHomeGoalEval(0.5f));
            //Evaluators.Add(new NormalPeopleFunnyGoalEval(0.5f));
        }

        ~NormalPeopleThink()
        {
            Evaluators.Clear();
            MostDesirable = null;
        }

        public void AddEvaluator(GoalEvaluator<NormalPeople> p)
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

        public void AddGotFoodGoal()
        {
            if (NotPresent((int)NormalPeopleGoalType.Food))
            {
                RemoveAllSubgoals();
                AddSubGoal(new NormalPeopleFoodGoal(Owner, (int)NormalPeopleGoalType.Food));
            }
        }

        public void AddGoHomeGoal()
        {
            if (NotPresent((int)NormalPeopleGoalType.Home))
            {
                RemoveAllSubgoals();
                AddSubGoal(new NormalPeopleHomeGoal(Owner, (int)NormalPeopleGoalType.Home));
            }
        }

        public void AddWorkGoal()
        {
            if (NotPresent((int)NormalPeopleGoalType.Work))
            {
                //Debug.Log("$Add attack goal");
                RemoveAllSubgoals();
                AddSubGoal(new NormalPeopleWorkGoal(Owner, (int)NormalPeopleGoalType.Work));
            }
        }

        public void AddFunnyGoal()
        {
            if (NotPresent((int)NormalPeopleGoalType.Funny))
            {
                //Debug.Log("$Add attack goal");
                RemoveAllSubgoals();
                AddSubGoal(new NormalPeopleFunnyGoal(Owner, (int)NormalPeopleGoalType.Funny));
            }
        }

    }
}