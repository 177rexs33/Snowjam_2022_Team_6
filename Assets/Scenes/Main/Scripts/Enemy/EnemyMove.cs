using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] GameTime time;

        [System.NonSerialized] public float Speed;
        [System.NonSerialized] public List<object> Path;


        bool moving = false;
        Vector3 moveEndPosition;
        Vector3 moveDirection;

        EnemyPosition curWaypoint;
        int waypointIndex = 0;
        bool waiting = false;
        float waitEndTime;
        void Start()
        {
            if(Path.Count > 0 && Path[0] is EnemyPosition position)
            {
                transform.localPosition = position.Position;
                if(position.WaitTime > 0)
                {
                    waitEndTime = time.Time + position.WaitTime;
                    waiting = true;
                }
                waypointIndex++;
            }
            else
            {
                Debug.LogWarning("Path does not start on a position, destroying");
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if(waiting && time.Time > waitEndTime)
            {
                waiting = false;
            }
            if (!waiting)
            {
                UpdateWaypoint();
            }
        }

        void UpdateWaypoint()
        {
            if(waypointIndex >= Path.Count)
            {
                return;
            }

            if(moving)
            {
                transform.localPosition += moveDirection * Speed * time.DeltaTime;
                // if dot product of velocity and displacement is positve 
                // then velocity is moving away from origin
                var ds = transform.localPosition - moveEndPosition;
                if (ds.x * moveDirection.x + ds.y * moveDirection.y > 0)
                {
                    transform.localPosition = moveEndPosition;
                    moving = false;

                    if (curWaypoint.WaitTime > 0)
                    {
                        waiting = true;
                        waitEndTime = time.Time + curWaypoint.WaitTime;
                    }
                    waypointIndex++;
                }
            }
            else
            {
                if (Path[waypointIndex] is EnemyPosition position)
                {
                    moving = true;
                    curWaypoint = position;
                    moveEndPosition = (Vector3)position.Position;
                    moveDirection = moveEndPosition - transform.localPosition;
                    moveDirection.Normalize();
                }
                else if (Path[waypointIndex] is EnemyPositionEnd)
                {
                    End();
                }
            }
        }

        void End()
        {
            Destroy(gameObject);
        }
    }
}
