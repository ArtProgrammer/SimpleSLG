using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameContent
{
    public class ShortCut : MonoBehaviour
    {
        public bool IsTroopsBtnsVisible = false;

        public RectTransform TroopsBtns;

        public bool IsTentsInfoVisible = false;

        public RectTransform TentInfoPanel;

        private void Start()
        {
            
        }

        private void Initialized()
        {
            IsTroopsBtnsVisible = true;
            IsTentsInfoVisible = true;
            SwitchTentInfoPanel();
        }

        public void SwitchTroopsBtns()
        {
            if (TroopsBtns)
            {
                TroopsBtns.localScale = IsTroopsBtnsVisible ? Vector3.zero : Vector3.one;
                IsTroopsBtnsVisible = !IsTroopsBtnsVisible;
                
            }
        }

        public void SwitchTentInfoPanel()
        {
            if (TentInfoPanel)
            {
                TentInfoPanel.localScale = IsTentsInfoVisible ? Vector3.zero : Vector3.one;
                IsTentsInfoVisible = !IsTentsInfoVisible;

            }
        }
    }
}