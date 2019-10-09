using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Utils;

namespace GameContent
{
    public enum GangColor
    {
        Blue,
        Red
    }

    public class Gang : IUpdateable //MonoBehaviour, 
    {
        public int ID;

        public GangColor TheColor;

        public int EconomicID;

        public EconomicSystem Economic;

        public List<int> AlienGangIDs = new List<int>();

        public List<int> EnemyGangIDs = new List<int>();

        public Army TheArmy = null;

        public Vector3 Location = Vector3.zero;

        // test data
        public List<int> SoldierCfgIDs = new List<int>();

        public List<int> TentsVolumes = new List<int>();

        public void AddSoldierInfo(int cfgid, int volume)
        {
            SoldierCfgIDs.Add(cfgid);
            TentsVolumes.Add(volume);
        }

        public void AddAlien(int id)
        {
            if (IsEnemyGang(id))
            {
                EnemyGangIDs.Remove(id);
            }

            if (!IsAlienGang(id))
            {
                AlienGangIDs.Add(id);
            }
        }

        public void AddEnemy(int id)
        {
            if (IsAlienGang(id))
            {
                AlienGangIDs.Remove(id);
            }

            if (!IsEnemyGang(id))
            {
                EnemyGangIDs.Add(id);
            }
        }

        public bool IsAlienGang(int id)
        {
            return AlienGangIDs.Contains(id);
        }
        
        public bool IsEnemyGang(int id)
        {
            return EnemyGangIDs.Contains(id);
        }

        public List<int> GetAlienGangs()
        {
            return AlienGangIDs;
        }

        public List<int> GetEnemyGangs()
        {
            return EnemyGangIDs;
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //    Initialize();
        //}

        public void Initialize()
        {
            GangManager.Instance.AddGang(this);

            ID = IDAllocator.Instance.GetID();

            TheArmy = new Army();
            TheArmy.TheColor = TheColor;
            TheArmy.SoldierCfgIDs = SoldierCfgIDs;
            TheArmy.TentsVolumes = TentsVolumes;
            TheArmy.GangID = ID;
            TheArmy.Location = Location;
            TheArmy.Initialize();
        }

        private void OnDestroy()
        {
            if (GangManager.IsAlive)
                GangManager.Instance.RemoveGang(ID);
        }

        public void OnUpdate(float dt)
        {

        }

        public void OnFixedUpdate(float dt)
        {

        }
    }
}