using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class Gang : MonoBehaviour, IUpdateable
    {
        public int ID;

        public int EconomicID;

        public EconomicSystem Economic;

        public List<int> AlienGangIDs = new List<int>();

        public List<int> EnemyGangIDs = new List<int>();

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
        void Start()
        {
            GangManager.Instance.AddGang(this);
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