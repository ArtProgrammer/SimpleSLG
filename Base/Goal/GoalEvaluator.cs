using SimpleAI.Game;

namespace SimpleAI
{
    public class GoalEvaluator<T> where T : BaseEntity
    {
        private float CharactorBias;

        public float Bias
        {
            set
            {
                CharactorBias = value;
            }
            get
            {
                return CharactorBias;
            }
        }

        public GoalEvaluator(float bias)
        {
            CharactorBias = bias;
        }

        public virtual float CalculateDesirability(T p)
        {
            return 0.0f;
        }

        public virtual void SetGoal(T p)
        {

        }
    }
}
