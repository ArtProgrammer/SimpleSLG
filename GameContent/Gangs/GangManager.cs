using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class GangManager
    {
        private static GangManager TheInstance = new GangManager();

        public static GangManager Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public static bool IsAlive
        {
            get
            {
                return TheInstance != null;
            }
        }

        private Dictionary<int, Gang> GangInfos = new Dictionary<int, Gang>();

        public void AddGang(Gang gang)
        {
            if (!GangInfos.ContainsKey(gang.ID))
            {
                GangInfos.Add(gang.ID, gang);
            }
        }

        public void RemoveGang(int id)
        {
            if (GangInfos.ContainsKey(id))
            {
                GangInfos.Remove(id);
            }
        }

        public Gang GetGang(int id)
        {
            if (GangInfos.ContainsKey(id))
            {
                return GangInfos[id];
            }

            return null;
        }

        public bool IsEnemyGang(int srcID, int dstID)
        {
            Gang gang = GetGang(srcID);

            if (gang != null)
            {
                return gang.IsEnemyGang(dstID);
            }

            return false;
        }

        public bool IsAlienGang(int srcID, int dstID)
        {
            Gang gang = GetGang(srcID);

            if (gang != null)
            {
                return gang.IsAlienGang(dstID);
            }

            return false;
        }
    }
}