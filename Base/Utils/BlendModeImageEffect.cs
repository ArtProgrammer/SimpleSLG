using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.Utils
{
    [ExecuteInEditMode]
    public class BlendModeImageEffect : MonoBehaviour
    {
        #region Variables
        public Shader CurShader;

        public Texture2D blendTexture;

        public float blendOpacity = 1.0f;

        private Material CurMaterial = null;

        public bool IsEnable = true;

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
        }

        // Update is called once per frame
        void Update()
        {
            if (IsEnable)
            {
                blendOpacity = Mathf.Clamp(blendOpacity, 0.0f, 1.0f);
            }
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            bool hasMat = false;

            if (IsEnable)
            {
                if (!System.Object.ReferenceEquals(null, CurShader))
                {
                    TheMaterial.SetTexture("_BlendTex", blendTexture);
                    TheMaterial.SetFloat("_Opacity", blendOpacity);

                    Graphics.Blit(source, destination, TheMaterial);
                    hasMat = true;
                }
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
        }
    }
}