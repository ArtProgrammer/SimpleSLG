using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;

using SimpleAI.Utils;
using SimpleAI.Spatial;
using SimpleAI.PoolSystem;
//using GameContent;
//using GameContent.Skill;
//using GameContent.Item;
//using GameContent.SimAgent;

//using Config;
using GameContent;

namespace SimpleAI.Supervisors
{
    public class GlorySupervisor : SingletonAsComponent<GlorySupervisor>
    {
        public static GlorySupervisor Instance
        {
            get
            {
                return (GlorySupervisor)InsideInstance;
            }
        }

        public Action SuperInitialize;

        public void Initialize()
        {
            Reload();
            DontDestroyOnLoad(gameObject);
            InitThirdPart();
        }

        public void Reload()
        {
            //SKillMananger.Instance.LoadSkills();

            //ItemManager.Instance.LoadDatas();

            //BulletCfgMgr.Instance.LoadCfgs();

            //WeaponConfigMgr.Instance.LoadConfigs();

            //FruitSpawner.Instance.Intialize();
            //FruitSpawner.Instance.RandSpawnApple();
            //FruitSpawner.Instance.RandSpawnApple();
            //FruitSpawner.Instance.RandSpawnApple();

            // load from config
            SpatialManager.Instance.Init(0, 0, 0, 100, 100, 100);

            ConfigDataMgr.Instance.Initialize();

            GangManager.Instance.Initialize();

            //UILord.Instance.Init();

            if (!System.Object.ReferenceEquals(null, SuperInitialize))
            {
                SuperInitialize();
            }
        }

        protected void InitThirdPart()
        {
            //DOTween.Init(true, true, null);
        }

        [SerializeField]
        public Transform RoleContenter = null;

        public Transform NPC_Home = null;

        public Transform NPC_Food = null;

        void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            GangManager.Instance.CurGang.Economic.SetSelected();
        }

        public void SpawnNpc(int cfgid)
        {
            SpawnNpc(cfgid, Vector3.zero);
        }

        public void SpawnNpc(int cfgid, Vector3 pos)
        {
            SpawnNpc(cfgid, pos, Quaternion.identity);
        }

        public void SpawnNpc(int cfgid, Vector3 pos, Quaternion rot)
        {
            //PrefabsConfig config =
            //    ConfigDataMgr.Instance.PrefabCfgLoader.GetDataByID(cfgid);

            //if (!System.Object.ReferenceEquals(null, config))
            //{
            //    GameObject go = PrefabsAssetHolder.Instance.GetPrefabByID(config.ID);
            //    if (go)
            //    {
            //        GameObject npcInstance =
            //            PrefabPoolingSystem.Instance.Spawn(go, pos, rot);

            //        npcInstance.transform.SetParent(RoleContenter);

            //        SimWood sw = npcInstance.GetComponent<SimWood>();

            //        if (sw)
            //        {
            //            sw.Home = NPC_Home;
            //            sw.Food = NPC_Food;
            //        }
            //    }
            //}
        }

        public void SpawnItem(int cfgid)
        {
            SpawnItem(cfgid, Vector3.zero);
        }

        public void SpawnItem(int cfgid, Vector3 pos)
        {
            SpawnItem(cfgid, pos, Quaternion.identity);
        }

        public void SpawnItem(int cfgid, Vector3 pos, Quaternion rot)
        {
            //ItemConfig itemCfg = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(cfgid);

            //if (!System.Object.ReferenceEquals(null, itemCfg))
            //{
            //    GameObject go = PrefabsAssetHolder.Instance.GetPrefabByID(itemCfg.PrefabID);

            //    if (!System.Object.ReferenceEquals(null, go))
            //    {
            //        GameObject inst =
            //            PrefabPoolingSystem.Instance.Spawn(go,
            //                pos, rot);

            //        inst.transform.SetParent(RoleContenter);

            //        ItemGiver ig = inst.GetComponent<ItemGiver>();
            //        if (!System.Object.ReferenceEquals(null, ig))
            //        {
            //            ig.ItemCfgID = cfgid;
            //        }
            //    }
            //}
        }
    }
}