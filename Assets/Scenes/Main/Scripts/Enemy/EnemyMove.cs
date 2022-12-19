using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class EnemyMove : MonoBehaviour
    {
        enum MoveState
        {
            Path,
            Waiting,
            Charge,
            FollowScreen
        }

        [SerializeField] GameTime time;
        [SerializeField] ScreenProperties screenProperties;

        [SerializeField] PlayerContainerSO playerContainer;

        [System.NonSerialized] public float Speed;
        [System.NonSerialized] public List<object> Path;

        MoveState moveState = MoveState.Path;

        Vector3 moveEndPosition;
        Vector3 moveDirection;

        EnemyPathWaypoint curWaypoint;
        int waypointIndex = 0;
        bool autoAdvanceNextState = false;
        float nextStateTime;
        void Start()
        {
            if(Path.Count > 0 && Path[0] is EnemyPathWaypoint position)
            {
                transform.localPosition = position.Position;
                if(position.WaitTime > 0)
                {
                    nextStateTime = time.Time + position.WaitTime;
                    autoAdvanceNextState = true;
                    moveState = MoveState.Waiting;
                }
                AdvanceWaypoint();
            }
            else
            {
                Debug.LogWarning("Path does not start on a position, destroying");
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (autoAdvanceNextState && time.Time > nextStateTime)
            {
                autoAdvanceNextState = false;
                AdvanceWaypoint();
            }
            UpdateWaypoint();
        }

        void UpdateWaypoint()
        {
            if(waypointIndex >= Path.Count)
            {
                return;
            }
            if(moveState == MoveState.Path)
            {
                transform.localPosition += moveDirection * Speed * time.DeltaTime;
                // if dot product of velocity and displacement is positve 
                // then velocity is moving away from origin
                var ds = transform.localPosition - moveEndPosition;
                if (ds.x * moveDirection.x + ds.y * moveDirection.y > 0)
                {
                    transform.localPosition = moveEndPosition;

                    if (curWaypoint.WaitTime > 0)
                    {
                        moveState = MoveState.Waiting;
                        nextStateTime = time.Time + curWaypoint.WaitTime;
                        autoAdvanceNextState = true;
                    }
                    else
                    {
                        AdvanceWaypoint();
                    }
                }
            }
            else if (moveState == MoveState.FollowScreen)
            {
                transform.localPosition += time.DeltaTime * screenProperties.Speed * Vector3.down;
            }
            else if (moveState == MoveState.Charge)
            {
                var ds = 
                    playerContainer.Player.transform.localPosition
                    - transform.localPosition;
                transform.localPosition += time.DeltaTime * ds.normalized * Speed;
            }
        }

        void AdvanceWaypoint()
        {
            waypointIndex++;
            if(waypointIndex >= Path.Count)
            {
                return;
            }
            if (Path[waypointIndex] is EnemyPathWaypoint position)
            {
                moveState = MoveState.Path;
                curWaypoint = position;
                moveEndPosition = (Vector3)position.Position;
                moveDirection = moveEndPosition - transform.localPosition;
                moveDirection.Normalize();
            }
            else if (Path[waypointIndex] is EnemyPathFlag flag)
            {
                switch (flag.Flag)
                {
                    case EnemyPathFlagEnum.End:
                        End();
                        break;
                    case EnemyPathFlagEnum.FollowScreen:
                        moveState = MoveState.FollowScreen;
                        break;
                    case EnemyPathFlagEnum.Charge:
                        moveState = MoveState.Charge;
                        break;
                }
                if (flag.Time is float duration)
                {
                    autoAdvanceNextState = true;
                    nextStateTime = time.Time + duration;
                }
            }
        }

        void End()
        {
            Destroy(gameObject);
        }
    }
}
