using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI;
using SimpleAI.Game;
using SimpleAI.Timer;
using SimpleAI.Utils;

using Config;
using GameContent;

namespace GameContent
{
    public class BaseSoldier : BaseRole
    {
        public int ID = 0;

        public int GangID = 0;

        public int CfgID = 0;

        //public Transform WorkLocation;

        public NavMeshAgent NavAgent;

        public SimSensor<BaseSoldier> Sensor;

        public TargetSystem TargetSys;

        public CombatComponent CombatCom;

        public bool UseGoals = true;

        public SoldierThink Brain;

        public bool UseFSM = false;

        public FSMSoldierStateMachine FSM;

        [SerializeField]
        protected float BrainRegIntervel = 10.0f;

        protected Regulator BrainReg;

        [SerializeField]
        protected float SensorRegIntervel = 8.0f;

        protected Regulator SensorReg;

        [SerializeField]
        protected float TargetSysRegIntervel = 1.0f;
        protected Regulator TargetSysReg;

        //
        private float TheAttackInterval = 1.0f;
        public float AttackIntervel
        {
            set
            {
                if (TheAttackInterval != value)
                {
                    TheAttackInterval = value;

                    if (AttackReg != null)
                    {
                        AttackReg.SetNumUpdatesPerSec(value);
                    }
                    else
                    {
                        AttackReg = new Regulator(value);
                    }
                }
            }
            get
            {
                return TheAttackInterval;
            }
        }
        public Regulator AttackReg = new Regulator(10.0f);

        public float MoveSpeed
        {
            set
            {
                if (!TheMoveSpeed.Equals(value))
                {
                    TheMoveSpeed = value;

                    if (NavAgent != null)
                    {
                        NavAgent.speed = TheMoveSpeed;
                    }
                }
            }
            get
            {
                return TheMoveSpeed;
            }
        }

        public Action<int> OnSoldierDie;

        // test property
        public Vector3 DstPos = Vector3.zero;

        public float ForceMove = 0.5f;

        public float AttackWilling = 0.1f;

        public BaseSoldier CurTarget = null;

        public int HPs
        {
            set
            {
                if (HP != value)
                {
                    HP = value;

                    if (HP < 0)
                    {
                        IsAlive = false;
                        Despawned();

                        if (OnSoldierDie != null)
                        {
                            OnSoldierDie(ID);
                        }
                    }
                }
            }
            get
            {
                return HP;
            }
        }

        public int AttackValue = 3;

        public void OnTargetGet(BaseRole target)
        {
            if (target != null)
                AttackWilling = 1.0f;
            else
                AttackWilling = 0.1f;

            CurTarget = (BaseSoldier)target;
        }

        public void ChangeGang(int id)
        {
            GangID = id;
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            NavAgent = GetComponent<NavMeshAgent>();

            ID = IDAllocator.Instance.GetID();
        }

        public override void Initialize()
        {
            base.Initialize();

            LoadCfgData(CfgID);

            BrainReg = new Regulator(BrainRegIntervel);

            SensorReg = new Regulator(SensorRegIntervel);

            TargetSysReg = new Regulator(TargetSysRegIntervel);

            Brain = new SoldierThink(this, 1);

            FSM = new FSMSoldierStateMachine(this);

            Sensor = new SimSensor<BaseSoldier>(this);

            Sensor.Range = AlertRadius;

            Sensor.Initialize();

            CombatCom = GetComponent<CombatComponent>();

            TargetSys = new TargetSystem(this);

            TargetSys.OnTargetGet = OnTargetGet;

            Memory.AddLocation("Work", WorkPlace);
        }

        public void LoadCfgData(int cfgid)
        {
            //if (CfgID != cfgid)
            {
                CfgID = cfgid;

                var cfgData = ConfigDataMgr.Instance.CharacterCfgLoader.GetDataByID(cfgid);

                if (cfgData != null)
                {
                    AlertRadius = cfgData.AlertRange;

                    AttackRadius = cfgData.AttackRange;

                    AttackValue = cfgData.Attack;

                    AttackIntervel = cfgData.AttackSpeed;

                    MoveSpeed = cfgData.MoveSpeed;
                }
            }
        }

        public override void Process(float dt)
        {
            base.Process(dt);

            if (BrainReg.IsReady())
            {
                if (UseGoals)
                {
                    Brain.Arbitrate();

                    Brain.Process();
                }

                if (UseFSM)
                {
                    FSM.Process(dt);
                }
            }

            if (SensorReg.IsReady())
            {
                Sensor.Process(dt);
            }

            if (TargetSysReg.IsReady())
            {
                TargetSys.Process(dt);
            }
        }

        public override void FixedProcess(float dt)
        {
            base.FixedProcess(dt);
        }

        public override bool HandleMessage(Telegram msg)
        {
            return false;
        }

        public void MoveTo(Vector3 pos)
        {
            if (NavAgent)
            {
                NavAgent.isStopped = false;
                DstPos = pos;
                NavAgent.SetDestination(pos);
            }
        }

        public void StopMove()
        {
            if (NavAgent)
            {
                NavAgent.isStopped = true;
            }
        }

        public bool IsArrive()
        {
            //if (NavAgent)
            //{
            //    //return NavAgent.remainingDistance <= 3.0f;
            //    return CombatHolder.Instance.IsCloseEnough(this, 
            //        NavAgent.destination, CollideRadius);
            //}

            if (TargetSys.CurTarget != null)
            {
                return CombatHolder.Instance.IsCloseEnough(this,
                    TargetSys.CurTarget.Position, CollideRadius);
            }
            else
            {
                return CombatHolder.Instance.IsCloseEnough(this,
                    DstPos, CollideRadius);
            }

            return false;
        }

        public void Attack()
        {
            if (AttackReg.IsReady() &&
                TargetSys.CurTarget != null)
            {
                TargetSys.CurTarget.HPs -= AttackValue;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, AlertRadius);
        }
    }
}