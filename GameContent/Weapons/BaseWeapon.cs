using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Timer;

namespace GameContent
{
    /// <summary>
    /// Martial has most quick attack frequency,
    /// next is cold weapon,
    /// final is hot weapon.
    /// but the experiences of weapon can minimal the frequency.
    /// </summary>
    public enum WeaponCategory
    {
        Martial,
        ColdWeapon,
        HotWeapon
    }

    public class BaseWeapon
    {
        public bool NeedBullet;

        public int ID;

        public int CfgID;

        public float AttactInteval;

        public Regulator Reg;

        public float Weight;

        public bool HasAmmo;

        public float CurDurability;

        public float MaxDurability = 100.0f;

        public float CurAttackRange;

        public float BaseAttackValue;

        public float CurAccuracyRate;

        public virtual bool IsAvailable()
        {
            if (CurDurability <= 0.0f)
            {
                return false;
            }

            return true;
        }

        public virtual void TryUse(Vector3 pos)
        {
            if (Reg.IsReady())
            {
                // 
            }
        }

        public virtual void TryUse(Quaternion dir)
        {
            if (Reg.IsReady())
            {
                // 
            }
        }

        public virtual void TryUse(BaseEntity target)
        {
            if (Reg.IsReady())
            {
                // 
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void Process(float dt = 0.0f)
        {

        }

        public virtual void Finish()
        {

        }
    }
}