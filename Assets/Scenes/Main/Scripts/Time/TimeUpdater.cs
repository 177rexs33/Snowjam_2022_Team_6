using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class TimeUpdater : MonoBehaviour
    {
        [SerializeField] List<GameTime> gameTimes = new List<GameTime>();

        void Awake()
        {
            gameTimes.ForEach(t => t.Time = Time.time);
        }

        void Update()
        {
            gameTimes.ForEach(t => {
                t.DeltaTime = Time.deltaTime * t.Scale;
                t.Time += t.DeltaTime;
            });
        }

        void FixedUpdate()
        {
            gameTimes.ForEach(t => t.FixedDeltaTime = Time.fixedDeltaTime);
        }
    }
}
