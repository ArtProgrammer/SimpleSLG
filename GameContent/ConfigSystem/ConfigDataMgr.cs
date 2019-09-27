using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Config;

namespace GameContent
{
    class ConfigDataMgr
    {
        private static ConfigDataMgr TheInstance = new ConfigDataMgr();

        public static ConfigDataMgr Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public IconsConfigLoader IconCfgLoader = new IconsConfigLoader();

        public PrefabsConfigLoader PrefabCfgLoader = new PrefabsConfigLoader();

        public CharacterConfigLoader CharacterCfgLoader = new CharacterConfigLoader();

        public BuildingsConfigLoader BuildingCfgLoader = new BuildingsConfigLoader();

        //public EffectsConfigLoader EffectCfgLoader = new EffectsConfigLoader();

        //public ItemConfigLoader ItemCfgLoader = new ItemConfigLoader();

        //public BuffConfigLoader BuffCfgLoader = new BuffConfigLoader();

        //public SkillConfigLoader SkillCfgLoader = new SkillConfigLoader();

        public void Initialize()
        {
            LoadConfig();
            LoadAssets();
        }

        public void LoadConfig()
        {
            TextAsset ta = Resources.Load("TextAssets/IconsConfig") as TextAsset;
            IconCfgLoader.LoadConfigData(ta.text);

            TextAsset prefabTa = Resources.Load("TextAssets/PrefabsConfig") as TextAsset;
            PrefabCfgLoader.LoadConfigData(prefabTa.text);

            TextAsset characterCfg = Resources.Load("TextAssets/CharacterConfig") as TextAsset;
            CharacterCfgLoader.LoadConfigData(characterCfg.text);

            TextAsset buildsCfg = Resources.Load("TextAssets/BuildingsConfig") as TextAsset;
            BuildingCfgLoader.LoadConfigData(buildsCfg.text);

            //TextAsset effectTa = Resources.Load("TextAssets/EffectsConfig") as TextAsset;
            //EffectCfgLoader.LoadConfigData(effectTa.text);

            //TextAsset itemTa = Resources.Load("TextAssets/ItemConfig") as TextAsset;
            //ItemCfgLoader.LoadConfigData(itemTa.text);

            //TextAsset buffTa = Resources.Load("TextAssets/BuffConfig") as TextAsset;
            //BuffCfgLoader.LoadConfigData(buffTa.text);

            //TextAsset skillTa = Resources.Load("TextAssets/SkillConfig") as TextAsset;
            //SkillCfgLoader.LoadConfigData(skillTa.text);
        }

        public void LoadAssets()
        {
            foreach (var item in IconCfgLoader.Datas)
            {
                IconsAssetHolder.Instance.AddIcon(item.Key, item.Value.Path);
            }

            foreach (var item in PrefabCfgLoader.Datas)
            {
				PrefabsAssetHolder.Instance.AddPrefab(item.Key, item.Value.Path);
			}
        }

        public void Clear()
        {

        }
    }
}
