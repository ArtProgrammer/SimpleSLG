using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class BaseSoldier : BaseRole
    {
        public int GangID = 0;

        public void ChangeGang(int id)
        {
            GangID = id;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public override void Process(float dt)
        {

        }

        public override void FixedProcess(float dt)
        {

        }
    }
}