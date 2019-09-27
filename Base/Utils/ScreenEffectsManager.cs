using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.Utils
{
    //[ExecuteInEditMode]
    public class ScreenEffectsManager : MonoBehaviour
    {
        #region Variables
        public Shader CurShader;

        public float GrayScaleAmount = 1.0f;

        private Material CurMaterial = null;

        //
        public Shader DepthShader = null;

        private Material DepthMaterial = null;

        public float DepthPower = 1.0f;
        #endregion

        #region Properties
        Material TheMaterial
        { 
            get
            { 
                if (System.Object.ReferenceEquals(null, CurMaterial))
                {
                    CurMaterial = new Material(CurShader);
                    CurMaterial.hideFlags = HideFlags.HideAndDontSave;
                }
                return CurMaterial;
            }
        }

        Material DepthMat
        { 
            get
            { 
                if (System.Object.ReferenceEquals(null, DepthMaterial))
                {
                    DepthMaterial = new Material(DepthShader);
                    DepthMaterial.hideFlags = HideFlags.HideAndDontSave;
                }
                return DepthMaterial;
            }
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            if (!SystemInfo.supportsImageEffects)
            {
                enabled = false;
                return;
            }

            if (!CurShader && !CurShader.isSupported)
            {
                enabled = false;
            }

            if (!DepthShader && !DepthShader.isSupported)
            {
                enabled = false; 
            }

        }

        // Update is called once per frame
        void Update()
        {
            GrayScaleAmount = Mathf.Clamp(GrayScaleAmount, 0.0f, 1.0f);

            Camera.main.depthTextureMode = DepthTextureMode.Depth;

            DepthPower = Mathf.Clamp(DepthPower, 0, 5);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            bool hasMat = false;

            if (!System.Object.ReferenceEquals(null, CurShader) &&
                !System.Object.ReferenceEquals(null, TheMaterial))
            {
                TheMaterial.SetFloat("_LuminosityAmount", GrayScaleAmount);
                Graphics.Blit(source, destination, TheMaterial);
                hasMat = true;
            }

            if (!System.Object.ReferenceEquals(null, DepthShader) &&
                !System.Object.ReferenceEquals(null, DepthMat))
            {
                DepthMat.SetFloat("_DepthPower", DepthPower);
                Graphics.Blit(source, destination, DepthMat);
                hasMat = true;
            }

            if (!hasMat)
            {
                Graphics.Blit(source, destination);
            }
        }

        private void OnDisable()
        {
            if (CurMaterial)
            {
                DestroyImmediate(CurMaterial);
            }

            if (DepthMat)
            {
                DestroyImmediate(DepthMat);
            }
        }
    }
}