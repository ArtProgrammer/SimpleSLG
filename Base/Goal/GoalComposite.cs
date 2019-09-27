using System.Collections.Generic;

using SimpleAI.Game;
using SimpleAI.Messaging;

namespace SimpleAI
{
    public class GoalComposite<T> : Goal<T> where T : BaseEntity
    {
        public GoalComposite(T p, int type) : base(p, type)
        {

        }

        ~GoalComposite()
        {
            RemoveAllSubgoals();
        }

        public override void Activate() { }

        public override GoalStatus Process()
        {
            return GoalStatus.Inactive;
        }

        public override void Terminate() { }

        public override bool HandleMessage(Telegram msg)
        {
            return ForwardMessageToFrontSubgoal(msg);
        }

        public override void AddSubGoal(Goal<T> g)
        {
            SubGoals.Insert(0, g);
        }

        public void RemoveAllSubgoals()
        {
            for (int i = 0; i < SubGoals.Count; i++)
            {
                if (SubGoals[i] != null)
                {
                    SubGoals[i].Terminate();
                }
            }

            SubGoals.Clear();
        }

        protected List<Goal<T>> SubGoals = new List<Goal<T>>();

        protected virtual GoalStatus ProcessSubgoals()
        {
            while (SubGoals.Count > 0)
            {
                Goal<T> firstgoal = SubGoals[0];
                if (firstgoal.IsComplete || firstgoal.HasFailed)
                {
                    firstgoal.Terminate();
                    SubGoals.Remove(firstgoal);
                }else
                {
                    break;
                }
            }

            if (SubGoals.Count > 0)
            {
                GoalStatus status = SubGoals[0].Process();

                if (status == GoalStatus.Complete && SubGoals.Count > 1)
                {
                    return GoalStatus.Active;
                }

                return status;
            }
            else
            {
                return GoalStatus.Complete;
            }
        }

        protected bool ForwardMessageToFrontSubgoal(Telegram msg)
        {
            if (SubGoals.Count > 0)
            {
                return SubGoals[0].HandleMessage(msg);
            }

            return false;
        }
    }
}
