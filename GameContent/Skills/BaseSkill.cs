using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class BaseSkill : IUpdateable
    {
        public int ID;

        public bool IsActive;

        void Start()
        {
            Initialize();
        }

        public virtual void Initialize()
        {

        }

        public virtual void Process(float dt)
        {

        }

        public virtual void Use()
        {

        }

        public void OnUpdate(float dt)
        {
            if (IsActive)
            {
                Process(dt);
            }
        }

        public void OnFixedUpdate(float dt)
        {

        }
    }
}