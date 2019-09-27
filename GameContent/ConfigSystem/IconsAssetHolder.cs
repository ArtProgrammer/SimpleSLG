using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Config;

namespace GameContent
{
    public class IconsAssetHolder
    {
        private static IconsAssetHolder TheInstance = new IconsAssetHolder();

        public static IconsAssetHolder Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private Dictionary<int, Sprite> Icons = new Dictionary<int, Sprite>();

        public Sprite GetIconByID(int id)
        {
            if (Icons.ContainsKey(id))
            {
                return Icons[id];
            }

            return null;
        }

        public void AddIcon(int id, string path)
        {
            if (!Icons.ContainsKey(id))
            {
                Sprite sp = Resources.Load<Sprite>(path);
                if (sp)
                    Icons.Add(id, sp);
            }            
        }

        public void ClearIconByID(int id)
        {
            if (Icons.ContainsKey(id))
            {
                // free the sprite asset.
                Icons.Remove(id);
            }
        }

        public void ClearIcons()
        {
            foreach (var item in Icons)
            {
				Resources.UnloadAsset(item.Value);

				ClearIconByID(item.Key);
            }

            Resources.UnloadUnusedAssets();
        }
    }
}
