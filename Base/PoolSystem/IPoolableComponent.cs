using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.PoolSystem
{
    public interface IPoolableComponent
    {
        void Spawned();
        void Despawned();
    }
}