using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent
{
    public class NormalDataUI : MonoBehaviour
    {
        public Text MoneyTxt;

        public Text TimeTxt;

        private void Awake()
        {
            EconomicManager.Instance.OnEconomicSystemChange += SwitchCurEconomicSys;
        }

        // Start is called before the first frame update
        void Start()
        {
            //EconomicManager.Instance.CurEconomicSysSelect.OnMoneyChange += OnMoneyChange;
            
            //UIsHolder.Instance.GangNormalDataUI = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchCurEconomicSys(EconomicSystem econo)
        {
            if (econo == null) return;

            //if (EconomicManager.Instance.CurEconomicSysSelect != econo)
            {
                EconomicManager.Instance.CurEconomicSysSelect = econo;
                OnMoneyChange(EconomicManager.Instance.CurEconomicSysSelect.TotalMoney);
                EconomicManager.Instance.CurEconomicSysSelect.OnMoneyChange += OnMoneyChange;
            }
        }

        public void OnMoneyChange(int num)
        {
            MoneyTxt.text = num.ToString();
        }
    }
}