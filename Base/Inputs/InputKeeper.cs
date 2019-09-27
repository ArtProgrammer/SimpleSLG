using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Utils;

namespace SimpleAI.Inputs
{
    public class InputKeeper : SingletonAsComponent<InputKeeper>
    {
        public static InputKeeper Instance
        { 
            get
            {
                return (InputKeeper)InsideInstance;
            }
        }

        public Action<Vector3> OnLeftClickPos;

        public Action<Transform> OnLeftClickObject;

        public Action<Vector3> OnRightClickPos;

        public Action<Transform> OnRightClickObject;

        public Action<Vector3> OnMovingPos;

        public Action<Transform> OnMovingObject;

        public Action<RaycastHit> OnLeftClickHit;

        public bool NeedMovingState = false;
    }
}