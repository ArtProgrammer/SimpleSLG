using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.PoolSystem
{
    public struct PoolablePrefabData
    {
        public GameObject Go;
        public IPoolableComponent[] PoolableComponents;
    }

    public class PrefabPool
    {
        Dictionary<GameObject, PoolablePrefabData> ActiveList =
            new Dictionary<GameObject, PoolablePrefabData>();

        Queue<PoolablePrefabData> InActiveList =
            new Queue<PoolablePrefabData>();

        public GameObject Spawn(GameObject prefab,
            Vector3 position,
            Quaternion rotation)
        {
            PoolablePrefabData data;
            if (InActiveList.Count > 0)
            {
                data = InActiveList.Dequeue();
            }
            else
            {
                GameObject go = GameObject.Instantiate(prefab,
                    position, rotation) as GameObject;
                data = new PoolablePrefabData();
                data.Go = go;
                data.PoolableComponents = go.GetComponents<IPoolableComponent>();
            }

            data.Go.SetActive(true);
            data.Go.transform.position = position;
            data.Go.transform.rotation = rotation;
            for (int i = 0; i < data.PoolableComponents.Length; ++i)
            {
                data.PoolableComponents[i].Spawned();
            }

            ActiveList.Add(data.Go, data);

            return data.Go;
        }

        public bool Despawn(GameObject obj)
        {
            if (!ActiveList.ContainsKey(obj))
            {
                Debug.LogError("This object is not mananged by this object pool!");
                return false;
            }

            PoolablePrefabData data = ActiveList[obj];
            for (int i = 0; i < data.PoolableComponents.Length; ++i)
            {
                data.PoolableComponents[i].Despawned();
            }

            data.Go.SetActive(false);
            ActiveList.Remove(obj);
            InActiveList.Enqueue(data);

            return true;
        }
    }
}