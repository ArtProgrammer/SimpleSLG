using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public delegate bool TransitChecker<T>(T owner);

    public class FSMSoldierTransition<T> : BaseTransition
    {
        TransitChecker<T> Conditions;

        public void TransitCondition(TransitChecker<T> func)
        {
            Conditions += func;
        }

        public bool ReadyToTransit(T owner)
        {
            if (Conditions != null)
            {
                return Conditions(owner);
            }

            return false;
        }
    }
}