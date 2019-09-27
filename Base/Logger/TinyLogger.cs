using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.Logger
{
    public sealed class TinyLogger
    {
        private static readonly TinyLogger TheInstance = new TinyLogger();

        private TinyLogger() { }

        static TinyLogger() { }

        public static TinyLogger Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public bool IsDebugOpen = true;

        public bool IsErrorOpen = true;

        public bool IsWarningOpen = true;

        public void DebugLog(string log)
        {
            if (IsDebugOpen)
                Debug.Log(log);
        }

        public void ErrorLog(string log)
        {
            if (IsErrorOpen)
                Debug.LogError(log);
        }

        public void WarningLog(string log)
        {
            if (IsWarningOpen)
                Debug.LogWarning(log);
        }
    }
}