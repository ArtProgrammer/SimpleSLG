using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.Utils
{
    public enum MultitonType
    { 
        IDAlloc,
        SpatialID,

    }

    public class Multiton<T> where T : Multiton<T>
    {
        private static readonly Dictionary<MultitonType, Multiton<T>> Instances =
            new Dictionary<MultitonType, Multiton<T>>();

        private Multiton()
        { 

        }

        public static Multiton<T> GetInstance(MultitonType mtype)
        { 
            if (!Instances.ContainsKey(mtype))
            {
                Instances.Add(mtype, new Multiton<T>());
            }

            return Instances[mtype];
        }
    }
}