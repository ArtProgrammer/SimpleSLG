using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent
{
    public class TentActionPanel : MonoBehaviour
    {
        public Tent CurTent;

        public GameObject RootPanel;

        // Start is called before the first frame update
        void Start()
        {
            RootPanel = gameObject;

            Close();

            UIsHolder.Instance.TentActionPanel = this;
        }

        public void Show()
        {
            //CurArmy.CurTent;
            if (RootPanel)
            {
                RootPanel.SetActive(true);
            }
        }

        public void Close()
        {
            if (RootPanel)
            {
                RootPanel.SetActive(false);
            }
        }

        public void TryOccupy()
        {
            GangManager.Instance.CurGang.TheArmy.CurTent.TryOccupy();
        }
    }
}