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
            Recharging,
            Disabled
        }

        [SerializeField] GameTime time;
        [SerializeField] GameTime enemyTime;

        [SerializeField] GameInputReader inputReader;

        [SerializeField] EventSO actionDisabledEvent;
        [SerializeField] EventSO actionReadyEvent;
        [SerializeField] EventSO actionActiveEvent;
        [SerializeField] EventSO actionRechargingEvent;

        [SerializeField] EventSO playerDeathEvent;
        [SerializeField] EventSO restartFromCheckpointEvent;

        [SerializeField] ActionParameters parameters;

        State curState = State.Ready;
        float stateChangeTime;

        void Awake()
        {
            inputReader.Gameplay.Action += OnActionPressed;

            restartFromCheckpointEvent.Event += OnRestartFromCheckpoint;
            playerDeathEvent.Event += OnPlayerDeath;
        }

        void OnDestroy()
        {
            restartFromCheckpointEvent.Event -= OnRestartFromCheckpoint;
            playerDeathEvent.Event -= OnPlayerDeath;

        }

        void OnRestartFromCheckpoint()
        {
            curState = State.Ready;
            stateChangeTime = time.Time;
            actionReadyEvent.Event?.Invoke();
        }

        void OnPlayerDeath()
        {
            curState = State.Disabled;
            stateChangeTime = time.Time;
            actionDisabledEvent.Event?.Invoke();
        }

        void Update()
        {
            if(curState == State.Active) { 
                if(time.Time > stateChangeTime + parameters.ActiveDuration)
                {
                    enemyTime.Resume();
                    curState = State.Recharging;
                    stateChangeTime = time.Time;
                    actionRechargingEvent.Event?.Invoke();
                }
            }
            else if(curState == State.Recharging)
            {
                if (time.Time > stateChangeTime + parameters.RechargeDuration)
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
