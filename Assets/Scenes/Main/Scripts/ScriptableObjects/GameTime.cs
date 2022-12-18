using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "GameTime", menuName = "Game Time", order = 0)]
    public class GameTime : ScriptableObject
    {
        private float resumeScale = 0f;

        private bool _paused = false;
        public bool Paused
        {
            get => _paused;
            private set
            {
                _paused = value;
            }
        }

        [SerializeField] private float defaultScale = 1f;

        private float _scale = 1f;
        public float Scale {
            get => _scale;
            private set
            {
                _scale = value;
                ScaleChanged?.Invoke(value);
            } 
        }
        public System.Action<float> ScaleChanged;
        public System.Action<float> TimeChanged;

        public float Time { get; private set; }
        [System.NonSerialized] public float DeltaTime;
        [System.NonSerialized] public float FixedDeltaTime;

        void OnEnable()
        {
            _scale = defaultScale;
            Paused = false;
        }

        public void Pause()
        {
            if (Paused) return;
            Paused = true;
            resumeScale = Scale;
            Scale = 0f;
            _scale = 0f;
        }

        public void Resume()
        {
            if (!Paused) return;
            Paused = false;
            Scale = resumeScale;
        }

        public void ResetScale()
        {
            Scale = defaultScale;
        }

        public void TickTime()
        {
            Time += DeltaTime * Scale;
        }

        public void SetTime(float newTime)
        {
            Time = newTime;
            TimeChanged?.Invoke(Time);
        }
    }
}
