using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public abstract class ShotPath : ScriptableObject
    {
        public float TScale = 1f, XScale = 1f, YScale = 1f;
        public float TOffset = 0f;
        public abstract Vector2 GetPosition(float t);
    }
}
