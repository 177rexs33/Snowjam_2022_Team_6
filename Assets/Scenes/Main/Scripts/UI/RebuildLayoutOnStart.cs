using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SJ22
{
    public class RebuildLayoutOnStart : MonoBehaviour
    {
        [SerializeField] bool twice;
        void Start()
        {
            for(int i = 0; i < (twice ? 2 : 1); ++i)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            }
        }
    }
}
