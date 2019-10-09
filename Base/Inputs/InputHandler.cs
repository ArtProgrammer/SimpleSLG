using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SimpleAI.Game;
using SimpleAI.Logger;
using SimpleAI.Utils;


namespace SimpleAI.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        private Vector3 CurClickPos = Vector3.zero;

        private Vector3 CurMovePos = Vector3.zero;

        private BaseGameEntity CurSelectEntity = null;

        private Transform CurSelectTrans = null;

        private Camera MainCamera = null;

        private Transform selfTrans = null;

        private Ray TheRay;

        private RaycastHit TheHit;

        public float MaxDistance = 1000.0f;

        public GameObject CurHitObject = null;

        public GameObject CurMoveObject = null;

        // Start is called before the first frame update
        void Start()
        {
            MainCamera = Camera.main;
            selfTrans = MainCamera.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (EventSystem.current &&
                EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                CurHitObject = null;

                if (GetClick(Input.mousePosition, 
                    ref CurHitObject,
                    ref CurClickPos))
                {
                    TinyLogger.Instance.DebugLog("$ click pos " + 
                        CurClickPos.x.ToString() + ", " + CurClickPos.y.ToString() + ", " +
                        CurClickPos.z.ToString()
                        );

                    if (!System.Object.ReferenceEquals(null, InputKeeper.Instance.OnLeftClickPos))
                    {
                        InputKeeper.Instance.OnLeftClickPos(CurClickPos);
                    }

                    if (!System.Object.ReferenceEquals(null, InputKeeper.Instance.OnLeftClickObject) &&
                        !System.Object.ReferenceEquals(null, CurHitObject))
                    {                        
                        InputKeeper.Instance.OnLeftClickObject(CurHitObject.transform);
                        CurHitObject = null;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (GetClick(Input.mousePosition,
                    ref CurHitObject,
                    ref CurClickPos))
                {
                    if (!System.Object.ReferenceEquals(null, InputKeeper.Instance.OnRightClickPos))
                    {
                        InputKeeper.Instance.OnRightClickPos(CurClickPos);
                    }

                    if (!System.Object.ReferenceEquals(null, InputKeeper.Instance.OnRightClickObject) &&
                        !System.Object.ReferenceEquals(null, CurHitObject))
                    {
                        InputKeeper.Instance.OnRightClickObject(CurHitObject.transform);
                        CurHitObject = null;
                    }
                }
            }

            if (InputKeeper.Instance.NeedMovingState)
            {
                //GetPosOnFly(ref CurMovePos);
                if (GetClick(Input.mousePosition,
                    ref CurHitObject,
                    ref CurClickPos))
                {
                    if (!System.Object.ReferenceEquals(null, InputKeeper.Instance.OnMovingPos))
                    {
                        InputKeeper.Instance.OnMovingPos(CurMovePos);
                    }

                    if (!System.Object.ReferenceEquals(null, InputKeeper.Instance.OnMovingObject) &&
                        !System.Object.ReferenceEquals(null, CurHitObject))
                    {
                        InputKeeper.Instance.OnMovingObject(CurHitObject.transform);
                        CurHitObject = null;
                    }
                }
            }
        }

        public void GetPosOnFly(ref Vector3 position)
        {
            if (GetClick(Input.mousePosition,
                    ref CurHitObject,
                    ref CurClickPos))
            {
                position = CurClickPos;
            }
        }

        public bool GetClick(Vector3 pos, 
            ref GameObject obj, 
            ref Vector3 clickPos)
        {
            TheRay = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(TheRay, out TheHit, MaxDistance))
            {
                if (!System.Object.ReferenceEquals(null, TheHit.rigidbody))
                {
                    obj = TheHit.rigidbody.gameObject;
                }
                else if (!System.Object.ReferenceEquals(null, TheHit.transform))
                {
                    obj = TheHit.transform.gameObject;
                }

                clickPos = TheHit.point;

                //Debug.DrawLine(TheRay.origin, TheHit.point);
                Debug.DrawLine(Camera.main.transform.position, TheHit.point, Color.red);

                return true;
            }

            return false;
        }
    }
}