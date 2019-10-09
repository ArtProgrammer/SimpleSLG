using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class SoldierConfigLoader {
        public Dictionary<int, SoldierConfig> Datas = new Dictionary<int, SoldierConfig> ();

        public Dictionary<int, SoldierConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 12) {
                    SoldierConfig data = new SoldierConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    int.TryParse(split[2], out data.HP);
                    int.TryParse(split[3], out data.AlertRange);
                    int.TryParse(split[4], out data.AttackRange);
                    int.TryParse(split[5], out data.Salary);
                    int.TryParse(split[6], out data.Attack);
                    int.TryParse(split[7], out data.Defence);
                    float.TryParse(split[8], out data.MoveSpeed);
                    float.TryParse(split[9], out data.AttackSpeed);
                    int.TryParse(split[10], out data.PrefabID);
                    int.TryParse(split[11], out data.IconID);
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public SoldierConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
