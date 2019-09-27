using System;

namespace SimpleAI.Timer
{
    public class Regulator
    {
        private float UpdatePeriod = 0.0f;

        private float NextUpdateTime = 0.0f;

        private Random TheRandom = new Random();

        private float UpdatePeriodVariator = 10.0f;

        public Regulator(float numUpdatesPerSec)
        {
            //Console.WriteLine("$ regulator ctor.");
            SetNumUpdatesPerSec(numUpdatesPerSec);
        }

        private float GetUpdatePeriodSmoth()
        {
            return (TheRandom.Next() % 200 - 100) * 0.01f * UpdatePeriodVariator;
        }

        /// <summary>
        /// Set the number of updates per second.
        /// </summary>
        /// <param name="numUpdatesPerSec">The number of updates.</param>
        public void SetNumUpdatesPerSec(float numUpdatesPerSec)
        {
            if (numUpdatesPerSec.Equals(0.0f))
            {
                UpdatePeriod = 0.0f;
            }

            if (numUpdatesPerSec > 0.0f)
            {
                UpdatePeriod = 1000.0f / numUpdatesPerSec;
            }
            else
            {
                UpdatePeriod = -1.0f;
            }

            //Console.WriteLine("$ regulator SetNumUpdatesPerSec.");

            NextUpdateTime = TimeWrapper.Instance.realtimeSinceStartup + UpdatePeriod + GetUpdatePeriodSmoth();
        }

        /// <summary>
        /// Checking if these should be a update trigger.
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            if (UpdatePeriod.Equals(0.0f))
            {
                return true;
            }

            if (UpdatePeriod < 0) { return false; }

            float currentTime = TimeWrapper.Instance.realtimeSinceStartup;

            if (currentTime > NextUpdateTime)
            {
                NextUpdateTime = currentTime + UpdatePeriod + GetUpdatePeriodSmoth();
                return true;
            }

            return false;
        }
    }
}
