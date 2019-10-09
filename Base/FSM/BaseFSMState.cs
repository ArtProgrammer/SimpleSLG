using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;
using SimpleAI.Graphs;

namespace SimpleAI
{
    public class BaseFSMState : GraphNode
    {
        public BaseFSMState()
        {
            ID = IDAllocator.Instance.GetID();
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Process(float dt = 0.0f)
        {

        }

        public virtual void CheckTransition()
        {

        }
    }
}