using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI.Messaging;
using SimpleAI.Utils;
using SimpleAI.Logger;
using SimpleAI.Spatial;
//using GameContent;
//using GameContent.Defence;
//using GameContent.Skill;
//using GameContent.Agents;

//using GameContent.Item;

namespace SimpleAI.Game
{
    public class BaseGameEntity : BaseEntity //SpatialFruitNode, ITelegramReceiver, IUpdateable
    {
        [SerializeField]
        public int MaxXue = 100;

        [SerializeField]
        private int XueNum = 100;

        public int Xue
        {
            get
            {
                return XueNum;
            }
            set
            {
                XueNum = value;
                if (XueNum <= 0)
                {
                    XueNum = 0;
                }

                if (!System.Object.ReferenceEquals(null, OnXueChanged))
                {
                    OnXueChanged(XueNum);
                }
            }
        }

        public bool IsAlive
        {
            get
            {
                return XueNum >= 0;
            }
        }

        protected Action<int> OnXueChanged;

        public float TheMoveSpeed = 3.0f;

        public float MoveSpeed
        {
            set
            {
                if (!TheMoveSpeed.Equals(value))
                {
                    TheMoveSpeed = value;
                    NMAgent.speed = TheMoveSpeed;
                }                
            }
            get
            {
                return TheMoveSpeed;
            }
        }

        [SerializeField]
        private int QiNum = 100;

        public int Qi
        { 
            set
            {
                QiNum = value;

                if (!System.Object.ReferenceEquals(null, QiChanged))
                {
                    QiChanged(QiNum);
                }
            }
            get
            {
                return QiNum;
            }
        }

        protected Action<int> QiChanged;

        [SerializeField]
        protected int TheRaceSignal = 0;

        public int RaceSignal
        { 
            set
            {
                if (TheRaceSignal != value)
                {
                    TheRaceSignal = value;
                    //RaceType = DefenceSystem.Instance.Int2RaceType(TheRaceSignal);
                }
            }
            get
            {
                return TheRaceSignal;
            }
        }

        [SerializeField]
        public int RaceType
        {
            set;get;
        }

        [SerializeField]
        protected int TheCampType = 0;

        public int CampType
        { 
            set
            {
                TheCampType = value;
            }
            get
            {
                return TheCampType;
            }
        }

        public float AttackRadius = 1.0f;

        public float CollideRadius = 1.0f;

        //private List<BaseBuff> BuffList = 
        //    new List<BaseBuff>();

        public BaseGameEntity CurTarget = null;

        //protected SimSensor<BaseGameEntity> TheSensor = null;

        //public SimSensor<BaseGameEntity> SimSensorMem
        //{
        //    get
        //    {
        //        return TheSensor;
        //    }
        //}

        //public TargetSystem TargetSys
        //{
        //    set;get;
        //}

        public Transform Body = null;

        public Transform WeaponPoint = null;

        public Vector3 Facing = Vector3.forward;

        public float FOV = 60.0f;

        public BaseGameEntity Target
        {
            set
            {
                CurTarget = value;
            }
            get
            {
                return CurTarget;
            }
        }

        public bool IsTargetLost
        {
            get
            {
                return System.Object.ReferenceEquals(null, CurTarget);
            }
        }      

        //public void AddBuff(BaseBuff buff)
        //{
        //    BuffList.Add(buff);
        //}

        //public void RemoveBuff(BaseBuff buff)
        //{
        //    BuffList.Remove(buff);
        //}

        //protected void ProcessBuffs(ref float dt)
        //{ 
        //    for (int i = 0; i < BuffList.Count; i++)
        //    {
        //        BuffList[i].OnUpdate(dt);
        //    }
        //}

        //public void ClearBuffList()
        //{
        //    for (int i = 0; i < BuffList.Count; i++)
        //    {
        //        BuffList[i].Despawned();
        //    }

        //    BuffList.Clear();
        //}

        public virtual void LoadData()
        {
            //TheRaceType = 0;
            //TheCampType = 0;
        }

        public virtual void SaveData()
        { 

        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void GetPosition(ref Vector3 val)
        {
            val = transform.position;
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            EntityManager.Instance.RegisterEntity(this);

            if (IsPlayerControl)
            {
                EntityManager.Instance.PlayerEntity = this;
            }

            NMAgent = GetComponentInChildren<NavMeshAgent>();
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();            

            //TheSensor = new SimSensor<BaseGameEntity>(this);

            //TheSensor.Initialize();
        }

        /// <summary>
        /// Finish this instance.
        /// </summary>
        public override void Finish()
        {
            if (EntityManager.IsAlive)
            {
                EntityManager.Instance.RemoveEntity(this);
            }

            base.Finish();
        }

        public override void Process(float dt) 
        {
            base.Process(dt);

            //ProcessBuffs(ref dt);
        }

        public NavMeshAgent NMAgent = null;

        //public virtual void UseSkill(BaseSkill skill, ref Vector3 position)
        //{
        //    skill.SetOwner(this);
        //    SKillMananger.Instance.TryUseSkill(skill, ref position);
        //}

        //public virtual void UseSkill(BaseSkill skill, BaseGameEntity target)
        //{
        //    skill.SetOwner(this);
        //    SKillMananger.Instance.TryUseSkill(skill, target);
        //}

        public virtual void UseSkill(int skillid, Vector3 pos)
        {
            //skill.SetOwner(this);
            //SKillMananger.Instance.TryUseSkill(skillid, pos, this);
        }

        public virtual void UseSkill(int skillid, ref Vector3 pos)
        {
            //skill.SetOwner(this);
            //SKillMananger.Instance.TryUseSkill(skillid, ref pos, this);
        }

        public virtual void UseSkill(int skillid, BaseGameEntity target)
        {
            //skill.SetOwner(this);
            //SKillMananger.Instance.TryUseSkill(skillid, target, this);
        }

        //public virtual void UseItem(BaseItem item, BaseGameEntity target)
        //{ 
        //    if (!System.Object.ReferenceEquals(null, item) &&
        //        !System.Object.ReferenceEquals(null, target))
        //    {
        //        //item.Use(target);
        //        //ItemManager.Instance.TryUseItem()
        //    }
        //}

        public virtual void UseItem(int itemid, BaseGameEntity target)
        {
            //ItemManager.Instance.TryUseItem(itemid, target);
        }

        public virtual void UseItem(int itemid, Vector3 pos)
        {
            //ItemManager.Instance.TryUseItem(itemid, pos);
        }

        void OnDestroy()
        {
            //ClearBuffList();

            if (IDAllocator.IsAlive)
            {
                IDAllocator.Instance.RecycleID(TheID);
            }

            Finish();

            if (GameLogicSupvisor.IsAlive)
            {
                GameLogicSupvisor.Instance.Unregister(this);
            }

            if (EntityManager.IsAlive)
            {
                EntityManager.Instance.RemoveEntity(this);
            }

            HandleDestory();
        }

        public override bool HandleMessage(Telegram msg) 
        {
            TinyLogger.Instance.DebugLog(string.Format("$ BaseGameEntity handle message {0}", ID));
            return false;
        }
    }
}