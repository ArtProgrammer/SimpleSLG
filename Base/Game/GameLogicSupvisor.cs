using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;
using SimpleAI.Timer;

namespace SimpleAI.Game
{
    public interface IUpdateable
    {
        void OnUpdate(float dt);
        void OnFixedUpdate(float dt);
    }

    public class GameLogicSupvisor : SingletonAsComponent<GameLogicSupvisor>
    {
        public static GameLogicSupvisor Instance
        {
            get { return ((GameLogicSupvisor)InsideInstance); }
        }

        GameLogicSupvisor()
        {
            Debug.Log("$ GameLogicSupervisor ctor");
        }

        private bool IsGameFreezing = false;

        public bool IsFreezing
        {
            set
            {
                IsGameFreezing = value;
            }
            get
            {
                return IsGameFreezing;
            }
        }

        private float TheGameSpeed = 1;

        public float GameSpeed
        {
            set
            {
                TheGameSpeed = value;
            }
            get 
            {
                return TheGameSpeed;
            }
        }

        private void Start()
        {
            Debug.Log("$ GameLogicSupervisor start");
        }

        List<IUpdateable> UpdateableObjects = new List<IUpdateable>();

        public void Register(IUpdateable obj)
        {
            if (!Instance.UpdateableObjects.Contains(obj))
            {
                Instance.UpdateableObjects.Add(obj);
            }
        }

        public void Unregister(IUpdateable obj)
        {
            if (Instance.UpdateableObjects.Contains(obj))
            {
                Instance.UpdateableObjects.Remove(obj);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (IsGameFreezing)
                return;

            float dt = TimeWrapper.Instance.deltaTime * TheGameSpeed;

            for (int i = 0; i < Instance.UpdateableObjects.Count; i++)
            {
                Instance.UpdateableObjects[i].OnUpdate(dt);
            }
        }

        private void FixedUpdate()
        {
            if (IsGameFreezing)
                return;

            float dt = TimeWrapper.Instance.deltaTime * TheGameSpeed;

            for (int i = 0; i < Instance.UpdateableObjects.Count; i++)
            {
                Instance.UpdateableObjects[i].OnFixedUpdate(dt);
            }
        }
    }
}