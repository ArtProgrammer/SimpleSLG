using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.PoolSystem;
using SimpleAI.Inputs;
using SimpleAI.Utils;

/// <summary>
/// One tent has 6 soldiers.
/// </summary>
namespace GameContent
{
    public class Tent // : MonoBehaviour
    {
        public int ID = 0;

        public int GangID = 0;

        public int CfgID = 0;

        public GangColor TheColor;

        private List<BaseSoldier> Soldiers = new List<BaseSoldier>();

        [SerializeField]
        private int CurCount = 0;

        public int MaxCount = 10;

        public bool IsPlayerCtrl = false;

        public Vector3 Location = Vector3.zero;

        public void OnSoldierDie(int id)
        {
            RemoveSoldier(id);
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //    Initialize();
        //}

        public void Initialize()
        {
            //Location = transform.position;

            ID = IDAllocator.Instance.GetID();

            LoadSoldiers();

            //if (IsPlayerCtrl)
            //    InputKeeper.Instance.OnRightClickPos += ForceMove;
        }

        public void LoadSoldiers()
        {
            string soldierPath = "Prefabs/Roles/Soldier";

            if (TheColor == GangColor.Blue)
            {
                soldierPath = "Prefabs/Roles/Soldier2";
            }
            var soldierPrefab = Resources.Load(soldierPath) as GameObject;

            for (int i = 0; i < MaxCount; i++)
            {
                var soldier = PrefabPoolingSystem.Instance.Spawn(
                    soldierPrefab, Location, Quaternion.identity);
                var com = soldier.GetComponentInChildren<BaseSoldier>();

                if (com != null)
                {
                    com.GangID = GangID;

                    com.CfgID = CfgID;

                    com.LoadCfgData(CfgID);

                    AddSoldier(com);
                    com.OnSoldierDie += OnSoldierDie;
                }
            }
        }

        public void AddSoldier(BaseSoldier soldier)
        {
            if (CurCount < MaxCount)
            {
                Soldiers.Add(soldier);
                CurCount++;
            }
        }

        public void RemoveSoldier(int id)
        {
            if (CurCount > 0)
            {
                for (int i = 0; i < Soldiers.Count; i++)
                {
                    if (Soldiers[i].ID == id)
                    {
                        Soldiers.Remove(Soldiers[i]);
                        CurCount--;
                        break;
                    }
                }
            }
        }

        public void ForceMove(Vector3 pos)
        {
            for (int i = 0; i < Soldiers.Count; i++)
            {
                Soldiers[i].ForceMove = 1.0f;
            }

            MoveTo(pos);
        }

        /// <summary>
        /// send message to soldiers belong to this tent.
        /// </summary>
        /// <param name="pos"></param>
        public void MoveTo(Vector3 pos)
        {
            for (int i = 0; i < Soldiers.Count; i++)
            {
                Soldiers[i].MoveTo(pos);
            }
        }
    }
}