using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;

namespace GameContent
{
    public class EconomicManager : SingletonAsComponent<EconomicManager>
    {
        public static EconomicManager Instance
        {
            get
            {
                return (EconomicManager)InsideInstance;
            }
        }

        private Dictionary<int, EconomicSystem> Economics =
            new Dictionary<int, EconomicSystem>();

        private EconomicSystem CurEconSys;

        public System.Action<EconomicSystem> OnEconomicSystemChange;

        public EconomicSystem CurEconomicSysSelect
        {
            set
            {
                if (CurEconSys != value)
                {
                    //CurEconSys.OnMoneyChange = null;
                    CurEconSys = value;

                    if (OnEconomicSystemChange != null)
                    {
                        OnEconomicSystemChange(CurEconSys);
                    }
                }
            }
            get
            {
                return CurEconSys;
            }
        }

        public void AddEconomic(int id, EconomicSystem eco)
        {
            if (!Economics.ContainsKey(id))
            {
                Economics.Add(id, eco);
            }
        }

        public EconomicSystem GetEconomic(int id)
        {
            if (Economics.ContainsKey(id))
            {
                return Economics[id];
            }

            return null;
        }

        public bool PushTax(int id, int num)
        {
            EconomicSystem es = GetEconomic(id);

            if (es != null)
            {
                return es.PushTax(num);
            }

            return false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}