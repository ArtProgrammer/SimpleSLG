using SimpleAI.Game;
using SimpleAI.Messaging;

namespace SimpleAI
{
    public enum GoalStatus
    {
        Active,
        Inactive,
        Complete,
        Failed
    }

    public class Goal<T> where T : BaseEntity, ITelegramReceiver
    {
        protected int Type = 0;

        protected T Owner = null;

        protected GoalStatus Status = GoalStatus.Inactive;

        protected void ActiveIfInactive()
        {
            if (IsInactive)
            {
                Activate();
            }
        }

        protected void ReactivateIfFailed()
        {
            if (HasFailed)
            {
                Status = GoalStatus.Inactive;
            }
        }

        public Goal(T p, int type)
        {
            Owner = p;
            Type = type;
            Status = GoalStatus.Inactive;
        }

        public virtual void Activate() { }

        public virtual GoalStatus Process()
        {
            return GoalStatus.Inactive;
        }

        public virtual void Terminate() { }

        public virtual bool HandleMessage(Telegram msg) { return false; }

        public virtual void AddSubGoal(Goal<T> g) { }

        public bool IsComplete
        {
            get
            {
                return Status == GoalStatus.Complete;
            }
        }

        public bool IsActive
        {
            get
            {
                return Status == GoalStatus.Active;
            }
        }

        public bool IsInactive
        {
            get
            {
                return Status == GoalStatus.Inactive;
            }
        }

        public bool HasFailed
        {
            get
            {
                return Status == GoalStatus.Failed;
            }
        }

        public int GoalType
        {
            get
            {
                return Type;
            }
        }
    }
}
