using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent
{
    public class GameDateTime : IUpdateable
    {
        private static GameDateTime TheInstance = new GameDateTime();

        public static GameDateTime Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public bool IsActive { set; get; }

        public float SecondPerDay = 1;

        // Start is called before the first frame update
        void Start()
        {
            IsActive = true;
        }

        public void Pause()
        {
            IsActive = false;
        }

        public void Resume()
        {
            IsActive = true;
        }

        public virtual void OnUpdate(float dt)
        {
            if (IsActive)
            {
            }
        }

        public virtual void OnFixedUpdate(float dt)
        {
            if (IsActive)
            {
            }
        }
    }
}