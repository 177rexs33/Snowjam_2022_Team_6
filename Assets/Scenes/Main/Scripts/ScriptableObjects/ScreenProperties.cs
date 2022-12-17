using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "ScreenProperties", menuName = "Screen Properties", order = 0)]
    public class ScreenProperties : ScriptableObject
    {
        public float Width;
        public float Height;
    }
}
