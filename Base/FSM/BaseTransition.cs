using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Graphs;

namespace SimpleAI
{
    public delegate bool TransitChecker();

    public class BaseTransition : GraphEdge
    {
        TransitChecker Conditions;

        public void TransitCondition(TransitChecker func)
        {
            Conditions += func;
        }

        public bool ReadyToTransit()
        {
            if (Conditions != null)
            {
                return Conditions();
            }

            return false;
        }
    }
}