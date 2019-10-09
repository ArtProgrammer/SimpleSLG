using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    /// <summary>
    /// This system used to get the targets of the owner at runtime.
    /// </summary>
    public class TargetSystem
    {
        public BaseSoldier Owner = null;

        protected BaseSoldier TargetInMind = null;

        public Action<BaseRole> OnTargetGet;

        public TargetSystem(BaseSoldier owner)
        {
            Owner = owner;
        }

        public BaseSoldier CurTarget
        {
            set
            {
                TargetInMind = value;

                if (OnTargetGet != null)
                {
                    OnTargetGet(value);
                }
            }
            get
            {
                return TargetInMind;
            }
        }

        public virtual void Initialize()
        {
            
        }

        List<BaseSoldier> PotentialOps = new List<BaseSoldier>();
        public virtual void Process(float dt)
        {
            float sqrClosestDist = float.MaxValue;

            PotentialOps.Clear();

            CurTarget = null;

            PotentialOps = Owner.Sensor.GetRecentlySensedOpponents();

            for (int i = 0; i < PotentialOps.Count; i++)
            {
                if (PotentialOps[i].IsAlive && 
                    (!System.Object.ReferenceEquals(PotentialOps[i], Owner)) &&
                    PotentialOps[i] != Owner)
                {
                    if (PotentialOps[i].ID == Owner.ID)
                    {
                        int j = 0;
                    }

                    float sqrDist = Vector3.SqrMagnitude(Owner.Position -
                        PotentialOps[i].Position);

                    if (sqrDist < sqrClosestDist)
                    {
                        sqrClosestDist = sqrDist;
                        CurTarget = PotentialOps[i];
                    }
                }
            }
        }

        public virtual bool IsTargetPresent()
        {
            return !System.Object.ReferenceEquals(CurTarget, null);
        }

        public virtual bool IsTargetWithInFOV()
        {
            return Owner.Sensor.IsOpponentWithinFOV(CurTarget);
        }

        public virtual bool IsTargetAttackable()
        {
            return Owner.Sensor.IsOpponentAttackable(CurTarget);
        }

        public virtual bool CanTargetAttack()
        {
            bool isAttackable = false;
            if (Owner.Sensor.IsOpponentAttackable(CurTarget))
            {
                if (CombatHolder.Instance.IsInAttackRange(Owner, CurTarget))
                {
                    isAttackable = true;
                }
            }
            return isAttackable;
        }

        public Vector3 GetLastRecordedPosition()
        {
            return Owner.Sensor.GetLastRecordedPositionOfOpponent(CurTarget);
        }

        public float GetTimeTargetHasBeenVisible()
        {
            return Owner.Sensor.GetTimeOpponentHasBeenVisible(CurTarget);
        }

        public float GetTimeTargetHasBeenOutOfView()
        {
            return Owner.Sensor.GetTimeOpponentHasBeenOutOfView(CurTarget);
        }

        public virtual void ClearTarget()
        {
            CurTarget = null;
        }
    }
}
