using System;
using UnityEngine;

namespace SimpleAI.Timer
{
    /// <summary>
    /// Time in seconds. Data type is float.
    /// </summary>
    public sealed class TimeWrapper
    {
        private static readonly TimeWrapper TheInstance = new TimeWrapper();

        private DateTime StartDateTime = DateTime.Today;

        private float RealTimeFromStart = 0.0f;

        private float DeltaTime = 0.0f;

        private long CurTicks = 0;

        private long StartTicks = 0;

        private float MilliSecPerTicks = 0.0001f;

        private bool Inited = false;

        static TimeWrapper()
        {
            //Console.WriteLine("$$$ Time wrapper start.");
        }

        private TimeWrapper() { }

        public static TimeWrapper Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public float realtimeSinceStartup
        {
            get
            {
                //return Time.time;
                return GetRunningTime();
            }
        }

        public float deltaTime
        {
            get
            {
                return Time.deltaTime;
                //return GetDeltaTime();
            }
        }

        public void Init()
        {
            Inited = true;

            StartTicks = CurTicks = DateTime.Now.Ticks;
        }

        private void InitIfNotInited()
        {
            if (!Inited)
            {
                Init();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private float GetRunningTime()
        {
            InitIfNotInited();

            CurTicks = DateTime.Now.Ticks;

            RealTimeFromStart = 
                (float)(CurTicks - StartTicks) * MilliSecPerTicks;

            return RealTimeFromStart;
        }

        /// <summary>
        /// This not work as will yet. 0 mostly.
        /// </summary>
        /// <returns></returns>
        private float GetDeltaTime()
        {
            InitIfNotInited();

            long cur = DateTime.Now.Ticks;
            DeltaTime = (float)(cur - CurTicks) * MilliSecPerTicks;
            CurTicks = cur;

            return DeltaTime;
        }

        public void Reset()
        {
            Inited = false;
        }
    }
}
