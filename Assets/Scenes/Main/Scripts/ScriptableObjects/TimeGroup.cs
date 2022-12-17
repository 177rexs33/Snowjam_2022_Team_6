using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    [CreateAssetMenu(fileName = "TimeGroup", menuName = "Time Group", order = 0)]
    public class TimeGroup : ScriptableObject
    {
        public List<GameTime> GameTimes = new List<GameTime>();

        public void Pause()
        {
            GameTimes.ForEach(t => t.Pause());
        }

        public void Resume()
        {
            GameTimes.ForEach(t => t.Resume());
        }
    }
}
