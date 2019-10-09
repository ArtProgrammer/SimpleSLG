using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace GameContent
{
    public class TentsInfo : MonoBehaviour
    {
        Army CurArmy;

        public RectTransform TentsPanel;

        public List<Image> Images = new List<Image>();

        //List<Army> CurArmies = new List<Army>();

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            LoadTents();
        }

        public void Initialize()
        {
            if (TentsPanel)
            {
                for (int i = 0; i < TentsPanel.childCount; i++)
                {
                    Transform sub = TentsPanel.GetChild(i);
                    Image btn = sub.GetComponent<Image>();
                    Images.Add(btn);
                }
            }
        }

        public void LoadTents()
        {
            CurArmy = GangManager.Instance.CurGang.TheArmy;

            if (CurArmy != null)
            {
                for (int i = 0; i < CurArmy.Tents.Count; i++)
                {
                    var cfgData = ConfigDataMgr.Instance.CharacterCfgLoader.GetDataByID(
                        CurArmy.Tents[i].CfgID);

                    if (cfgData != null)
                    {
                        var icondata = ConfigDataMgr.Instance.IconCfgLoader.GetDataByID(cfgData.IconID);

                        if (icondata != null)
                        {
                            //icondata.Path
                            Texture2D original = Resources.Load(icondata.Path) as Texture2D;
                            //var sprite = original as Sprite;
                            Images[i].sprite = Sprite.Create(original, new Rect(0, 0,
                                    original.width, original.height),
                                    new Vector2(0, 0),
                                    32);
                        }
                    }
                }
            }
        }

        public void SelectTent(int index)
        {
            if (CurArmy != null && CurArmy.Tents.Count > index)
            {
                Tent tent = CurArmy.Tents[index];

                CurArmy.CurTent = tent;

                //CurArmies.Clear();
                //CurArmies.Add(tent);
            }
        }
    }
}