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

        private Dictionary<int, List<int>> GangsInWar =
            new Dictionary<int, List<int>>();

        private Dictionary<int, List<int>> GangsInEnemy =
            new Dictionary<int, List<int>>();

        private Dictionary<int, List<int>> GangsInAllien =
            new Dictionary<int, List<int>>();

        public void AddGangsInWar(int srcID, int dstID)
        {
            if (!GangsInWar.ContainsKey(srcID))
            {
                GangsInWar.Add(srcID, new List<int>());
            }

            if (!GangsInWar.ContainsKey(dstID))
            {
                GangsInWar.Add(dstID, new List<int>());
            }

            if (!IsGangsInWar(srcID, dstID))
            {
                GangsInWar[srcID].Add(dstID);
            }

            if (!IsGangsInWar(dstID, srcID))
            {
                GangsInWar[dstID].Add(srcID);
            }
        }

        public void RemoveGangsInWar(int srcID, int dstID)
        {
            if (IsGangsInWar(srcID, dstID))
            {
                GangsInWar[srcID].Remove(dstID);
            }

            if (IsGangsInWar(dstID, srcID))
            {
                GangsInWar[dstID].Remove(srcID);
            }
        }

        public void AddGangsEnemy(int srcID, int dstID)
        {
            if (!GangsInEnemy.ContainsKey(srcID))
            {
                GangsInEnemy.Add(srcID, new List<int>());
            }

            if (!GangsInEnemy.ContainsKey(dstID))
            {
                GangsInEnemy.Add(dstID, new List<int>());
            }

            if (!IsEnemyGangs(srcID, dstID))
            {
                GangsInEnemy[srcID].Add(dstID);
            }

            if (!IsEnemyGangs(dstID, srcID))
            {
                GangsInEnemy[dstID].Add(srcID);
            }
        }

        public void RemoveGangInEnemy(int srcID, int dstID)
        {
            if (IsEnemyGangs(srcID, dstID))
            {
                GangsInEnemy[srcID].Remove(dstID);
            }

            if (IsEnemyGangs(dstID, srcID))
            {
                GangsInEnemy[dstID].Remove(srcID);
            }
        }

        public void AddGangsAllien(int srcID, int dstID)
        {
            if (!GangsInAllien.ContainsKey(srcID))
            {
                GangsInAllien.Add(srcID, new List<int>());
            }

            if (!GangsInAllien.ContainsKey(dstID))
            {
                GangsInAllien.Add(dstID, new List<int>());
            }

            if (!IsAllienGangs(srcID, dstID))
            {
                GangsInAllien[srcID].Add(dstID);
            }

            if (!IsAllienGangs(dstID, srcID))
            {
                GangsInAllien[dstID].Add(srcID);
            }
        }

        public void RemoveGangInAllien(int srcID, int dstID)
        {
            if (IsGangsInWar(srcID, dstID))
            {
                GangsInAllien[srcID].Remove(dstID);
            }

            if (IsGangsInWar(dstID, srcID))
            {
                GangsInAllien[dstID].Remove(srcID);
            }
        }

        public bool IsGangsInWar(int srcID, int dstID)
        {
            if (GangsInWar.ContainsKey(srcID))
            {
                return GangsInWar[srcID].Contains(dstID);
            }

            return false;
        }

        public bool IsEnemyGangs(int srcID, int dstID)
        {
            if (GangsInEnemy.ContainsKey(srcID))
            {
                return GangsInEnemy[srcID].Contains(dstID);
            }

            return false;
        }

        public bool IsAllienGangs(int srcID, int dstID)
        {
            if (GangsInAllien.ContainsKey(srcID))
            {
                return GangsInAllien[srcID].Contains(dstID);
            }

            return false;
        }

        public void GetInWarGangs(int srcid, ref List<int> ids)
        {
            if (GangsInEnemy.ContainsKey(srcid))
            {
                ids = GangsInEnemy[srcid];
            }
        }

        public void GetEnemyGangs(int srcid, ref List<int> ids)
        {
            if (GangsInEnemy.ContainsKey(srcid))
            {
                ids = GangsInEnemy[srcid];
            }
        }

        public void GetAllienGangs(int srcid, ref List<int> ids)
        {
            if (GangsInAllien.ContainsKey(srcid))
            {
                ids = GangsInAllien[srcid];
            }
        }

        public void ClearGangsInWar(int srcid)
        {
            if (GangsInWar.ContainsKey(srcid))
            {
                GangsInWar[srcid].Clear();
            }
        }

        public void ClearEnemyGangs(int srcid)
        {
            if (GangsInEnemy.ContainsKey(srcid))
            {
                GangsInEnemy[srcid].Clear();
            }
        }

        public void ClearAllienGangs(int srcid)
        {
            if (GangsInAllien.ContainsKey(srcid))
            {
                GangsInAllien[srcid].Clear();
            }
        }

        public void Initialize()
        {
            Gang gang = new Gang();
            gang.TheColor = GangColor.Blue;
            gang.Location = new Vector3(-5, 0, -5);

            gang.AddSoldierInfo(100010, 20);
            gang.AddSoldierInfo(100010, 20);
            //gang.AddSoldierInfo(100012, 15);
            //gang.AddSoldierInfo(100012, 30);
            //gang.AddSoldierInfo(100012, 30);
            gang.Initialize();
            foreach (var tent in gang.TheArmy.Tents)
            {
                tent.MoveTo(gang.Location);
            }

            AddGang(gang);

            CurGang = gang;

            //CurGang.Economic.SetSelected();

            Gang testGang = new Gang();
            testGang.TheColor = GangColor.Red;
            testGang.Location = new Vector3(30, 0, 30);
            testGang.AddSoldierInfo(100010, 36);
            //testGang.AddSoldierInfo(100010 25);
            //testGang.AddSoldierInfo(100010, 25);
            testGang.Initialize();
            
            AddGang(testGang);

            foreach(var tent in testGang.TheArmy.Tents)
            {
                tent.MoveTo(testGang.Location);
            }

            AddGangsEnemy(gang.ID, testGang.ID);
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
    }
}