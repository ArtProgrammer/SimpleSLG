using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI;
using SimpleAI.PoolSystem;
using SimpleAI.Timer;

namespace GameContent
{
    public class BaseBenefit : BaseEntity, IPoolableComponent
    {
        #region PRIVATE_MEMBER

        [SerializeField]
        private int TheOwnerID = 0;

        private List<int> OwnersID = new List<int>();

        [SerializeField]
        private int MoneyPerMonth = 0;

        [SerializeField]
        private int MoneyLeft = 0;

        [SerializeField]
        private float TaxRate = 0;

        [SerializeField]
        private float BenefitRate = 1;

        [SerializeField]
        private bool Taxed = false;

        private float MonthSeconds = 0;

        [SerializeField]
        private float MonthDays = 30;

        private Regulator BenefitReg;

        private List<int> EmployerIDs = new List<int>();

        [SerializeField]
        private int MaxEmployerCount = 6;

        [SerializeField]
        private int CurrentEmployerCount = 1;

        private float EmployerTaxRate = 1.0f;

        [SerializeField]
        private float CaptureRange = 5.0f;

        #endregion

        #region PUBLIC_MEMBER

        public string Name;

        public Transform EntryPos;

        public int CurEmployerCount
        {
            set
            {
                if (CurrentEmployerCount != value &&
                    value <= MaxEmployerCount)
                {
                    CurrentEmployerCount = value;
                    EmployerTaxRate = (float)CurEmployerCount / MaxEmployerCount;
                }
            }
            get
            {
                return CurrentEmployerCount;
            }
        }

        public int OwnerID
        {
            set
            {
                TheOwnerID = value;
            }
            get
            {
                return TheOwnerID;
            }
        }

        public void AddOwner(int id)
        {
            if (!OwnersID.Contains(id))
                OwnersID.Add(id);
        }

        public void RemoveOwner(int id)
        {
            OwnersID.Remove(id);
        }
        #endregion

        #region PRIVATE_FUNCTIONS

        #endregion

        #region PUBLIC_FUNCTIONS

        public void AddEmployer(int id)
        {

        }

        public void RemoveEmployer(int id)
        {

        }

        public virtual void Spawned()
        {
            
        }

        public virtual void Despawned()
        {
            TheOwnerID = 0;
            MoneyLeft = 0;
            TaxRate = 0;
            BenefitRate = 1;
        }

        public override void PreInitialize()
        {
            base.PreInitialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            MonthSeconds = GameDateTime.Instance.SecondPerDay * MonthDays;

            //BenefitReg = new Regulator(1.0f / MonthSeconds);

            BenefitReg = new Regulator(3.0f);
        }

        public override void Process(float dt)
        {
            if (BenefitReg.IsReady())
            {
                PushTax();
            }
        }

        public override void FixedProcess(float dt)
        {

        }

        public override bool HandleMessage(Telegram msg)
        {
            return false;
        }

        public void PushTax()
        {
            if (OwnersID.Count == 0) return;

            int num = (int)(MoneyPerMonth * TaxRate * EmployerTaxRate);
            int taxPerOwner = num / OwnersID.Count;

            int taxes = 0;
            foreach (var id in OwnersID)
            {
                if (EconomicManager.Instance.PushTax(OwnerID, taxPerOwner))
                {
                    taxes += taxPerOwner;
                }
            }

            MoneyLeft += MoneyPerMonth - taxes;
        }
        #endregion
    }
}