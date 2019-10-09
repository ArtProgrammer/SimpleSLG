using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent
{
    public class FSMSoldierStateMachine : BaseStateMachine
    {
        public BaseSoldier Owner { set; get; }

        public FSMSoldierStateMachine(BaseSoldier owner) : base()
        {
            Owner = owner;

            FSMSoldierAlertState alertState = new FSMSoldierAlertState(owner);
            FSMSoldierAttackState attackState = new FSMSoldierAttackState(owner);

            AddNode(alertState);
            AddNode(attackState);

            CurState = alertState;

            //FSMSoldierTransition<BaseSoldier> trans1 = new FSMSoldierTransition<BaseSoldier>();
            //trans1.Src = alertState.ID;
            //trans1.Dst = attackState.ID;
            //trans1.TransitCondition((BaseSoldier par) => {
            //    return par.TargetSys.CurTarget &&
            //        CombatHolder.Instance.IsInAlertRange(par, par.TargetSys.CurTarget, 30.0f);
            //});
            //AddEdge(trans1);

            //FSMSoldierTransition<BaseSoldier> trans2 = new FSMSoldierTransition<BaseSoldier>();
            //trans2.TransitCondition((BaseSoldier par) => {
            //    return par.TargetSys.CurTarget == null ||
            //        !CombatHolder.Instance.IsInAlertRange(par, par.TargetSys.CurTarget, 30.0f);
            //});

            BaseTransition trans1 = new BaseTransition();
            trans1.Src = alertState.ID;
            trans1.Dst = attackState.ID;
            trans1.TransitCondition(() => {
                return Owner.TargetSys.CurTarget != null && Owner.TargetSys.CurTarget.IsAlive;
                //&&
                //    CombatHolder.Instance.IsInAlertRange(Owner, Owner.TargetSys.CurTarget, 30.0f);
            });
            AddEdge(trans1);

            BaseTransition trans2 = new BaseTransition();
            trans2.TransitCondition(() => {
                return Owner.TargetSys.CurTarget == null || !Owner.TargetSys.CurTarget.IsAlive;
                //||
                //    !CombatHolder.Instance.IsInAlertRange(Owner, Owner.TargetSys.CurTarget, 30.0f);
            });
            trans2.Src = attackState.ID;
            trans2.Dst = alertState.ID;
            AddEdge(trans2);
        }

        public void Initialize()
        {

        }

        public override void Process(float dt)
        {
            if (IsActive)
            {
                var edges = GetEdgesFrom(CurState.ID);
                foreach (var edge in GetEdgesFrom(CurState.ID))
                {
                    var trans = (BaseTransition)edge;
                    if (trans.ReadyToTransit())
                    {
                        MakeTransition(trans);
                        break;
                    }
                    else
                    {
                        CurState.Process(dt);
                    }
                }
            }
        }
    }
}