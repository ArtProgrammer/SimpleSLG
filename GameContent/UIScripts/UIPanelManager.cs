using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Inputs;

namespace GameContent
{
    public class UIPanelManager : MonoBehaviour
    {
        public BuildingInfo CurSelectBuildingInfo;

        public void OnTryClickBuidling(Transform trans)
        {
            if (trans != null)
            {
                BaseBenefit bb = trans.GetComponent<BaseBenefit>();
                if (bb != null)
                {
                    CurSelectBuildingInfo.Show(bb);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            InputKeeper.Instance.OnLeftClickObject += OnTryClickBuidling;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}