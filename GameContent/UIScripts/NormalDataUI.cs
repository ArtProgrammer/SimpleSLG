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

        // Start is called before the first frame update
        void Start()
        {
            EconomicManager.Instance.CurEconomicSysSelect.OnMoneyChange += OnMoneyChange;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMoneyChange(int num)
        {
            MoneyTxt.text = num.ToString();
        }
    }
}