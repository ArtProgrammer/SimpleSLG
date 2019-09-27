using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class BenefitMap : MonoBehaviour
    {
        private Dictionary<int, BaseBenefit> Benefis =
             new Dictionary<int, BaseBenefit>();

        public void AddBenefit(BaseBenefit be)
        {
            if (!Benefis.ContainsKey(be.ID))
            {
                Benefis.Add(be.ID, be);
            }
        }

        public void RemoveBenefit(int id)
        {
            if (Benefis.ContainsKey(id))
            {
                Benefis.Remove(id);
            }
        }

        public BaseBenefit GetBenefit(int id)
        {
            if (Benefis.ContainsKey(id))
            {
                return Benefis[id];
            }

            return null;
        }

        public void Clear()
        {
            Benefis.Clear();
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