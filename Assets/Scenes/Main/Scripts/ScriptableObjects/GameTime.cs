using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "GameTime", menuName = "Game Time", order = 0)]
    public class GameTime : ScriptableObject
    {
        private float resumeTime = 0f;

        private float _scale = 1f;
        public float Scale {
            get => _scale;
            private set
            {
                _scale = value;
                ScaleChanged(value);
            } 
        }
        public System.Action<float> ScaleChanged;

        [System.NonSerialized] public float Time;
        [System.NonSerialized] public float DeltaTime;
        [System.NonSerialized] public float FixedDeltaTime;

        public void Pause()
        {
            resumeTime = Scale;
            Scale = 0f;
        }

        public void Resume()
        {
            Scale = resumeTime;
        }
    }
}
