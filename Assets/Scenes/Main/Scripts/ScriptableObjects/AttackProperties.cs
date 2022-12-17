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
    }

    public abstract class AttackProperties : ScriptableObject
    {
        public class Wait
        {
            public float Seconds;
            public Wait(float seconds) => Seconds = seconds;
        }

        public Sprite Sprite;
        public Bounds Hitbox;

        /// <returns>Either an instance of ShotProperties or an instance of Wait</returns>
        public abstract IEnumerator GenerateShotPattern();
    }
}
