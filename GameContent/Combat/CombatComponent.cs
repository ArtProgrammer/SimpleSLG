using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class CombatComponent : MonoBehaviour, IUpdateable
    {
        public bool IsActive;

        #region RUNTIME_PROPERTIES
        public float MartialExp;

        public float ColdWeaponExp;

        public float HotWeaponExp;

        public int AmmorKind;

        public int CurAmmor;

        public int MaxAmmor;

        public int CurEquipWeight;

        public int MaxEquipWeight;

        public float CurEnergy;

        /// <summary>
        /// MaxEnergy can be trained to increase.
        /// </summary>
        public float MaxEnergy;

        /// <summary>
        /// Combat experience arise with any fight.
        /// </summary>
        public float CombatExp;

        public BaseWeapon CurWeapon;

        public List<BaseWeapon> Weapons = new List<BaseWeapon>();

        #endregion RUNTIME_PROPERTIES

        void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        public float MatialFactor()
        {
            return MartialExp;
        }

        public float ColdWeaponFactor()
        {
            return ColdWeaponExp;
        }

        public float HotWeaponFactor()
        {
            return HotWeaponExp;
        }

        public float RateUseMatialWeapon()
        {
            // if no ammor, cold weapon, use matial.
            return 0.0f;
        }

        public float RateUseColdWeapon()
        {
            // if no ammor and has cold weapon, cold weapon.
            return 0.0f;
        }

        public float RateUseHotWeapon()
        {
            // if has hot weapon and ammor, use hot weapon.
            return 0.0f;
        }

        public void LoadWeapons()
        {
            MartialWeapon martialWeapon = new MartialWeapon();
            Weapons.Add(martialWeapon);
        }

        public void AutoChooseWeapon()
        {
            if (CurWeapon.IsAvailable())
                return;

            for (int i = Weapons.Count - 1; i >= 0; i--)
            {
                if (Weapons[i].IsAvailable())
                {
                    SwitchWeapon(Weapons[i]);
                    break;
                }
            }
        }

        public Action<int> OnWeaponChange;

        public void SwitchWeapon(BaseWeapon weapon)
        {
            if (CurWeapon != weapon)
            {
                CurWeapon = weapon;

                if (OnWeaponChange != null)
                {
                    OnWeaponChange(CurWeapon.ID);
                }
            }
        }

        public void TryUseWeapon()
        {
            if (CurWeapon.IsAvailable())
            {
                //CurWeapon.TryUse();
            }
        }

        public void Initialize()
        {
            IsActive = true;

            LoadWeapons();
        }

        public void Process(float dt = 0.0f)
        {
            AutoChooseWeapon();

            // If has target, try use weapon.
            TryUseWeapon();
        }

        public void OnUpdate(float dt)
        {

        }

        public void OnFixedUpdate(float dt)
        {
            if (IsActive)
            {
                Process(dt);
            }
        }
    }
}