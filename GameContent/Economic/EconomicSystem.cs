using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;

namespace GameContent
{
    public class EconomicSystem
    {
        public int ID = 0;

        public int TotalMoney = 0;

        public List<int> BuildIDList = new List<int>();

        public int MonthMoney = 0;

        public Action<int> OnMoneyChange;

        public bool IsCurEconomicSys = false;

        public EconomicSystem()
        {
            Initialize();
        }

        private void Awake()
        {
            
        }

        public void SetSelected()
        {
            EconomicManager.Instance.CurEconomicSysSelect = this;
        }

        private void Start()
        {
            
        }

        public void Initialize()
        {
            EconomicManager.Instance.AddEconomic(ID, this);

            //if (IsCurEconomicSys)
            //{
            //    EconomicManager.Instance.CurEconomicSysSelect = this;
            //}
        }

        public bool PushTax(int num)
        {
            MonthMoney += num;

            TotalMoney += num;

            if (OnMoneyChange != null)
            {
                OnMoneyChange(MonthMoney);
            }

            return true;
        }
    }
}