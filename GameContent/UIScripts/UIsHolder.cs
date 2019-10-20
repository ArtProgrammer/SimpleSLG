using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class UIsHolder
    {
        private static UIsHolder TheInstance = new UIsHolder();

        public static UIsHolder Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public TentActionPanel TentActionPanel;

        public NormalDataUI GangNormalDataUI;

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