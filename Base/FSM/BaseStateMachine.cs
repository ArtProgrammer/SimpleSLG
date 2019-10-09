using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Graphs;

namespace SimpleAI
{
    public class BaseStateMachine : SparseGraph<BaseFSMState, BaseTransition>
    {
        protected BaseFSMState CurState;

        public bool IsActive;

        //public BaseStateMachine() : base(true)
        //{
        //    IsActive = true;
        //}

        public BaseStateMachine(bool dirgraph = true) : base(dirgraph)
        {
            IsActive = true;
        }

        public void MakeTransition(BaseTransition trans)
        {
            if (trans != null)
            {
                var src = GetNode(trans.Src);
                var dst = GetNode(trans.Dst);

                if (src != null)
                {
                    src.OnExit();
                }

                if (dst != null)
                {
                    dst.OnEnter();
                }

                CurState = dst;
            }
        }

        public virtual void Process(float dt)
        {
            if (CurState != null)
            {
                CurState.Process(dt);
            }
        }
    }
}