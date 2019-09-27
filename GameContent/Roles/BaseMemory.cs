using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class BaseMemory : MonoBehaviour
    {
        private Dictionary<string, Transform> Locations = new Dictionary<string, Transform>();

        public void AddLocation(string name, Transform trans)
        {
            if (!Locations.ContainsKey(name))
            {
                Locations.Add(name, trans);
            }
        }

        public Transform GetLocation(string name)
        {
            if (Locations.ContainsKey(name))
            {
                return Locations[name];
            }

            return null;
        }

        #region PRIVATE_FUNCTIONS
        
        #endregion

        #region PUBLIC_FUNCTIONS
        public BaseMemory()
        {

        }

        public virtual void PreInitialize()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void Process(float dt)
        {

        }

        public virtual void FixedProcess(float dt)
        {

        }
        #endregion
    }
}