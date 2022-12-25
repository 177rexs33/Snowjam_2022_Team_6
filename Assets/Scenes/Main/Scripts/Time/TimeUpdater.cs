using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class TimeUpdater : MonoBehaviour
    {
        [SerializeField] TimeGroup timeGroup;
        [SerializeField] EventSO playerDieEvent;

        void Awake()
        {
            playerDieEvent.Event += OnPlayerDie;
            
            timeGroup.GameTimes.ForEach(t => {
                t.Resume();
                t.ResetScale();
                t.SetTime(0);
            });
        }

        void Update()
        {
            timeGroup.GameTimes.ForEach(t => {
                t.DeltaTime = Time.deltaTime * t.Scale;
                t.TickTime();
            });


        }

        void FixedUpdate()
        {
            timeGroup.GameTimes.ForEach(t => t.FixedDeltaTime = Time.fixedDeltaTime);
        }

        void OnPlayerDie()
        {
            timeGroup.GameTimes.ForEach(t => t.Pause());
        }
    }
}
