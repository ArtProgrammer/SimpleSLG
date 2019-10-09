using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class InventoryItem
    {
        public int CfgID;
        public int Count;
        public int MaxCount;
    }

    /// <summary>
    /// Contain weapons, ammors, bullets, etc.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        public Dictionary<int, InventoryItem> Items =
            new Dictionary<int, InventoryItem>();

        public bool AddItem(int cfgID, int count = 1)
        {
            return true;
        }

        public void RemoveItem(int cfgID, int count = 1)
        {

        }

        public void Initialize()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }
    }
}