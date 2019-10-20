using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.PoolSystem;
using SimpleAI.Inputs;
using SimpleAI.Utils;
using SimpleAI.Spatial;

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

        public BaseSoldier Commander;

        [SerializeField]
        private int CurCount = 0;

        public int MaxCount = 10;

        public bool IsPlayerCtrl = false;

        public Vector3 Location = Vector3.zero;

        public float SharingRange = 10.0f;

        private Bounds SearchBound;

        public List<int> SharingBenefitIDs = new List<int>();

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
                soldier.TentID = ID;
                soldier.OnSoldierDie += OnSoldierDie;
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
                        Soldiers[i].TentID = 0;
                        CurCount--;
                        break;
                    }
                }
            }
        }

        private List<Vector3> SoldiersPos = new List<Vector3>();

        /// <summary>
        /// just one line for now.
        /// </summary>
        /// <param name="pos"></param>
        public void CalSoldierPositions(Vector3 pos)
        {
            SoldiersPos.Clear();

            int countPerRow = 6;
            for (int i = 0; i < Soldiers.Count; i++)
            {
                int j = i / countPerRow;
                int m = i % countPerRow;

                SoldiersPos.Add(new Vector3(pos.x + m * 1.50f, pos.y, pos.z + j * 1.50f));
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
            CalSoldierPositions(pos);

            for (int i = 0; i < Soldiers.Count; i++)
            {
                Soldiers[i].MoveTo(SoldiersPos[i]);
            }
        }

        public void OnSelect()
        {
            for (int i = 0; i < Soldiers.Count; i++)
            {
                Soldiers[i].OnSelect();
            }
        }

        public void OnLoseSelect()
        {
            for (int i = 0; i < Soldiers.Count; i++)
            {
                Soldiers[i].OnLoseSelect();
            }
        }

        public void TryOccupy()
        {
            SearchBound.center = Location;
            SearchBound.size = Vector3.one * SharingRange;
            //SharingRange;
            List<SpatialFruitNode> nodes = new List<SpatialFruitNode>();
            SpatialManager.Instance.QueryRange(ref SearchBound, nodes);

            foreach (var node in nodes)
            {
                bool result = node is BaseBenefit;
                if (result)
                {
                    var benefit = (BaseBenefit)node;
                    AddBenefit(benefit);
                }
            }
        }

        public void AddBenefit(BaseBenefit benefit)
        {
            if (!SharingBenefitIDs.Contains(benefit.ID))
            {
                SharingBenefitIDs.Add(benefit.ID);
                benefit.AddOwner(ID);
            }
        }

        public void AddBenefitByID(int id)
        {
            if (!SharingBenefitIDs.Contains(id))
            {
                SharingBenefitIDs.Add(id);
            }
        }

        public void RemoveBenefitByID(int id)
        {
            SharingBenefitIDs.Remove(id);
        }
    }
}