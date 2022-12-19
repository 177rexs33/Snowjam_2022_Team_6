using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class ShotProperties
    {
        public Sprite Sprite;
        public Bounds Hitbox;
        public float Angle;
        public ShotPath Path;
        public Vector2 Offset = Vector2.zero;
    }

    public abstract class AttackProperties : ScriptableObject
    {
        public class Wait
        {
            public float Seconds;
            public Wait(float seconds) => Seconds = seconds;
        }

        /// <returns>Either an instance of ShotProperties or an instance of Wait</returns>
        public abstract IEnumerator GenerateShotPattern();
    }
}
