using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class CombatHolder
    {
        private static CombatHolder TheInstance = new CombatHolder();

        public static CombatHolder Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public bool IsEnemy(int isrcGangID, int dstGangID)
        {
            return GangManager.Instance.IsEnemyGangs(isrcGangID, dstGangID);
        }

        public bool IsAllien(int isrcGangID, int dstGangID)
        {
            return GangManager.Instance.IsAllienGangs(isrcGangID, dstGangID);
        }

        public bool IsCloseEnough(BaseEntity src, BaseEntity dst)
        {
            Vector3 dis = src.Position - dst.Position;

            float radiuses = src.CollideRadius + dst.CollideRadius;

            return dis.sqrMagnitude < radiuses * radiuses;
        }

        public bool IsCloseEnough(BaseEntity src, BaseEntity dst,
            float distance)
        {
            Vector3 dis = src.Position - dst.Position;
            return dis.sqrMagnitude < distance * distance;
        }

        /// <summary>
        /// Check weather a point int the close range.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsCloseEnough(BaseEntity src, Vector3 pos)
        {
            Vector3 dis = src.Position - pos;

            return dis.sqrMagnitude < src.CollideRadius * src.CollideRadius;
        }

        public bool IsCloseEnough(BaseEntity src, Vector3 pos, float range)
        {
            Vector3 dis = src.Position - pos;

            return dis.sqrMagnitude < range * range;
        }

        public bool IsInAttackRange(BaseEntity src, BaseEntity dst)
        {
            Vector3 dis = src.Position - dst.Position;

            float radiuses = src.AttackRadius + dst.CollideRadius;

            return dis.sqrMagnitude < radiuses * radiuses;
        }

        public bool IsInAttackRange(BaseEntity src, BaseEntity dst,
            float distance)
        {
            Vector3 dis = src.Position - dst.Position;

            return dis.sqrMagnitude < distance * distance;
        }

        public bool IsInAttackRange(BaseEntity src, Vector3 pos)
        {
            Vector3 dis = src.Position - pos;

            return dis.sqrMagnitude < src.AttackRadius * src.AttackRadius;
        }

        public bool IsInAlertRange(BaseEntity src, BaseEntity dst,
            float distance)
        {
            Vector3 dis = src.Position - dst.Position;

            return dis.sqrMagnitude < distance * distance;
        }

        Vector3 Src2Dst = Vector3.zero;
        public bool IsSecondInFOVOfFirst(Vector3 srcPos,
            Vector3 facing, Vector3 dstPos, float fov)
        {
            Src2Dst = dstPos - srcPos;
            Src2Dst.Normalize();

            return Vector3.Dot(facing, Src2Dst) >= Mathf.Cos(fov * 0.5f);
        }

        public bool IsLOSOkay(Vector3 srcPos, Vector3 dstPos)
        {
            return true;
        }
    }
}
