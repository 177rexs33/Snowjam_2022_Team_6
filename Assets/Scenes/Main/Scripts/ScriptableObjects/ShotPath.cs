using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public abstract class ShotPath : ScriptableObject
    {
        public Vector2 PositionOffset = Vector2.zero;

        public float TScale = 1f, XScale = 1f, YScale = 1f;
        public float TOffset = 0f;
        public abstract Vector2 F(float t);

        public Vector2 GetPosition(float t)
        {
            var p = F(TScale * t + TOffset);
            return new Vector2(p.x * XScale, p.y * YScale) + PositionOffset;
        }
    }
}
