using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Config;

namespace GameContent
{
    public class PrefabsAssetHolder
    {
        private static PrefabsAssetHolder TheInstance = new PrefabsAssetHolder();

        public static PrefabsAssetHolder Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private Dictionary<int, GameObject> Prefabs = new Dictionary<int, GameObject>();

        public GameObject GetPrefabByID(int id)
        {
            if (Prefabs.ContainsKey(id))
            {
                return Prefabs[id];
            }

            return null;
        }

        public void AddPrefab(int id, string path)
        {
            if (!Prefabs.ContainsKey(id))
            {
                GameObject go = Resources.Load<GameObject>(path);
                if (go)
                {
                    Prefabs.Add(id, go);
                }
            }
        }

        public void ClearByID(int id)
        {
            if (Prefabs.ContainsKey(id))
            {
                Prefabs.Remove(id);
            }
        }

        public void ClearAll()
        {
            foreach (var item in Prefabs)
            {
                Resources.UnloadAsset(item.Value);
                ClearByID(item.Key);
            }
        }
    }
}
