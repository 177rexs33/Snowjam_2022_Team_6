using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class PlayerAction : MonoBehaviour
    {
        enum State
        {
            Ready,
            Active,
            Recharging
        }

        [SerializeField] GameTime time;
        [SerializeField] GameTime enemyTime;

        [SerializeField] GameInputReader inputReader;

        [SerializeField] EventSO actionReadyEvent;
        [SerializeField] EventSO actionActiveEvent;
        [SerializeField] EventSO actionRechargingEvent;

        [SerializeField] float actionDuration;
        [SerializeField] float rechargeDuration;

        State curState = State.Ready;
        float stateChangeTime;

        void Awake()
        {
            inputReader.Gameplay.Action += OnActionPressed;
        }

        void Update()
        {
            if(curState == State.Active) { 
                if(time.Time > stateChangeTime + actionDuration)
                {
                    enemyTime.Resume();
                    curState = State.Recharging;
                    stateChangeTime = time.Time;
                    actionRechargingEvent.Event?.Invoke();
                }
            }
            else if(curState == State.Recharging)
            {
                if (time.Time > stateChangeTime + rechargeDuration)
                {
                    curState = State.Ready;
                    stateChangeTime = time.Time;
                    actionReadyEvent.Event?.Invoke();
                }
            }
        }

        void OnActionPressed()
        {
            if(curState == State.Ready)
            {
                enemyTime.Pause();
                stateChangeTime = time.Time;
                curState = State.Active;
                actionActiveEvent.Event?.Invoke();
            }
        }
    }
}
