using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent
{
    public class BuildingInfo : MonoBehaviour
    {
        RectTransform Root;

        public Text BuildingNameTxt;

        // Start is called before the first frame update
        void Start()
        {
            Root = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show(BaseBenefit bb)
        {
            //Root.gameObject.SetActive(false);
            Root.localScale = Vector3.one;
            BuildingNameTxt.text = bb.name;
        }

        public void Close()
        {
            //Root.gameObject.SetActive(false);
            Root.localScale = Vector3.zero;
        }
    }
}