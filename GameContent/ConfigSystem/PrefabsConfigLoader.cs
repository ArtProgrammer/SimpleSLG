using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class PrefabsConfigLoader {
        public Dictionary<int, PrefabsConfig> Datas = new Dictionary<int, PrefabsConfig> ();

        public Dictionary<int, PrefabsConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    PrefabsConfig data = new PrefabsConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Path= split[2];
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public PrefabsConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
