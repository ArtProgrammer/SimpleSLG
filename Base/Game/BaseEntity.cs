using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;
using SimpleAI.Spatial;
using SimpleAI.Messaging;

namespace SimpleAI.Game
{
    public class BaseEntity : SpatialFruitNode, ITelegramReceiver, IUpdateable
    {
        #region PRIVATE_PROPERTIES


        #endregion PRIVATE_PROPERTIES

        #region PROTECTED_PROPERTIES
        [SerializeField]
        protected int TheID = 0;
        #endregion PROTECTED_PROPERTIES

        #region PUBLIC_PROPERTIES
        public bool IsActive = true;

        public bool IsPlayerControl = false;

        public int ID
        {
            get { return TheID; }
        }
        #endregion PUBLIC_PROPERTIES

        void Awake()
        {
            PreInitialize();
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        void OnDestroy()
        {
            Finish();
        }

        public virtual bool HandleMessage(Telegram msg)
        {
            return false;
        }

        public virtual void OnUpdate(float dt)
        {
            if (IsActive)
            {
                TheUpdate();
                Process(dt);
            }
        }

        public virtual void OnFixedUpdate(float dt)
        {
            if (IsActive)
            {
                FixedProcess(dt);
            }
        }

        #region Override_Methods
        public virtual void PreInitialize()
        {
            TheID = IDAllocator.Instance.GetID();
        }

        public virtual void Initialize()
        {
            TheStart();

            GameLogicSupvisor.Instance.Register(this);            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Finish()
        {
            if (IDAllocator.IsAlive)
            {
                IDAllocator.Instance.RecycleID(TheID);
            }

            if (GameLogicSupvisor.IsAlive)
            {
                GameLogicSupvisor.Instance.Unregister(this);
            }            

            HandleDestory();
        }

        public virtual void Process(float dt)
        {

        }

        public virtual void FixedProcess(float dt)
        {

        }
        #endregion Override_Methods
    }
}