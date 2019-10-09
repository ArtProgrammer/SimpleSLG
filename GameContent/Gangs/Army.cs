using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Inputs;

namespace GameContent
{
    public class Army // : MonoBehaviour
    {
        public int ID = 0;

        public int GangID = 0;

        // test data.
        public List<int> SoldierCfgIDs = new List<int>();

        public List<int> TentsVolumes = new List<int>();

        public List<Tent> Tents = new List<Tent>();

        public Tent CurTent;

        public Vector3 Location = Vector3.zero;

        public GangColor TheColor;

        public List<Tent> GetTents()
        {
            return Tents;
        }

        public Tent GetTent(int id)
        {
            for (int i = 0; i < Tents.Count; i++)
            {
                if (Tents[i].ID == id)
                    return Tents[i];
            }

            return null;
        }

        public void AddTent(Tent tent)
        {
            if (GetTent(tent.ID) == null)
            {
                tent.GangID = GangID;
                tent.TheColor = TheColor;
                tent.Location = Location;
                Tents.Add(tent);
            }
        }

        public void RemoveTent(int id)
        {
            var tent = GetTent(id);

            if (tent != null)
            {
                Tents.Remove(tent);
                tent = null;
            }
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //    Initialize();
        //}

        public void Initialize()
        {
            InputKeeper.Instance.OnRightClickPos += ForceMove;

            //Tent tent1 = new Tent();
            //tent1.GangID = GangID;
            //tent1.Location = Location;
            //tent1.Initialize();
            //AddTent(tent1);

            //Tent tent2 = new Tent();
            //tent2.GangID = GangID;
            //tent2.Location = Location;
            //tent2.Initialize();
            //AddTent(tent2);

            for (int i = 0; i < SoldierCfgIDs.Count; i++)
            {
                Tent tent1 = new Tent();
                tent1.GangID = GangID;
                tent1.TheColor = TheColor;
                tent1.CfgID = SoldierCfgIDs[i];
                tent1.MaxCount = TentsVolumes[i];
                tent1.Location = Location;
                tent1.Initialize();
                AddTent(tent1);
            }
        }

        public void ForceMove(Vector3 pos)
        {
            if (CurTent != null)
            {
                CurTent.ForceMove(pos);
            }
        }
    }
}