using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class HotWeapon : BaseWeapon
    {
        public int CurAmmorCount;

        public override bool IsAvailable()
        {
            if (CurAmmorCount <= 0)
            {
                return false;
            }

            return base.IsAvailable();
        }
    }
}