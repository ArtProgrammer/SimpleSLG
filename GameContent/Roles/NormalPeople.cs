using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using SimpleAI;
using SimpleAI.Game;
using SimpleAI.Timer;

namespace GameContent
{
    public class NormalPeople : BaseRole
    {
        public Transform WorkLocation;

        public Transform HouseLocation;

        public Transform FoodLocation;

        public Transform Body;

        public NavMeshAgent NavAgent;

        public NormalPeopleThink Brain;

        [SerializeField]
        protected float BrainRegIntervel = 10.0f;

        protected Regulator BrainReg;

        [SerializeField]
        protected float FoodIntervel = 3.0f;

        protected Regulator FoodReg;

        [SerializeField]
        protected float WorkInterval = 15.0f;

        protected Regulator WorkReg;

        public bool IsInBuilding = false;

        public float MoveSpeed
        {
            set
            {
                if (TheMoveSpeed != value)
                {
                    NavAgent.speed = value;
                    TheMoveSpeed = value;
                }
            }
            get
            {
                return TheMoveSpeed;
            }
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            NavAgent = GetComponent<NavMeshAgent>();

            NavAgent.speed = MoveSpeed;

            BrainReg = new Regulator(BrainRegIntervel);

            FoodReg = new Regulator(FoodIntervel);

            WorkReg = new Regulator(WorkInterval);
        }

        public override void Initialize()
        {
            base.Initialize();

            Memory.AddLocation("Work", WorkLocation);
            Memory.AddLocation("House", HouseLocation);
            Memory.AddLocation("Food", FoodLocation);

            Brain = new NormalPeopleThink(this, 1);

            //transform.position = Memory.GetLocation("House").position;
        }

        public override void Process(float dt)
        {
            base.Process(dt);

            if (BrainReg.IsReady())
            {
                Brain.Arbitrate();
                Brain.Process();
            }
        }

        public void Move(Vector3 pos)
        {
            NavAgent.isStopped = false;
            NavAgent.SetDestination(pos);
        }

        public void GetEnergy()
        {
            if (FoodReg.IsReady())
            {
                Energy += 10;

                MoneyLeft -= 1;
            }
        }

        public void Work()
        {
            if (WorkReg.IsReady())
            {
                Energy -= 5;

                MoneyLeft += 5;
            }
        }

        public bool IsArrive()
        {
            return NavAgent.remainingDistance <= 3.0f;
        }

        public void StopMove()
        {
            NavAgent.isStopped = true;
        }


        public void EnterBuilding()
        {
            if (!IsInBuilding)
            {
                //Body.gameObject.SetActive(false);
                IsInBuilding = true;
            }
            
        }

        public void ExitBuilding()
        {
            if (IsInBuilding)
            {
                IsInBuilding = false;
                //Body.gameObject.SetActive(true);
            }
        }
    }
}