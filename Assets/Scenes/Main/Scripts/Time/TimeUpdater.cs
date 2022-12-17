using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class TimeUpdater : MonoBehaviour
    {
        [SerializeField] TimeGroup timeGroup;

        void Awake()
        {
            timeGroup.GameTimes.ForEach(t => t.Time = Time.time);
        }

        void Update()
        {
            timeGroup.GameTimes.ForEach(t => {
                t.DeltaTime = Time.deltaTime * t.Scale;
                t.Time += t.DeltaTime;
            });
        }

        void FixedUpdate()
        {
            timeGroup.GameTimes.ForEach(t => t.FixedDeltaTime = Time.fixedDeltaTime);
        }
    }
}
