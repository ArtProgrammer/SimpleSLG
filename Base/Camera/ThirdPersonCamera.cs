using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.TheCamera
{
    [ExecuteInEditMode]
    public class ThirdPersonCamera : MonoBehaviour
    {
        public Transform Target = null;

        private bool IsReady = false;

        [SerializeField]
        private bool HasChanged = false;

        public float Dis2Target = 100.0f;

        [SerializeField]
        private float AxisZRot = 0.0f;

        [SerializeField]
        private float AxisYRot = 45.0f;

        [SerializeField]
        private float AxisXRot = 0.0f;

        public float Distance
        {
            set 
            {
                if (!float.Equals(Dis2Target, value))
                {
                    HasChanged = true;
                }

                Dis2Target = value;
            }
            get {
                return Dis2Target;
            }
        }

        public float XRot
        {
            set {
                if (!float.Equals(AxisXRot, value))
                {
                    HasChanged = true;
                }

                AxisXRot = value;
            }
            get {
                return AxisXRot;
            }
        }

        public float YRot
        {
            set
            {
                if (!float.Equals(AxisYRot, value))
                {
                    HasChanged = true;
                }

                AxisYRot = value;
            }
            get
            {
                return AxisYRot;
            }
        }

        public float ZRot
        {
            set
            {
                if (!float.Equals(AxisZRot, value))
                {
                    HasChanged = true;
                }

                AxisZRot = value;
            }
            get
            {
                return AxisZRot;
            }
        }

        private Vector3 OffsetPos = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            HasChanged = true;

            IsReady = Target != null;
        }

        // Update is called once per frame
        void Update()
        {
            HandleMove();
        }

        protected void HandleMove()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                AxisYRot += 1f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                AxisYRot -= 1f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                AxisXRot -= 1f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                AxisXRot += 1f;
            }

            if (Input.GetKey(KeyCode.O))
            {
                Distance -= 1.0f;
            }
            if (Input.GetKey(KeyCode.P))
            {
                Distance += 1.0f;
            }

            if (HasChanged)
            {
                CalOffsetPos();

                HasChanged = false;
            }

            if (IsReady)
            {
                transform.position = Target.position + OffsetPos;
                transform.LookAt(Target);
            }            
        }

        protected void CalOffsetPos()
        {
            if (IsReady)
            {
                OffsetPos.y = Distance * Mathf.Sin(Mathf.Deg2Rad * AxisYRot);
                OffsetPos.x = Distance * Mathf.Cos(AxisYRot * Mathf.Deg2Rad) *
                    Mathf.Sin(AxisXRot * Mathf.Deg2Rad);
                OffsetPos.z = -Distance * Mathf.Cos(AxisYRot * Mathf.Deg2Rad) *
                    Mathf.Cos(AxisXRot * Mathf.Deg2Rad);
            }
        }
    }
}