using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class BuildingsConfigLoader {
        public Dictionary<int, BuildingsConfig> Datas = new Dictionary<int, BuildingsConfig> ();

        public Dictionary<int, BuildingsConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 10) {
                    BuildingsConfig data = new BuildingsConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    float.TryParse(split[2], out data.EffectRange);
                    int.TryParse(split[3], out data.BuildCost);
                    int.TryParse(split[4], out data.BuyCost);
                    int.TryParse(split[5], out data.DestroyCost);
                    int.TryParse(split[6], out data.OutputPerMonth);
                    int.TryParse(split[7], out data.Stamina);
                    float.TryParse(split[8], out data.AgeingRate);
                    int.TryParse(split[9], out data.RenovationCost);
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public BuildingsConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
