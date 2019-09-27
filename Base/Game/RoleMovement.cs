using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
//using GameContent.SimAgent;

namespace SimpleAI.Game
{
    public class RoleMovement : MonoBehaviour, IUpdateable
    {
        //Transform Target;
        //BaseGameEntity Target = null;

        //SimWood Wood = null;

        private Vector3 Offset = Vector3.zero;

        private float RotValue = 0.0f;

        private bool NeedMove = false;

        private bool NeedRot = false;

        public float MoveSpeed = 10.0f;

        public float RotSpeed = 3.0f;

        // Start is called before the first frame update
        void Start()
        {
            GameLogicSupvisor.Instance.Register(this);

            //Wood = GetComponent<SimWood>();
        }

        // Update is called once per frame
        public void OnUpdate(float dt)
        {
            HandleInputs(dt);
        }

        public virtual void OnFixedUpdate(float dt)
        {
            Turning();
            Move();
        }

        public void Move()
        {
            if (NeedMove)
            {
                //transform.position += Offset;
                transform.Translate(Offset);
                Offset.x = Offset.y = Offset.z = 0.0f;
                NeedMove = false;
            }
        }

        public void Turning()
        {
            if (NeedRot)
            {
                transform.Rotate(Vector3.up, RotValue * Mathf.Rad2Deg); //RotValue
                RotValue = 0.0f;

                NeedRot = false;
            }
        }

        public Action OnSpaceClick;

        void HandleInputs(float dt)
        { 
            if (Input.GetKey(KeyCode.W))
            {
                Offset.z += dt * MoveSpeed;
                NeedMove = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                Offset.z -= dt * MoveSpeed;
                NeedMove = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Offset.x -= dt * MoveSpeed;
                NeedMove = true;
                //RotValue -= dt * RotSpeed;
                //NeedRot = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                Offset.x += dt * MoveSpeed;
                NeedMove = true;
                //RotValue += dt * RotSpeed;
                //NeedRot = true;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (OnSpaceClick != null)
                {
                    OnSpaceClick();
                }
            }            
        }

        void OnDestroy()
        {
            if (GameLogicSupvisor.IsAlive)
                GameLogicSupvisor.Instance.Unregister(this);
        }
    }
}
