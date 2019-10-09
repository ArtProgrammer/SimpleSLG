using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI;
using SimpleAI.PoolSystem;

namespace GameContent
{
    public class BaseRole : BaseEntity, IPoolableComponent
    {
        public int HP = 100;

        public bool IsAlive = true;

        public BaseMemory Memory = null;

        public int MoneyLeft = 0;

        public int HungryRate = 0;

        public Transform HouseTrans;

        public Transform WorkPlace;

        public int Energy = 100;

        public float EnergyDecreaseRate = 0.1f;

        public float HappinessRate = 1.0f;

        [SerializeField]
        protected float TheMoveSpeed = 3.0f;

        // Start is called before the first frame update
        #region PRIVATE_FUNCTIONS

        #endregion

        #region PUBLIC_FUNCTIONS
        public BaseRole()
        {

        }

        public virtual void Spawned()
        {
            IsActive = true;
            IsAlive = true;
            gameObject.SetActive(true);
        }

        public virtual void Despawned()
        {
            IsActive = false;
            IsAlive = false;
            gameObject.SetActive(false);
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            Memory = new BaseMemory();
            Memory.PreInitialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            Memory.Initialize();
        }

        public override void Process(float dt)
        {
            Memory.Process(dt);
        }

        public override void FixedProcess(float dt)
        {
            Memory.FixedProcess(dt);
        }

        public override bool HandleMessage(Telegram msg)
        {
            return false;
        }
        #endregion
    }
}