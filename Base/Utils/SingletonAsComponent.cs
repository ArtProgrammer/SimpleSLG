using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SimpleAI.Logger;

namespace SimpleAI.Utils
{
    public class SingletonAsComponent<T> : MonoBehaviour where 
        T : SingletonAsComponent<T>
    {
        private static T TheInstance;

        [SerializeField]
        private bool Alive = true;

        public static bool IsAlive
        {
            get
            {
                if (System.Object.ReferenceEquals(TheInstance, null))
                    return false;

                return TheInstance.Alive;
            }
        }

        protected static SingletonAsComponent<T> InsideInstance
        {
            get
            {
                if (!TheInstance)
                {
                    T[] managers =
                        GameObject.FindObjectsOfType(typeof(T)) as T[];
                    if (managers != null)
                    {
                        if (managers.Length == 1)
                        {
                            TheInstance = managers[0];
                            return TheInstance;
                        }
                        else if (managers.Length > 1)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("$You have more than one");
                            sb.Append(typeof(T).Name);
                            sb.Append(" in the scene. You only");
                            sb.Append("need 1, it's a singleton!");
                            TinyLogger.Instance.DebugLog(sb.ToString());

                            for (int i = 0; i < managers.Length; ++i)
                            {
                                T manager = managers[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }

                    GameObject go = new GameObject(typeof(T).Name, typeof(T));
                    TheInstance = go.GetComponent<T>();
                    DontDestroyOnLoad(TheInstance.gameObject);
                }

                return TheInstance;
            }
            set
            {
                TheInstance = value as T;
            }
        }

        void OnDestroy()
        {
            Alive = false;
        }

        void OnApplicationQuit()
        {
            Alive = false;
        }
    }
}