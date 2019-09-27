using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Logger;

namespace SimpleAI.Utils
{
    public class MineResource
    {
        private static MineResource TheInstance = new MineResource();

        public static MineResource Instance
        {
            get
            {
                return TheInstance;
            }
        }

        private Dictionary<string, Sprite> Sprites =
            new Dictionary<string, Sprite>();

        private Dictionary<string, GameObject> GameObjects =
            new Dictionary<string, GameObject>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadAssetBundleDependencies(string path, string name)
        {

        }

        public Sprite LoadSpriteFromAB(string path, string name)
        {
            return null;
        }

        public T LoadObjectFromAB<T>(string path, string name) where T : Object
        {
            return null;
        }

        private Sprite LoadSprite(string path,
            float pixelsPerUnit = 100.0f)
        {
            //Sprite newsp = new Sprite();
            Texture2D sptx = LoadTex(path);
            if (!System.Object.ReferenceEquals(sptx, null))
            {
                Sprite newsp = Sprite.Create(sptx,
                    new Rect(0, 0,
                    sptx.width, sptx.height),
                    new Vector2(0, 0),
                    pixelsPerUnit);

                return newsp;
            }
            else
            {
                TinyLogger.Instance.DebugLog("$failed to load tex: " + path);
            }

            return null;
        }

        private Texture2D LoadTex(string path)
        {
            Texture2D tex;
            byte[] fileData;

            if (File.Exists(path))
            {
                //TinyLogger.Instance.ErrorLog("$file: " + path + " exits");
                fileData = File.ReadAllBytes(path);
                tex = new Texture2D(2, 2);

                if (fileData.Length == 0)
                {
                    TinyLogger.Instance.ErrorLog("$ load no bytes data");
                }

                if (tex.LoadImage(fileData))
                {
                    //TinyLogger.Instance.ErrorLog("$ yet load image");
                    return tex;
                }
                else
                {
                    TinyLogger.Instance.ErrorLog("$ failed to load image");
                }
            }
            else
            {
                TinyLogger.Instance.ErrorLog("$file: " + path + " not exits");
            }

            return null;
        }
    }
}