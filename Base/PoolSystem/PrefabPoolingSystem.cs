using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;

namespace SimpleAI.PoolSystem
{
    public class PrefabPoolingSystem : SingletonAsComponent<PrefabPoolingSystem>
    {
        public static PrefabPoolingSystem Instance
        {
            get { return (PrefabPoolingSystem)InsideInstance; }
        }

        Dictionary<GameObject, PrefabPool> Prefab2PoolMap =
            new Dictionary<GameObject, PrefabPool>();

        Dictionary<GameObject, PrefabPool> Go2PoolMap =
            new Dictionary<GameObject, PrefabPool>();

        public GameObject Spawn(GameObject prefab,
            Vector3 pos, Quaternion rot)
        {
            if (!Prefab2PoolMap.ContainsKey(prefab))
            {
                Prefab2PoolMap.Add(prefab, new PrefabPool());
            }

            PrefabPool pool = Prefab2PoolMap[prefab];
            GameObject go = pool.Spawn(prefab, pos, rot);
            Go2PoolMap.Add(go, pool);

            return go;
        }

        public GameObject Spawn(GameObject prefab)
        {
            return Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        public bool Despawn(GameObject obj)
        {
            if (!Go2PoolMap.ContainsKey(obj))
            {
                Debug.LogError(string.Format("Object {0} not mananged " +
                    "by pool system!", obj.name));
                return false;
            }

            PrefabPool pool = Go2PoolMap[obj];
            if (pool.Despawn(obj))
            {
                Go2PoolMap.Remove(obj);
                return true;
            }

            return false;
        }

        public void Prespawn(GameObject prefab, int num)
        {
            List<GameObject> objects = new List<GameObject>();
            for (int i = 0; i < num; ++i)
            {
                objects.Add(Spawn(prefab));
            }

            for (int i = 0; i < num; ++i)
            {
                Despawn(objects[i]);
            }

            objects.Clear();
        }
    }
}