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

        public Gang CurGang = null;

        public void Initialize()
        {
            Gang gang = new Gang();
            gang.TheColor = GangColor.Blue;
            gang.Location = new Vector3(-5, 0, -5);

            gang.AddSoldierInfo(100011, 15);
            gang.AddSoldierInfo(100012, 15);
            gang.AddSoldierInfo(100012, 30);
            gang.AddSoldierInfo(100012, 30);
            gang.Initialize();

            AddGang(gang);

            CurGang = gang;

            Gang testGang = new Gang();
            testGang.TheColor = GangColor.Red;
            testGang.Location = new Vector3(30, 0, 30);
            //testGang.SoldierCfgIDs.Add(100010);
            //testGang.SoldierCfgIDs.Add(100012);
            //testGang.SoldierCfgIDs.Add(100012);

            //testGang.SoldierCfgIDs.Add(100010);
            //testGang.SoldierCfgIDs.Add(100010);
            //testGang.SoldierCfgIDs.Add(100010);
            testGang.AddSoldierInfo(100010, 10);
            testGang.AddSoldierInfo(100010, 25);
            testGang.AddSoldierInfo(100010, 25);
            testGang.Initialize();
            
            AddGang(testGang);
        }

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