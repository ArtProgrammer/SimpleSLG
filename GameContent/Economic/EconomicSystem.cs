﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;

namespace GameContent
{
    public class EconomicSystem : MonoBehaviour
    {
        public int ID = 0;

        public int TotalMoney = 0;

        public List<int> BuildIDList = new List<int>();

        public int MonthMoney = 0;

        public Action<int> OnMoneyChange;

        public bool IsCurEconomicSys = false;

        private void Awake()
        {
            EconomicManager.Instance.AddEconomic(ID, this);

            if (IsCurEconomicSys)
            {
                EconomicManager.Instance.CurEconomicSysSelect = this;
            }
        }

        private void Start()
        {
            
        }

        public void Initialize()
        {

        }

        public bool PushTax(int num)
        {
            MonthMoney += num;

            if (OnMoneyChange != null)
            {
                OnMoneyChange(MonthMoney);
            }

            return true;
        }
    }
}